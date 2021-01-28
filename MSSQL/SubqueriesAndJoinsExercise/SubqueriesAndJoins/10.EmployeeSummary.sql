SELECT TOP(50) 
			E1.EmployeeID,
			CONCAT(E1.FirstName,' ',E1.LastName) AS EmployeeName,
			CONCAT(M.FirstName,' ',M.LastName) AS ManagerName,
			D.[Name] AS DepartmentName
FROM EMPLOYEES AS E1
LEFT JOIN Employees AS M ON E1.ManagerID = M.EmployeeID
LEFT JOIN Departments AS D ON E1.DepartmentID = D.DepartmentID
ORDER BY E1.EmployeeID ASC
