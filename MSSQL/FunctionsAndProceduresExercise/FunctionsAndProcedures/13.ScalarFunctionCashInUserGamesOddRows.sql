CREATE   FUNCTION ufn_CashInUsersGames(@GameName NVARCHAR(Max))
Returns TABLE
AS
RETURN
(
	SELECT SUM(Cash) 
		AS [SumCash]	 
	FROM(
			SELECT	G.Name, 
					UG.Cash, 
					ROW_NUMBER() OVER(ORDER BY UG.Cash DESC) AS [ROW] 
			FROM UsersGames UG
			JOIN Games AS G ON UG.GameId = G.Id
			WHERE G.[Name] LIKE @GameName
		) 
		AS Subquery
	WHERE [ROW] % 2 !=0
	GROUP BY Name
)
GO

