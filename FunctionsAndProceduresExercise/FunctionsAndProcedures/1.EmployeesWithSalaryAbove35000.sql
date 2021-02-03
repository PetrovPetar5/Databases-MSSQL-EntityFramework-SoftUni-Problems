CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000
AS
	SELECT FirstName,LastName 
	FROM Employees 
	AS e
	WHERE e.Salary > 35000
GO