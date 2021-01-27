SELECT TOP(50) E.FirstName, E.LastName, T.[Name] AS [Town] , A.AddressText
FROM EMPLOYEES AS E
JOIN Addresses AS A ON A.AddressID = E.AddressID
JOIN Towns AS T ON A.TownID = T.TownID
ORDER BY E.FirstName, E.LastName