USE Geography

SELECT C.CountryCode, M.MountainRange, P.PeakName, P.Elevation FROM Countries AS C
JOIN MountainsCountries AS MC ON MC.CountryCode = C.CountryCode
JOIN Mountains AS M ON M.Id = MC.MountainId
JOIN Peaks AS P ON M.Id =P.MountainId
WHERE C.CountryCode = 'BG' AND P.Elevation > 2835
ORDER BY P.Elevation DESC