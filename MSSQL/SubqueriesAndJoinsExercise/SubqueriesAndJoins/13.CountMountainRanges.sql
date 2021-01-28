SELECT MC.CountryCode, COUNT(MC.MountainId) AS MontainRanges  FROM MountainsCountries AS MC
WHERE MC.CountryCode IN ('BG','RU','US')
GROUP BY MC.CountryCode
