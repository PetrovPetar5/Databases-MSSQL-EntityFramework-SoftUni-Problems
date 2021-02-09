SELECT	T.Id,
			CONCAT(A.FirstName,' ',ISNULL(A.MiddleName + ' ',''),A.LastName) AS [Full Name],
			c.[Name] AS [From],
			C2.[Name] AS [To],
CASE
WHEN		T.CancelDate IS NOT NULL THEN 'Canceled'
ELSE		CAST(ABS(DATEDIFF(DAY,T.ReturnDate,T.ArrivalDate)) AS NVARCHAR) + ' days'
END AS		[Duration]
FROM		AccountsTrips AS AR
			JOIN Accounts AS A ON AR.AccountId = A.Id
			JOIN Trips AS T ON AR.TripId = T.Id
			JOIN Cities AS C ON A.CityId = C.Id
			JOIN Rooms AS R ON T.RoomId = R.Id
			JOIN Hotels AS H ON R.HotelId = H.Id
			JOIN Cities AS C2 ON H.CityId = C2.Id
ORDER BY [Full Name],Id
