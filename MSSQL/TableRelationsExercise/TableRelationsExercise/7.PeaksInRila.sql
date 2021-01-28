SELECT MountainRange, p.PeakName, p.Elevation FROM Mountains AS M 
JOIN Peaks AS P ON M.Id = P.MountainId
WHERE M.MountainRange = 'Rila'
ORDER BY P.Elevation DESC