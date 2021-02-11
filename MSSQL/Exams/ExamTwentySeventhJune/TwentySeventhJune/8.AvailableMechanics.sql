		SELECT m.FirstName + ' ' + m.LastName AS [Available]
    FROM Mechanics AS m
             LEFT JOIN Jobs J
                       ON m.MechanicId = J.MechanicId
    WHERE J.Status = 'Finished'
       OR J.JobId IS NULL
    ORDER BY m.MechanicId