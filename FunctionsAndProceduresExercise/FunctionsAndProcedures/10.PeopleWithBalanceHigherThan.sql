SELECT * FROM AccountHolders
CREATE PROC usp_GetHoldersWithBalanceHigherThan (@TotalBalance DECIMAL(18,2))
AS
	SELECT FirstName, LastName 
	FROM AccountHolders ah
	JOIN Accounts AS a ON AH.Id = A.AccountHolderId
	GROUP BY FirstName, LastName
	HAVING SUM(Balance) > @TotalBalance
	ORDER BY FirstName,LastName
GO