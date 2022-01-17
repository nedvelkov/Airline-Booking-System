CREATE DATABASE ABS_database
GO

USE ABS_database
GO

CREATE TABLE Airline
(
	Id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Name] VARCHAR (5) NOT NULL,
	CONSTRAINT CHK_Airline_Name CHECK([dbo].[ufn_CheckNameAirline]([Name])=1),
	CONSTRAINT CHK_Unique_AirlineName UNIQUE([Name])
)

CREATE TABLE Airport
(
	Id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Name] CHAR (3) NOT NULL,
	CONSTRAINT CHK_Airport_Name CHECK(dbo.ufn_CheckNameAirport([Name])=1),
	CONSTRAINT CHK_Unique_AirportName UNIQUE([Name])
)

CREATE TABLE Flight
(
	Id VARCHAR(40) PRIMARY KEY,
	AirlineId INT NOT NULL,
	OriginId INT NOT NULL,
	DestinationId INT NOT NULL,
	Date Date NOT NULL,
	CONSTRAINT FK_Airline_Name FOREIGN KEY (AirlineId) REFERENCES Airline(Id),
	CONSTRAINT FK_Airport_Origin FOREIGN KEY (OriginId) REFERENCES Airport(Id),
	CONSTRAINT FK_Airport_Destination FOREIGN KEY (DestinationId) REFERENCES Airport(Id),
	CONSTRAINT CHK_Flight_Id CHECK(dbo.ufn_CheckFlightId([Id])=1)
)

CREATE TABLE FlightSection
(
	Id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	SeatClass SMALLINT NOT NULL,
	FlightId VARCHAR(40),
	CONSTRAINT FK_Flight_Id FOREIGN KEY (FlightId) REFERENCES Flight(Id),
	CONSTRAINT CHK_Flight_Section CHECK(dbo.ufn_ValidSeatClass(SeatClass)=1)

)

CREATE TABLE Seat
(
	Id BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[ROW] SMALLINT NOT NULL,
	[COLUMN] CHAR(1) NOT NULL,
	FlightSectionId INT NOT NULL,
	CONSTRAINT CHK_ROW_NUMBER CHECK(dbo.ufn_ValidRowNumber([Row])=1),
	CONSTRAINT CHK_COLUMN_CHAR CHECK(dbo.ufn_ValidColumnChar([Column])=1),
	CONSTRAINT FK_FlightSection_Id FOREIGN KEY (FlightSectionId) REFERENCES FlightSection(Id)
)