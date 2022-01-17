CREATE OR ALTER FUNCTION ufn_CheckNameAirport(@Name CHAR(3))
RETURNS BIT
AS
BEGIN
	IF(LEN(@Name)=3 AND @NAME NOT LIKE '%[^A-Z]%' AND dbo.ufn_AllUpperLettersAirlineName(@Name)=1)
	BEGIN
		RETURN 1
	END

	RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_CheckNameAirline(@Name VARCHAR(5))
RETURNS BIT
AS
BEGIN
	DECLARE @NameLength INT;
	DECLARE @NameWithLettersOnly INT;
	IF(LEN(@Name)>=1 OR LEN(@Name)<=5)
	BEGIN
		SET @NameLength=1;
	END
	IF(@Name NOT LIKE '%[^A-Z]%')
	BEGIN
		SET @NameWithLettersOnly=1
	END
	IF(@NameLength=1 AND @NameWithLettersOnly=1)
	BEGIN
		RETURN 1
	END
	RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_GetAirlineId(@Name CHAR(50))
RETURNS INT
AS
BEGIN
	DECLARE @AirlineCount INT;
	SET @AirlineCount=(SELECT COUNT(*) FROM Airline WHERE [Name] LIKE @Name)
	IF(@AirlineCount!=0)
	BEGIN
		RETURN -1
	END
	DECLARE @AirlaneId INT
	SET @AirlaneId=(SELECT TOP(1) Id FROM Airline WHERE [Name] LIKE @Name)

	RETURN @AirlaneId

END
GO

CREATE OR ALTER FUNCTION ufn_GetAirportId(@Name CHAR(3))
RETURNS INT
AS
BEGIN
	DECLARE @AirportCount INT;
	SET @AirportCount=(SELECT COUNT(*) FROM Airport WHERE [Name] LIKE @Name)
	IF(@AirportCount!=1)
	BEGIN
		RETURN -1
	END
	DECLARE @AirportId INT
	SET @AirportId=(SELECT TOP(1) Id FROM Airport WHERE [Name] LIKE @Name)

	RETURN @AirportId

END
GO


CREATE OR ALTER FUNCTION ufn_GetAirlineId(@Name VARCHAR(5))
RETURNS INT
AS
BEGIN
	DECLARE @AirlineCount INT;
	SET @AirlineCount=(SELECT COUNT(*) FROM Airline WHERE [Name] LIKE @Name)
	IF(@AirlineCount!=1)
	BEGIN
		RETURN -1
	END

	RETURN (SELECT TOP(1) Id FROM Airline WHERE [Name] LIKE @Name)

END
GO

CREATE OR ALTER FUNCTION ufn_OriginDifferenFromDestination(@Origin CHAR(3),@Destination CHAR(3))
RETURNS BIT
AS
BEGIN
	IF(@Origin!=@Destination)
	BEGIN
		RETURN 1
	END

	RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_CheckFlightId(@Id VARCHAR(40))
RETURNS BIT
AS
BEGIN
	IF(@Id LIKE '%[^A-Z0-9]%')
	BEGIN
		RETURN 1
	END

	RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_AllUpperLettersAirlineName(@Name CHAR(3))
RETURNS BIT
AS
BEGIN
	DECLARE @INDEX SMALLINT
	SET @INDEX=1
	WHILE @INDEX<=3
	BEGIN
		DECLARE @CharAsInt INT
		SET @CharAsInt=(SELECT ASCII(SUBSTRING(@Name,@INDEX,1)))
		IF(@CharAsInt<65 OR @CharAsInt>90) 	
			RETURN 0
		SET @INDEX+=1
	END
	RETURN 1
END
GO

CREATE OR ALTER FUNCTION ufn_ValidSeatClass(@SeatClassNumber SMALLINT)
RETURNS BIT
AS
BEGIN
	IF(@SeatClassNumber>=1 AND @SeatClassNumber<=3)
		RETURN 1

		RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_ValidRowNumber(@ROW SMALLINT)
RETURNS BIT
AS
BEGIN
	IF(@ROW>=1 AND @ROW<=100)
		RETURN 1

		RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_ValidColumnChar(@Column CHAR(1))
RETURNS BIT
AS
BEGIN
		DECLARE @CharAsInt INT
		SET @CharAsInt=(SELECT ASCII(@Column))
		IF(@CharAsInt>=65 OR @CharAsInt<=74) 	
			RETURN 1
		RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_ValidColumnsCount(@Columns SMALLINT)
RETURNS BIT
AS
BEGIN
		IF(@Columns>=1 OR @Columns<=10) 	
			RETURN 1
		RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_GetFlightSectionId(@FlightId VARCHAR(40),@SeatClass SMALLINT)
RETURNS INT
AS
BEGIN
	DECLARE @FlightSectionId INT;
	SET @FlightSectionId=(SELECT COUNT(*) 
									FROM FlightSection 
									WHERE (FlightId LIKE @FlightId AND SeatClass LIKE @SeatClass))
	IF(@FlightSectionId!=1)
	BEGIN
		RETURN -1
	END

	RETURN (SELECT TOP(1) Id 
				FROM FlightSection 
				WHERE (FlightId LIKE @FlightId AND SeatClass LIKE @SeatClass))

END
GO

CREATE OR ALTER FUNCTION ufn_IsFlightExist(@FlightId VARCHAR(40))
RETURNS BIT
AS
BEGIN
	DECLARE @Flights INT;
	SET @Flights=(SELECT COUNT(*) FROM Flight WHERE Id LIKE @FlightId)
	IF(@Flights=1)
	BEGIN
		RETURN 1
	END
	RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_GetColumnChar(@Column SMALLINT)
RETURNS CHAR(1)
AS
BEGIN
	DECLARE @ColumnChar CHAR(1)
	SET @ColumnChar=CHAR(@Column + 64)
	RETURN @ColumnChar
END
GO


CREATE OR ALTER FUNCTION ufn_IsFlightSectionExist(@FlightId VARCHAR(50),@SeatClass SMALLINT)
RETURNS BIT
AS
BEGIN
	DECLARE @FlightSectionCount INT;
	SET @FlightSectionCount=(SELECT COUNT(*) FROM FlightSection 
											 WHERE (FlightId LIKE @FlightId AND SeatClass LIKE @SeatClass))
	IF(@FlightSectionCount>=1)
	BEGIN
		RETURN 1
	END

	RETURN 0

END
GO

CREATE OR ALTER FUNCTION ufn_IsFlightBelongToaAirline(@FlightId VARCHAR(50),@AirlineName VARCHAR(5))
RETURNS BIT
AS
BEGIN
	DECLARE @AirlineId INT;
	SET @AirlineId=dbo.ufn_GetAirlineId(@AirlineName)
	DECLARE @FlightCount SMALLINT;
	SET @FlightCount=(SELECT COUNT(*) 
									FROM Flight 
									WHERE AirlineId LIKE @AirlineId)
	IF(@FlightCount=1)
	BEGIN
		RETURN 1
	END

	RETURN 0

END
GO