CREATE OR ALTER PROC usp_CreateAirport(@Name CHAR(50))
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

CREATE OR ALTER PROC usp_CreateAirline(@Name CHAR(50))
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