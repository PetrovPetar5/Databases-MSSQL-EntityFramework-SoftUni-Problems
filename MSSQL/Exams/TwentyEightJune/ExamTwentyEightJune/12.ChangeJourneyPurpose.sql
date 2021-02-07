CREATE PROC usp_ChangeJourneyPurpose(@JourneyId INT, @NewPurpose VARCHAR(11))
AS
BEGIN
	IF (@JourneyId NOT IN (SELECT  Id FROM Journeys))
	THROW 50001,'The journey does not exist!',1;

	IF	((SELECT COUNT(*) FROM Journeys AS J
		WHERE @JourneyId = J.Id AND J.Purpose = @NewPurpose) != 0)
		THROW 50002,'You cannot change the purpose!',1

	UPDATE Journeys
	SET Journeys.Purpose = @NewPurpose
	WHERE Journeys.Id = @JourneyId
END
