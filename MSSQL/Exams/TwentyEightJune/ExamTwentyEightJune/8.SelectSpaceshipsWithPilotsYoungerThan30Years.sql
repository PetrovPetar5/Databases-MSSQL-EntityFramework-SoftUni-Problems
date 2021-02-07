SELECT S.[Name], S.Manufacturer 
FROM Spaceships AS S
JOIN Journeys AS J ON S.Id = J.SpaceshipId
JOIN TravelCards AS TC ON J.Id = TC.JourneyId
JOIN Colonists AS C ON TC.ColonistId = C.Id
WHERE TC.JobDuringJourney LIKE 'pilot' AND DATEDIFF(YEAR,C.BirthDate,'01/01/2019') <30
ORDER BY S.[Name] ASC