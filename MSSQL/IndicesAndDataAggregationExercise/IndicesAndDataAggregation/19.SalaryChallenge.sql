SELECT TOP(10)	FirstName, 
				LastName,
				DepartmentID 
		FROM Employees AS E1
		WHERE E1.Salary >  (
						SELECT	AVG(Salary) AS [AverageDepartmentSalary] 
						FROM Employees AS E2
						WHERE E2.DepartmentID = E1.DepartmentID
						GROUP BY DepartmentID
					) 
		ORDER BY E1.DepartmentID ASC