CREATE PROC usp_GetEmployeesFromTown  (@TownName NVARCHAR(50) )
AS 
	SELECT FirstName, LastName 
	FROM Employees 
	AS e
	LEFT JOIN Addresses AS a ON e.AddressID = a.AddressID
	LEFT JOIN Towns AS t ON a.TownID = t.TownID
	WHERE T.[NAME] = @TownName
GO