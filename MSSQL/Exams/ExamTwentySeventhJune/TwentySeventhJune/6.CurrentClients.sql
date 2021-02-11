SELECT CONCAT(C.FirstName,' ',C.LastName) AS [Client],ABS(DATEDIFF(DAY,'2017/04/24',J.IssueDate)) AS [Days going], J.[Status] FROM JOBS AS J 
JOIN Clients AS C ON J.ClientId = C.ClientId
WHERE J.FinishDate IS NULL
ORDER BY [Days going] DESC, C.ClientId ASC