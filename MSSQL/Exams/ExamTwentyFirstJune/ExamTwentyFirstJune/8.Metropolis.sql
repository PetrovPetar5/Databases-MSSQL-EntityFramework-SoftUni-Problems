SELECT TOP(10)	C.Id, 
				C.[Name],
				C.CountryCode, COUNT(*) AS [Accounts] 
FROM			Cities AS C
				JOIN Accounts AS A ON C.Id = A.CityId
GROUP BY		C.Id,C.[Name],C.CountryCode
ORDER BY		[Accounts] DESC