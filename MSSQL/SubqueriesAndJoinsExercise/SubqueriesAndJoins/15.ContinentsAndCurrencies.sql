SELECT ContinentCode,
		CurrencyCode,
		CurrencyUsage 
FROM (SELECT ContinentCode,
			CurrencyCode,
			CurrencyUsage,
			DENSE_RANK() OVER(Partition by ContinentCode ORDER BY CurrencyUsage DESC )  AS CurrencyRank   
		FROM	(SELECT ContinentCode, 
						CurrencyCode, 
						COUNT(CurrencyCode) AS CurrencyUsage  
				FROM COUNTRIES
				GROUP BY ContinentCode, CurrencyCode
				) AS Q1
		WHERE CurrencyUsage > 1
	) AS Q2
	
WHERE CurrencyRank = 1
ORDER BY ContinentCode ASC

