CREATE FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
RETURNS NVARCHAR(250)
AS
BEGIN

	DECLARE @RoomId INT =	(
								SELECT TOP(1)	R.Id 
								FROM			Hotels AS H
												JOIN Rooms AS R ON R.HotelId = H.Id
												JOIN  Trips AS T ON R.Id = T.RoomId
								WHERE			H.Id = @HotelId 
												AND 	R.Beds >= @People 
												AND  @Date NOT BETWEEN T.ArrivalDate AND T.ReturnDate 
												AND	T.CancelDate IS NULL
												 AND YEAR(@Date) = YEAR(T.ArrivalDate)
								ORDER BY R.Price DESC
							);
	IF @RoomId IS NULL
	BEGIN
		RETURN 'No rooms available'
	END

	DECLARE @RoomType NVARCHAR(50) = (
										SELECT R.[Type] FROM Rooms AS R
										WHERE R.Id = @RoomId
									 );

	DECLARE @Beds INT =             (
										SELECT R.Beds FROM Rooms AS R
										WHERE R.Id = @RoomId
									);


	DECLARE @RoomPricepPerPerson DECIMAL(18,2) = (
													SELECT R.Price 
													FROM Rooms AS R
													WHERE R.Id =@RoomId
												);

	DECLARE @BaseRate DECIMAL(18,2) =	(
											SELECT H.BaseRate 
											FROM HOTELS AS H
											WHERE H.Id = @HotelId
										);

	DECLARE @TotalPrice DECIMAL (18,2) = (@BaseRate +@RoomPricepPerPerson) * @People;
	DECLARE @Output NVARCHAR(MAX) = CONCAT('Room',' ', @RoomId,':',' ', @RoomType,' (',@Beds,' beds) - $',@TotalPrice);
	RETURN @Output;

END
