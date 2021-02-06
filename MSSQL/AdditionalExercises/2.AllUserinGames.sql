SELECT	G.[Name] AS [Game],
		GT.[Name] AS [Game Type], 
		u.Username, 
		ug.[Level], 
		ug.Cash, 
		CH.[Name] AS [Character] 
FROM UsersGames 
AS UG
LEFT JOIN Users AS U ON UG.UserId=U.Id
LEFT JOIN Games AS G ON UG.GameId = G.Id
LEFT JOIN GameTypes AS GT ON G.GameTypeId = GT.Id
LEFT JOIN Characters AS CH ON UG.CharacterId = CH.Id
ORDER BY ug.[Level] DESC,u.Username ASC,[Game] ASC