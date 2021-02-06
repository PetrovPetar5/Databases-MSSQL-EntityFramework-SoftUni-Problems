USE SoftUniDatabase

 CREATE PROC usp_DeleteEmployeesFromDepartment(@departmentId INT)
AS
	ALTER TABLE Employees
	DROP CONSTRAINT [FK_Employees_Departments]

	ALTER TABLE Employees
	DROP CONSTRAINT [FK_Employees_Employees]

	ALTER TABLE [dbo].[EmployeesProjects]
	DROP CONSTRAINT [FK_EmployeesProjects_Employees]

	ALTER TABLE Departments
	DROP CONSTRAINT [FK_Departments_Employees]

	DELETE FROM Employees
	WHERE Employees.DepartmentID = @departmentId
GO