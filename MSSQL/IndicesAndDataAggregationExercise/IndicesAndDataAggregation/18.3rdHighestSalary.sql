SELECT	DISTINCT DepartmentID, 
		Salary AS [ThirdHigestSalary]
FROM (
		SELECT	DepartmentID,
				Salary,
				DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS [Rank]
		FROM Employees
	)	AS Subquery
WHERE RANK = 3
