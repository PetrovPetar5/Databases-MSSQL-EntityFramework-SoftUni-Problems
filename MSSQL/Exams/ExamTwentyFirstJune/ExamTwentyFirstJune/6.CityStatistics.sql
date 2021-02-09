SELECT		C.[Name] AS [City], 
			COUNT(*) AS [Hotels] 
FROM		Cities AS C
			JOIN Hotels AS H ON H.CityId = C.Id
GROUP BY	C.[Name]
ORDER BY	 [Hotels] DESC, C.[Name] ASC