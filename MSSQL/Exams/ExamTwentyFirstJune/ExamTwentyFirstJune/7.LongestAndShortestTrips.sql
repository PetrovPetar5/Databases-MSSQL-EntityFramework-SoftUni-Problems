SELECT		Id,
			[FullName], 
			MAX([DiffTrip]) AS [LongestTrip],
			MIN([DiffTrip]) AS [ShortestTrip]	 		
FROM					(
								SELECT	a.Id,
										CONCAT(A.FirstName,' ',A.LastName) AS [FullName],
										ABS(DATEDIFF(day,t.ReturnDate,t.ArrivalDate)) AS [DiffTrip]
								FROM	AccountsTrips AT
										JOIN Accounts AS A ON A.Id = AT.AccountId
										JOIN Trips AS T ON T.Id = AT.TripId
								WHERE	A.MiddleName IS NULL AND T.CancelDate IS NULL
						) AS DateDiffSubquery
GROUP BY [FullName],Id
ORDER BY [LongestTrip] DESC,[ShortestTrip] ASC