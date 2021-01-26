SELECT P.PeakName, R.RiverName, LOWER(CONCAT(P.PeakName,SUBSTRING(R.RiverName,2,LEN(R.RiverName)-1))) AS Mix  
FROM Peaks AS P, Rivers AS R
WHERE RIGHT(P.PeakName,1) = LEFT(R.RiverName,1)
ORDER BY Mix
