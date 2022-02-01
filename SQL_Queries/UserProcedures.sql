CREATE OR ALTER PROC usp_CreateAirport(@Name CHAR(3))
AS
BEGIN TRANSACTION
	DECLARE @ValidName BIT
	SET @ValidName=dbo.ufn_CheckNameAirport(@Name)
	IF(@ValidName=0)
	BEGIN
		ROLLBACK
		;THROW 50008,'Invalid name',1;
	END
	IF(dbo.ufn_GetAirportId(@Name)!=-1)
	BEGIN
		ROLLBACK
		;THROW 50009,'Airport with this name already exist',1;
	END
	INSERT Airport(Name) VALUES(@Name);
	COMMIT
GO

CREATE OR ALTER PROC usp_CreateAirline(@Name VARCHAR(5))
AS
BEGIN TRANSACTION
	IF(dbo.ufn_CheckNameAirline(@Name)=0)
	BEGIN
		ROLLBACK
		;THROW 50010,'Invalid name',1;
	END
	IF(dbo.ufn_GetAirlineId(@Name)!=-1)
	BEGIN
		ROLLBACK
		;THROW 50011,'Airline with this name already exist',1;
	END
	INSERT Airline(Name) VALUES(@Name);
	COMMIT
GO

CREATE OR ALTER PROC usp_CreateFlightDate(@Year INT,@Month INT,@Day INT,@Date Date OUTPUT)
AS
	DECLARE @DateOfFlight DATETIME
	SET @DateOfFlight=DATEFROMPARTS( @Year, @Month, @Day );
	IF(@DateOfFlight>GETDATE())
	BEGIN
		SET @Date=@DateOfFlight
		RETURN
	END
		;THROW 50002,'Invalid flight date',1
GO

CREATE OR ALTER PROC usp_CreateFlight(@AirlineName VARCHAR(5),@Origin CHAR(3),@Destination CHAR(3),@Year INT,@Month INT,@Day INT,@Id CHAR(40))
AS
BEGIN TRANSACTION
	--Validate origin and destination point
	IF([dbo].[ufn_OriginDifferenFromDestination](@Origin,@Destination)=0)
	BEGIN
		ROLLBACK
		;THROW 50003,'Destination must be different from origin point',1;
	END

	--Validate flight id
	IF([dbo].[ufn_CheckFlightId](@Id)=0)
	BEGIN
		ROLLBACK
		;THROW 50004,'Invalid flight id',1;
	END

	--GET Airline id
		DECLARE @AirlineId INT
		SET @AirlineId=[dbo].[ufn_GetAirlineId](@AirlineName)
	IF(@AirlineId=-1)
	BEGIN
		ROLLBACK
		;THROW 50005,'Airline with this name do not exist',1;
	END

	--GET Origin id
		DECLARE @OriginId INT
		SET @OriginId=[dbo].ufn_GetAirportId(@Origin)
	IF(@OriginId=-1)
	BEGIN
		ROLLBACK
		;THROW 50006,'Airport with this origin name do not exist',1;
	END

	--GET Destination id
		DECLARE @DestinationId INT
		SET @DestinationId=[dbo].ufn_GetAirportId(@Destination)
	IF(@DestinationId=-1)
	BEGIN
		ROLLBACK
		;THROW 50007,'Airport with this destination name do not exist',1;
	END

	--GET Flight date
	DECLARE @FlightDate DATE
	EXEC usp_CreateFlightDate @Year,@Month,@Day,@Date=@FlightDate OUTPUT


	INSERT INTO Flight VALUES(@Id,@AirlineId,@OriginId,@DestinationId,@FlightDate,0)
	COMMIT
GO

