SELECT	U.Username,
		G.[Name], COUNT(I.Id) AS [Items Count], 
		SUM(I.Price) AS [Items Price] 
FROM UsersGames AS UG
 JOIN Users AS U ON UG.UserId = U.Id
 JOIN Games AS G ON UG.GameId = G.Id
 JOIN UserGameItems AS UGI ON UG.Id = UGI.UserGameId 
 JOIN Items AS I ON UGI.ItemId = I.Id
GROUP BY U.Username, G.[Name]
HAVING COUNT(*) >= 10
ORDER BY [Items Count] DESC,[Items Price] DESC,U.Username ASC
