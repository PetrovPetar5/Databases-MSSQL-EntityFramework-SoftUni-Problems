SELECT TOP(5) EmployeeID AS [EmployeeId], JobTitle, E.AddressID AS [AddressId], A.AddressText 
FROM Employees AS E
JOIN Addresses AS A ON E.AddressID = A.AddressID
ORDER BY E.AddressID ASC