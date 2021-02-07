SELECT 	JobDuringJourney,	
		[FullName],	
		[JobRank] 
FROM			(
					SELECT	TC.JobDuringJourney AS JobDuringJourney,
							CONCAT(C.FirstName,' ',C.LastName) AS [FullName],
							DENSE_RANK() OVER( partition by TC.JobDuringJourney  ORDER BY C.BirthDate ASC) AS [JobRank]
							FROM TravelCards AS TC
							JOIN Colonists AS C ON TC.ColonistId = C.Id
				) AS RankSubquery
WHERE [JobRank] = 2