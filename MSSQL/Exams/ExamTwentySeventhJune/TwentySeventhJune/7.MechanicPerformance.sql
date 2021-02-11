			
			
SELECT CONCAT(FirstName,' ',LastName) AS [Mechanic], AVG([DaysDifference]) AS [Average Days]  FROM	(		
				SELECT ABS(DATEDIFF(DAY,J.FinishDate,J.IssueDate)) AS [DaysDifference], M.MechanicId,M.FirstName,M.LastName FROM Mechanics AS M
				JOIN Jobs AS J ON M.MechanicId = J.MechanicId
			) AS Subquery
GROUP BY MechanicId, FirstName,LastName
ORDER BY MechanicId ASC
