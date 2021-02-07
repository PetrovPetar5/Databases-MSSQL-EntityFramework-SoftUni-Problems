SELECT	P.[Name] AS [PlanetName], 
		COUNT(J.Id) AS [JourneysCount]  
FROM Planets AS P
JOIN Spaceports AS SS ON P.Id=SS.PlanetId
JOIN Journeys AS J ON J.DestinationSpaceportId = SS.Id
GROUP BY P.[Name]
ORDER BY [JourneysCount] DESC,[PlanetName] ASC