CREATE OR ALTER PROC usp_CreateFlightSection(@AirlineName VARCHAR(5),@FlightId VARCHAR(40),@Rows SMALLINT,@Columns SMALLINT,@SeatClass SMALLINT)
AS
BEGIN TRANSACTION

	--Validate seat class
	IF(dbo.ufn_ValidSeatClass(@SeatClass)=0)
	BEGIN
		ROLLBACK
		;THROW 50012,'Invalid seat class',1;
	END

		--Validate rows count
	IF(dbo.ufn_ValidRowNumBer(@Rows)=0)
	BEGIN
		ROLLBACK
		;THROW 50012,'Invalid rows count',1;
	END

			--Validate columns count 
	IF(dbo.ufn_ValidColumnsCount(@Columns)=0)
	BEGIN
		ROLLBACK
		;THROW 50013,'Invalid columns count',1;
	END

	--Validate flight
	IF(dbo.ufn_IsFlightExist(@FlightId)=0)
	BEGIN
		ROLLBACK
		;THROW 50014,'Flight with this id do not exist',1;
	END

	--GET Airline id
		DECLARE @AirlineId INT
		SET @AirlineId=[dbo].[ufn_GetAirlineId](@AirlineName)
	IF(@AirlineId=-1)
	BEGIN
		ROLLBACK
		;THROW 50015,'Airline with this name do not exist',1;
	END

	--Validate flight is part of airline
	IF(dbo.ufn_IsFlightBelongToaAirline(@FlightId,@AirlineName)=0)
	BEGIN
		ROLLBACK
		;THROW 50016,'Flight is not part of this airline',1;
	END

	--Validate if flight section already exist on this flight
	IF(dbo.ufn_IsFlightSectionExist(@FlightId,@SeatClass)=1)
	BEGIN
		ROLLBACK
		;THROW 50017,'Flight section already exist',1;
	END

	INSERT INTO FlightSection VALUES (@SeatClass,@FlightId);

	--GET Flight section id
	DECLARE @FlightSectionId INT
	SET @FlightSectionId=dbo.ufn_GetFlightSectionId(@FlightId,@SeatClass)
	IF(@FlightSectionId=-1)
	BEGIN
		ROLLBACK
		;THROW 50018,'Flght section with this class do not exist on this flight',1;
	END
	
	DECLARE @CurrentRow SMALLINT
	SET @CurrentRow=1;
	WHILE(@CurrentRow<=@Rows)
	BEGIN
		DECLARE @CurrentColumnNum SMALLINT
		SET @CurrentColumnNum=1
		WHILE(@CurrentColumnNum<=@Columns)
		BEGIN
			DECLARE @ColumChar CHAR(1)
			SET @ColumChar=CHAR(@CurrentColumnNum+64)
			INSERT INTO Seat VALUES(@CurrentRow,@ColumChar,@FlightSectionId,0)
			SET @CurrentColumnNum+=1
		END
		SET @CurrentRow+=1
	END
	COMMIT
GO

CREATE OR ALTER PROC usp_GetFlightIds
AS
	BEGIN TRANSACTION
		SET NOCOUNT ON;
		EXEC usp_CheckIfFlightIsDeparted
		SELECT Id FROM [dbo].Flight
	COMMIT
GO

CREATE OR ALTER PROC usp_FindAvailableFlights(@Origin CHAR(3),@Destination CHAR(3))
AS
	BEGIN TRANSACTION
		SET NOCOUNT ON;

	--Validate origin and destination point
	IF([dbo].[ufn_OriginDifferenFromDestination](@Origin,@Destination)=0)
	BEGIN
		ROLLBACK
		;THROW 50003,'Destination must be different from origin point',1;
	END

	--GET Origin id
		DECLARE @OriginId INT
		SET @OriginId=[dbo].ufn_GetAirportId(@Origin)
	IF(@OriginId=-1)
	BEGIN
		ROLLBACK
		;THROW 50006,'Airport with this origin name do not exist',1;
	END

	--GET Destination id
		DECLARE @DestinationId INT
		SET @DestinationId=[dbo].ufn_GetAirportId(@Destination)
	IF(@DestinationId=-1)
	BEGIN
		ROLLBACK
		;THROW 50007,'Airport with this destination name do not exist',1;
	END

	EXEC usp_CheckIfFlightIsDeparted

	SELECT Flight.Id,Airline.Name AS AirlineName,Flight.Date,AirOrig.Name as Origin,AirDest.Name as Destination
	FROM Flight
			 JOIN Airline ON Flight.AirlineId=Airline.Id
			 JOIN Airport AirOrig ON Flight.OriginId=AirOrig.Id
			 JOIN Airport AirDest ON Flight.DestinationId=AirDest.Id
			 WHERE OriginId=@OriginId AND DestinationId=@DestinationId AND IsDeparted=0

	COMMIT
GO

