CREATE OR ALTER FUNCTION [dbo].[ufn_IsWordComprised](@setOfLetters NVARCHAR(MAX), @word NVARCHAR(MAX))
RETURNS BIT AS
BEGIN
	DECLARE @Index INT = 1
	WHILE (@Index <= LEN(@Word)) --SOFIA
	BEGIN
	
		IF (CHARINDEX(SUBSTRING(@Word,@Index,1),@setOfLetters) = 0)
		RETURN 0
		
		SET @Index +=1
	END

	RETURN 1
END
GO