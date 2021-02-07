SELECT	C.Id, 
		CONCAT(FirstName,' ',LastName) AS [full_name] 
FROM Colonists AS C
JOIN TravelCards AS TC ON C.Id = TC.ColonistId
WHERE TC.JobDuringJourney LIKE 'pilot'
ORDER BY C.Id ASC