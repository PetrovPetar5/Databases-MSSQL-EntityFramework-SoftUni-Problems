CREATE TABLE Passports(
PassportID INT NOT NULL PRIMARY KEY,
PassportNumber NVARCHAR(10) UNIQUE NOT NULL 
)

INSERT INTO Passports
VALUES
		(101,'N34FG21B'),
		(102,'K65LO4R7'),
		(103,'ZE657QP2')

	CREATE TABLE Persons(
	PersonID INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(15) NOT NULL,
	Salary DECIMAL(8,2) NOT NULL,
	PassportID INT NOT NULL UNIQUE REFERENCES Passports(PassportID)
	)

	INSERT INTO Persons (FirstName,Salary,PassportID)
	VALUES 
			('Roberto',43300.00,102),
			('Tom',56100,103),
			('Yana',60200,101)

