SELECT FirstName FROM Employees
WHERE DepartmentID IN (3,10) AND YEAR(Employees.HireDate) BETWEEN 1995 AND 2005