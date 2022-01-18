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


	INSERT INTO Flight VALUES(@Id,@AirlineId,@OriginId,@DestinationId,@FlightDate)
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
			INSERT INTO Seat VALUES(@CurrentRow,@ColumChar,@FlightSectionId)
			SET @CurrentColumnNum+=1
		END
		SET @CurrentRow+=1
	END
	COMMIT
GO

CREATE OR ALTER PROC usp_GetAirports
AS

	BEGIN TRANSACTION
		SET NOCOUNT ON;
		SELECT [Name] FROM Airport
	COMMIT
GO

CREATE OR ALTER PROC usp_GetAirlines
AS

	BEGIN TRANSACTION
		SET NOCOUNT ON;
		SELECT [Name] FROM [dbo].[Airline]
	COMMIT
GO