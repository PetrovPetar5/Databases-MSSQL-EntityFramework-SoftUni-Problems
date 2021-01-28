SELECT TOP(1) AVG(Salary) AS [MinAverageSalary] 
FROM Employees E
JOIN Departments AS D ON  E.DepartmentID = D.DepartmentID
GROUP BY D.[Name]
ORDER BY [MinAverageSalary] ASC

SELECT MIN([AverageDepSal]) AS [MinAverageSalary]
 FROM 
		(SELECT  AVG(Salary) AS [AverageDepSal] FROM Employees AS E
		GROUP BY E.DepartmentID  ) AS [MinDepartmentSalaryQuery]