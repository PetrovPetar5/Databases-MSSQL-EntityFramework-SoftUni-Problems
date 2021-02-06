CREATE PROC usp_EmployeesBySalaryLevel @Cryteria NVARCHAR(50)
AS
	SELECT FirstName, LastName 
	FROM Employees 
		AS e
	WHERE  dbo.ufn_GetSalaryLevel(e.Salary) = @Cryteria
GO