SELECT E1.EmployeeID ,E1.FirstName, E1.ManagerID, E2.FirstName AS [ManagerName] FROM EMPLOYEES AS E1
JOIN Employees AS E2 ON E1.ManagerID = E2.EmployeeID
WHERE E1.ManagerID IN (3,7)
ORDER BY E1.EmployeeID ASC