CREATE OR ALTER FUNCTION ufn_CheckNameAirport(@Name CHAR(50))
RETURNS BIT
AS
BEGIN
	IF(LEN(@Name)=3 AND @NAME LIKE '%[^A-Z]%')
	BEGIN
		RETURN 1
	END

	RETURN 0
END
GO

CREATE OR ALTER FUNCTION ufn_CheckNameAirline(@Name CHAR(50))
RETURNS BIT
AS
BEGIN
	DECLARE @NameLength INT;
	DECLARE @NameWithLettersOnly INT;
	IF(LEN(@Name)>=1 OR LEN(@Name)<=5)
	BEGIN
		SET @NameLength=1;
	END
	IF(@Name LIKE '%[^A-Z]%')
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

CREATE OR ALTER FUNCTION ufn_CheckNameAirline(@Name CHAR(50))
RETURNS BIT
AS
BEGIN
	DECLARE @NameLength INT;
	DECLARE @NameWithLettersOnly INT;
	IF(LEN(@Name)>=1 OR LEN(@Name)<=5)
	BEGIN
		SET @NameLength=1;
	END
	IF(@Name LIKE '%[^A-Z]%')
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