SELECT	FirstName,
		LastName,
		FORMAT(BirthDate,'MM-dd-yyyy'), 
		C.[NAME], 
		A.Email AS [BirthDate] 
FROM	Accounts AS A
		JOIN Cities AS C ON A.CityId = C.Id
WHERE	A.Email LIKE 'E%'
ORDER BY C.[NAME] ASC