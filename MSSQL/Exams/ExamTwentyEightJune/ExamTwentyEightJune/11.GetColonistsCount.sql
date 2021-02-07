CREATE  FUNCTION udf_GetColonistsCount(@PlanetName VARCHAR (30))
RETURNS INT
AS
BEGIN
RETURN	
	(
		SELECT COUNT(TC.ColonistId) AS [Count] 
		FROM Journeys AS J 
			JOIN Spaceports AS SS ON J.DestinationSpaceportId = SS.Id
			JOIN Planets AS P ON SS.PlanetId = P.Id
			JOIN TravelCards AS TC ON J.Id = TC.JourneyId
		WHERE P.[Name] = @PlanetName
	)
END