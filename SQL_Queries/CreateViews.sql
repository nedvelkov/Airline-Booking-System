USE ABS_database
GO

CREATE VIEW GetAirportNames AS
SELECT [Name] FROM Airport

CREATE VIEW GetAirlineNames AS
SELECT [Name] FROM Airline

CREATE OR ALTER VIEW GetAirlinesWithFlightsCount AS
SELECT Airline.Name,COUNT(Flight.Id) AS Flights FROM Flight
	JOIN Airline on Flight.AirlineId=Airline.Id
	WHERE  IsDeparted=0	
	GROUP BY Airline.Name