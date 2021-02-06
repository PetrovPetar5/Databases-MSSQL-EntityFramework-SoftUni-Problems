CREATE PROC usp_GetTownsStartingWith (@TownStartingWith NVARCHAR(100))
AS
	SELECT [Name] 
	FROM Towns 
	AS t
	WHERE LEFT(t.[Name],LEN(@TownStartingWith)) = @TownStartingWith
GO