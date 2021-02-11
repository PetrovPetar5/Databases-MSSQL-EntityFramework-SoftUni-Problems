SELECT CONCAT(M.FirstName, ' ',M.LastName) AS [Mechanic], j.[Status], J.IssueDate FROM Mechanics AS M
 LEFT JOIN Jobs AS J ON M.MechanicId = J.MechanicId
 ORDER BY M.MechanicId,J.IssueDate,J.JobId