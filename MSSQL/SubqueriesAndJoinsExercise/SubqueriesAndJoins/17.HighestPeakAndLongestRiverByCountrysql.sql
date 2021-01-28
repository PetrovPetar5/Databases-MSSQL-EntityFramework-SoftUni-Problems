SELECT TOP(5) C.CountryName, MAX(P.Elevation) AS [Highest Peak Elevation], MAX(R.[Length]) AS [Longest River Length] 
FROM Countries AS C
LEFT JOIN MountainsCountries AS MC ON C.CountryCode = MC.CountryCode
LEFT JOIN Mountains AS M  ON MC.MountainId = M.Id
LEFT JOIN Peaks AS P ON M.Id = P.MountainId
LEFT JOIN CountriesRivers CR ON CR.CountryCode = C.CountryCode
LEFT JOIN Rivers AS R ON R.Id = CR.RiverId
GROUP BY C.CountryName
ORDER BY [Highest Peak Elevation] DESC, [Longest River Length] DESC, C.CountryName ASC