CREATE OR ALTER PROC usp_BookSeat(@AirlineName VARCHAR(5),@FlightId VARCHAR(40),@SeatClass SMALLINT,@Row SMALLINT,@Column CHAR(1))
AS
	BEGIN TRANSACTION

	--Validate seat class
	IF(dbo.ufn_ValidSeatClass(@SeatClass)=0)
	BEGIN
		ROLLBACK
		;THROW 50012,'Invalid seat class',1;
	END

		--Validate rows count
	IF(dbo.ufn_ValidRowNumBer(@Row)=0)
	BEGIN
		ROLLBACK
		;THROW 50031,'Invalid row number',1;
	END

			--Validate columns count 
	DECLARE @ColumnAsInt INT
	SET @ColumnAsInt=(SELECT ASCII(@Column))
	IF(dbo.ufn_ValidColumnsCount(@ColumnAsInt)=0)
	BEGIN
		ROLLBACK
		;THROW 50032,'Invalid column letter',1;
	END

	--Validate flight
	IF(dbo.ufn_IsFlightExist(@FlightId)=0)
	BEGIN
		ROLLBACK
		;THROW 50014,'Flight with this id do not exist',1;
	END

	--GET Airline id
		DECLARE @AirlineId INT
		SET @AirlineId=[dbo].[ufn_GetAirlineId](@AirlineName)
	IF(@AirlineId=-1)
	BEGIN
		ROLLBACK
		;THROW 50015,'Airline with this name do not exist',1;
	END

	--Validate flight is part of airline
	IF(dbo.ufn_IsFlightBelongToaAirline(@FlightId,@AirlineName)=0)
	BEGIN
		ROLLBACK
		;THROW 50016,'Flight is not part of this airline',1;
	END

	--Validate if flight section already exist on this flight
	IF(dbo.ufn_IsFlightSectionExist(@FlightId,@SeatClass)=0)
	BEGIN
		ROLLBACK
		;THROW 50033,'Flight section do not exist',1;
	END

		--GET Seat id
		--GET Flight section id
	DECLARE @FlightSectionId INT
	SET @FlightSectionId=dbo.ufn_GetFlightSectionId(@FlightId,@SeatClass)
		DECLARE @SeatId BIGINT
		SET @SeatId=([dbo].[ufn_GetSeatId](@FlightSectionId,@Row,@Column))
		--SELECT @SeatId
	IF(@SeatId IS NULL)
	BEGIN
		ROLLBACK
		;THROW 50035,'Seat with this numer do not exist',1;
	END
	
	IF((SELECT [Booked] FROM Seat WHERE Id=@SeatId)=1)
	BEGIN
		ROLLBACK
		;THROW 50036,'Seat is already booked',1;
	END

	UPDATE Seat
	SET Booked = 1
	WHERE Id = @SeatId;

	COMMIT
GO

CREATE OR ALTER PROC usp_GetRowsAndColumsOfFlightSection(@FlightId VARCHAR(40),@SeatClass SMALLINT)
AS
	BEGIN TRANSACTION
		SET NOCOUNT ON;
		DECLARE @FlightSectionId INT
		SET @FlightSectionId=(SELECT dbo.ufn_GetFlightSectionId(@FlightId,@SeatClass))
		SELECT TOP(1) [ROW],[COLUMN] FROM Seat WHERE FlightSectionId=@FlightSectionId ORDER BY Id DESC
	COMMIT
GO

CREATE OR ALTER PROC usp_HasAirport(@AirportName CHAR(3),@HasAirport BIT OUTPUT)
AS
	BEGIN TRANSACTION
	SET @HasAirport=(SELECT COUNT(*) FROM Airport WHERE [Name] LIKE @AirportName)
	COMMIT
GO

CREATE OR ALTER PROC usp_GetArilinesView
AS
	EXEC usp_CheckIfFlightIsDeparted

	SELECT Airline.Name AS AirlineName,
	   Flight.id as FlightId,
	   AirOrig.Name AS Origin,
	   AirDest.Name AS Destination,
	   FlightSection.SeatClass,
	   Seat.[Row],Seat.[COLUMN],Seat.Booked
	FROM Airline 
			JOIN Flight on Flight.AirlineId=Airline.Id
			JOIN Airport AirOrig ON Flight.OriginId=AirOrig.Id
			JOIN Airport AirDest ON Flight.DestinationId=AirDest.Id
			JOIN FlightSection on FlightSection.FlightId=Flight.Id
			JOIN Seat on Seat.FlightSectionId =FlightSection.Id
			WHERE Flight.IsDeparted =0					
GO

CREATE OR ALTER PROC usp_CheckIfFlightIsDeparted
AS
	BEGIN TRANSACTION
		UPDATE Flight
		SET IsDeparted=1
		WHERE [Date]<=GETDATE()
	COMMIT
GO