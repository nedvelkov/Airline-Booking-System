CREATE DATABASE ABS_database
USE ABS_database


CREATE TABLE Airlines
(
	Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	[Name] VARCHAR (5) NOT NULL,
	CONSTRAINT CHK_Airline_Name CHECK(LEN([Name]) >=1 AND LEN([Name]) <=5)
)

CREATE TABLE Airports
(
	Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	[Name] CHAR (3) NOT NULL,
	CONSTRAINT CHK_Airport_Name CHECK(LEN([Name])=3)
)

CREATE TABLE Flights
(
	Id VARCHAR(50) PRIMARY KEY,
	Name CHAR (3) NOT NULL,
	AirlineId uniqueidentifier NOT NULL,
	OriginId uniqueidentifier NOT NULL,
	DestinationId uniqueidentifier NOT NULL,
	Date Date,
	CONSTRAINT FK_Airline_Name FOREIGN KEY (AirlineId) REFERENCES Airlines(Id),
	CONSTRAINT FK_Airport_Origin FOREIGN KEY (OriginId) REFERENCES Airports(Id),
	CONSTRAINT FK_Airport_Destination FOREIGN KEY (DestinationId) REFERENCES Airports(Id)
)

CREATE TABLE FlightSections
(
	Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	SeatClass INT NOT NULL,
	FlightId VARCHAR(50),
	CONSTRAINT FK_Flight_Id FOREIGN KEY (FlightId) REFERENCES Flights(Id)
)

CREATE TABLE Seats
(
	[ROW] INT NOT NULL,
	[COLUMN] CHAR(1) NOT NULL,
	FlightSectionId uniqueidentifier,
	CONSTRAINT PK_Seat PRIMARY KEY ([ROW], [COLUMN]),
	CONSTRAINT CHK_ROW_NUMBER CHECK([ROW]>=1 AND [ROW]<=100),
	CONSTRAINT CHK_COLUMN_CHAR CHECK([COLUMN]>='A' AND [COLUMN]<='J'),
	CONSTRAINT FK_FlightSection_Id FOREIGN KEY (FlightSectionId) REFERENCES FlightSections(Id)
)