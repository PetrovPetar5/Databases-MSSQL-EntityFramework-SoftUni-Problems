CREATE PROC usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
BEGIN
	DECLARE @HotelId INT = (
								SELECT R.HotelId FROM Trips AS T
								JOIN Rooms AS R ON R.Id = T.RoomId
								WHERE T.Id = @TripId);

	DECLARE @TargetRoomHotelId INT = (
									SELECT H.Id FROM ROOMS AS R
									JOIN HOTELS AS H ON R.HotelId = H.Id
									WHERE R.Id = @TargetRoomId);

	IF (@HotelId <> @TargetRoomHotelId)
		THROW 50001,'Target room is in another hotel!',1;

	DECLARE @BedsNeeded INT =	(SELECT COUNT(*) FROM AccountsTrips GROUP BY TripId HAVING TripId=@TripId);

	DECLARE @DesiredRoomBeds INT = (SELECT R.Beds FROM ROOMS AS R  WHERE R.Id = @TargetRoomId);

	IF ( @BedsNeeded > @DesiredRoomBeds)
	THROW 50002,'Not enough beds in target room!',1;

	UPDATE Trips
	SET RoomId = @TargetRoomId
	WHERE Id= @TripId;

END
GO