CREATE FUNCTION ufn_GetSalaryLevel (@Salary DECIMAL(18,4))
RETURNS NVARCHAR(50)
BEGIN
	IF @Salary < 30000
	RETURN 'Low'
	ELSE IF @Salary > 50000
	RETURN 'High'
	RETURN 'Average'
END