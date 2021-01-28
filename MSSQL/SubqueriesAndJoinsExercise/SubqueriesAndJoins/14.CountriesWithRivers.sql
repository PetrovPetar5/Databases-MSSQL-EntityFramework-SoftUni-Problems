SELECT TOP(5) C.CountryName, R.RiverName FROM Countries AS C
JOIN Continents AS CO ON C.ContinentCode = CO.ContinentCode
LEFT JOIN CountriesRivers AS CR ON C.CountryCode = CR.CountryCode
LEFT JOIN Rivers AS R ON CR.RiverId = R.Id
WHERE CO.ContinentCode LIKE 'AF'
ORDER BY C.CountryName ASC
