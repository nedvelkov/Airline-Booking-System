CREATE OR ALTER FUNCTION ufn_CheckNameAirport(@Name CHAR(3))
RETURNS BIT
AS
BEGIN
	IF(LEN(@Name)=3 AND @NAME NOT LIKE '%[^A-Z]%' AND [dbo].[ufn_AllUpperLetters](@Name)=1)
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

CREATE OR ALTER FUNCTION ufn_GetAirportId(@Name CHAR(50))
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
	SET @AirportId=(SELECT TOP(1) Id FROM Airline WHERE [Name] LIKE @Name)

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

CREATE OR ALTER FUNCTION ufn_AllUpperLetters(@Name CHAR(3))
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