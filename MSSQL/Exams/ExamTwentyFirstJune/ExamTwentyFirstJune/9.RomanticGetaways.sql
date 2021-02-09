SELECT	A.Id, 
		A.Email, 
		C.[Name] ,
		COUNT(*) AS [Trips] 
FROM	AccountsTrips AS AR
		JOIN Accounts AS A ON AR.AccountId = A.Id
		JOIN Trips AS T ON AR.TripId = T.Id
		JOIN Rooms AS R ON T.RoomId = R.Id
		JOIN Hotels AS H ON R.HotelId = H.Id
		Join Cities AS C ON H.CityId = C.Id
WHERE	A.CityId = H.CityId
GROUP BY A.Id, A.Email,C.[Name]
ORDER BY [Trips] DESC,A.Id ASC