SELECT SUM([Difference]) 
AS [SumDifference] 
FROM	(	SELECT	DepositAmount - [NextDeposit] AS [Difference] 
			FROM	(	SELECT  Id, 
			 		DepositAmount, 
			 		LEAD(DepositAmount,1, NULL) OVER(ORDER BY Id ASC) AS [NextDeposit]
					FROM WizzardDeposits
					) AS Subquery
		) AS SecondSubquery
