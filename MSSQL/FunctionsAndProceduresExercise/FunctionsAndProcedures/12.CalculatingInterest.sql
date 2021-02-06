CREATE PROC usp_CalculateFutureValueForAccount(@AccountID INT,@Interest FLOAT)
AS
BEGIN
	SELECT	A.Id AS [Account id],
			FirstName AS [First Name], 
			AH.LastName AS [Last Name], 
			A.Balance AS [Current Balance],
			dbo.ufn_CalculateFutureValue(a.Balance,@Interest,5) AS [Balance in 5 years]
			FROM AccountHolders AS AH
	JOIN Accounts AS  A ON AH.Id = A.AccountHolderId
	WHERE @AccountID = A.Id
END
GO