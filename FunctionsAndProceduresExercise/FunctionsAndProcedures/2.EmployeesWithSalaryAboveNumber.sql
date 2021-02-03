CREATE PROC usp_GetEmployeesSalaryAboveNumber (@Number DECIMAL(18,2))
AS
	SELECT FirstName, LastName 
	FROM Employees AS e
	WHERE e.Salary >= @Number
GO