CREATE OR ALTER FUNCTION ufn_CalculateFutureValue (@Sum DECIMAL(18,4), @Interest FLOAT, @Years INT)
RETURNS DECIMAL(18,4)
AS
BEGIN
	DECLARE @YearsCount INT = 1;

	WHILE (@YearsCount <= @Years)
	BEGIN
		SET	@Sum = @Sum * (1 +@Interest);
		SET @YearsCount +=1;
	END

RETURN @Sum
END
GO
SELECT dbo.ufn_CalculateFutureValue (123.12,0.1,5)