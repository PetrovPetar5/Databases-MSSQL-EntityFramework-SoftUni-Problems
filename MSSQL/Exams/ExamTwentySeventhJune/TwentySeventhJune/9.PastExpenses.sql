SELECT	J.JobId, 
		SUM(P.Price) AS [Total]
FROM	Jobs AS J
		JOIN Orders AS O ON J.JobId = O.JobId
		JOIN OrderParts AS OP ON O.OrderId = OP.OrderId
		JOIN Parts AS P ON OP.PartId = P.PartId
WHERE	J.[Status] LIKE 'Finished'
GROUP BY J.JobId
ORDER BY SUM(P.Price) DESC, J.JobId ASC