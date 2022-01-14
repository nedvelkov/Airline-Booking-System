CREATE OR ALTER PROC usp_CreateAirport(@Name CHAR(3))
AS
BEGIN TRANSACTION
	DECLARE @ValidName BIT
	SET @ValidName=dbo.ufn_CheckNameAirport(@Name)
	IF(@ValidName=0)
	BEGIN
		ROLLBACK
		RETURN 'Invalid name';
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
		RETURN 'Invalid name';
	END
	INSERT Airline(Name) VALUES(@Name);
	COMMIT
GO

CREATE OR ALTER PROC usp_CreateFlightDate(@Year INT,@Month INT,@Day INT,@Date Date OUTPUT)
AS
	SET @Date=DATEFROMPARTS( @Year, @Month, @Day );
	--IF(@Date> CAST(GETDATE() AS DATE))
	--BEGIN
	--	RETURN
	--END
	--ELSE
	--	THROW 50002,'Invalid flight date',1
GO

CREATE OR ALTER PROC usp_CreateFlight(@AirlineName VARCHAR(5),@Origin CHAR(3),@Destination CHAR(3),@Year INT,@Month INT,@Day INT,@Id CHAR(40))
AS
BEGIN TRANSACTION
	--Validate origin and destination point
	IF([dbo].[ufn_OriginDifferenFromDestination](@Origin,@Destination)=0)
	BEGIN
		ROLLBACK
		RETURN 'Destination must be different from origin point';
	END

	--Validate flight id
	IF([dbo].[ufn_CheckFlightId](@Id)=0)
	BEGIN
		ROLLBACK
		RETURN 'Invalid flight id';
	END

	--GET Airline id
		DECLARE @AirlineId INT
		SET @AirlineId=[dbo].[ufn_GetAirlineId](@AirlineName)
	IF(@AirlineId=-1)
	BEGIN
		ROLLBACK
		RETURN 'Airline with this name do not exist';
	END

	--GET Origin id
		DECLARE @OriginId INT
		SET @OriginId=[dbo].[ufn_CheckNameAirport](@Origin)
	IF(@OriginId=-1)
	BEGIN
		ROLLBACK
		RETURN 'Airport with this origin name do not exist';
	END

	--GET Destination id
		DECLARE @DestinationId INT
		SET @DestinationId=[dbo].[ufn_CheckNameAirport](@Destination)
	IF(@DestinationId=-1)
	BEGIN
		ROLLBACK
		RETURN 'Airport with this destination name do not exist';
	END

	--GET Flight date
	DECLARE @FlightDate DATE
	BEGIN TRY  
		EXEC usp_CreateFlightDate 2021,2,15,@Date=@FlightDate OUTPUT
	END TRY  
	BEGIN CATCH
		ROLLBACK
		RETURN ERROR_MESSAGE()
	END CATCH; 

	INSERT INTO Flight Values(@Id,@AirlineId,@OriginId,@DestinationId,@FlightDate)
	COMMIT
GO

