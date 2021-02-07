SELECT COUNT(*) FROM TravelCards AS TC
JOIN Journeys AS J ON TC.JourneyId = J.Id
WHERE J.Purpose LIKE 'Technical'
GO
