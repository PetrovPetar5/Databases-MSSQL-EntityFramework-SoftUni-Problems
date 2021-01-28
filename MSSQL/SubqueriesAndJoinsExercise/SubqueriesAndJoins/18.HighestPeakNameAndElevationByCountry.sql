	SELECT TOP(5) 	CountryName AS [Country],
	[Highest Peak Name],	
	[Highest Peak Elevation],	
	[Mountain]						
	 FROM                                  (SELECT c.CountryName,
											CASE
											WHEN p.PeakName  is null then '(no highest peak)'
											ELSE p.PeakName
											END AS [Highest Peak Name],
											CASE
											WHEN P.PeakName IS NULL THEN 0
											ELSE P.Elevation
											END AS [Highest Peak Elevation],
											CASE
											WHEN p.PeakName  is null then '(no mountain)'
											ELSE M.MountainRange
											END AS [Mountain],
											DENSE_RANK() OVER(PARTITION BY C.CountryName ORDER BY P.Elevation DESC) 
											AS PeakRank
											FROM Countries AS C
											LEFT JOIN MountainsCountries AS MC ON C.CountryCode = MC.CountryCode
											LEFT JOIN Mountains AS M ON MC.MountainId = M.Id
											LEFT JOIN Peaks AS P ON M.Id = P.MountainId
											) AS VGZ
WHERE PeakRank = 1
ORDER BY CountryName ASC, [Highest Peak Name] ASC