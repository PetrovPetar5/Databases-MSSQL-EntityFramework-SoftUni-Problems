SELECT [Email Provider], COUNT(*) AS [Number of Users]
FROM	(
			SELECT SUBSTRING(u.Email,CHARINDEX('@',U.Email) + 1,LEN(U.Email) - CHARINDEX('@',U.Email))
			AS [Email Provider]
			FROM Users 
			AS U
		) AS Subquery
GROUP BY [Email Provider]
ORDER BY [Number of Users] DESC, [Email Provider] ASC
