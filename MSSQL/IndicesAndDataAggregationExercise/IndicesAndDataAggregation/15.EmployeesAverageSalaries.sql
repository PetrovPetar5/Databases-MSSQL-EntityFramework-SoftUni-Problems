		SELECT * INTO [EmployeesWithHigherSalaries] 
		FROM Employees
		WHERE Salary > 30000

		DELETE FROM [EmployeesWithHigherSalaries]
		WHERE ManagerID = 42

		UPDATE [EmployeesWithHigherSalaries]
		SET Salary +=5000
		WHERE DepartmentID = 1

		SELECT DepartmentID, 
				AVG(Salary) AS [AverageSalary] 
		FROM [EmployeesWithHigherSalaries]
		GROUP BY DepartmentID