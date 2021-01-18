CREATE DATABASE Hotel
USE Hotel

CREATE TABLE Employees(
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(10) NOT NULL,
LastName NVARCHAR(10) NOT NULL, 
Title NVARCHAR(20), 
Notes NTEXT
)

INSERT INTO Employees(FirstName,LastName)
VALUES
		('Viktoriq','Chocheva'),
		('Miglena','Zheleva'),
		('Neli','Arnaudova')

	CREATE TABLE Customers(
	AccountNumber NVARCHAR(40) PRIMARY KEY, 
	FirstName NVARCHAR(10) NOT NULL, 
	LastName NVARCHAR(10) NOT NULL,
	PhoneNumber NVARCHAR(15) NOT NULL, 
	EmergencyName NVARCHAR(15) NOT NULL, 
	EmergencyNumber NVARCHAR(15) NOT NULL, 
	Notes NTEXT
	)

	INSERT INTO Customers(AccountNumber,FirstName,LastName,PhoneNumber,EmergencyName,EmergencyNumber)
	VALUES
			('BG20392223131','Milena','Mateeva','0896180135','Stoqn Jelev','0782211221'),
			('BG203922231211','Qnita','Yordanova','0896280135','Georgi Jelev','0782211221'),
			('BG20392223931','Qna','Panchveva','0894180135','Kolio Jelev','0897212112')

			CREATE TABLE RoomStatus(
			RoomStatus NVARCHAR(20) PRIMARY KEY,
			Notes NTEXT)

			INSERT INTO RoomStatus(RoomStatus)
			VALUES 
					('Clean'),
					('Needs cleaning'),
					('Occupied')

CREATE TABLE RoomTypes(
RoomType NVARCHAR(20) PRIMARY KEY, 
Notes NTEXT
)

INSERT INTO RoomTypes(RoomType)
VALUES
		('Apartment'),
		('Single'),
		('Family')

CREATE TABLE BedTypes(
BedType NVARCHAR(10) PRIMARY KEY, 
Notes NTEXT
)

INSERT INTO BedTypes(BedType)
VALUES
		('King'),
		('Queen'),
		('Coach')

CREATE TABLE Rooms(
RoomNumber SMALLINT PRIMARY KEY,
RoomType NVARCHAR(20) FOREIGN KEY REFERENCES RoomTypes(RoomType) NOT NULL, 
BedType NVARCHAR(10) FOREIGN KEY REFERENCES BedTypes(BedType) NOT NULL, 
Rate DECIMAL(2,1), 
RoomStatus NVARCHAR(20) FOREIGN KEY REFERENCES RoomStatus(RoomStatus) NOT NULL, 
Notes NTEXT
)

INSERT INTO Rooms(RoomNumber,RoomType,BedType,RoomStatus)
VALUES
		(10,'Single','King','Clean'),
		(11,'Apartment','Queen','Clean'),
		(12,'Family','King','Occupied')

CREATE TABLE Payments(
Id INT PRIMARY KEY IDENTITY, 
EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL, 
PaymentDate DATETIME2 NOT NULL, 
AccountNumber NVARCHAR(40) FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL , 
FirstDateOccupied DATETIME2 NOT NULL, 
LastDateOccupied DATETIME2 NOT NULL, 
TotalDays AS DATEDIFF(DAY,FirstDateOccupied, LastDateOccupied), 
AmountCharged DECIMAL(6,2) NOT NULL, 
TaxRate DECIMAL(2,1) NOT NULL, 
TaxAmount AS AmountCharged *TaxRate , 
PaymentTotal DECIMAL(7,2) NOT NULL,
Notes NTEXT
)

INSERT INTO Payments(EmployeeId,PaymentDate,AccountNumber,FirstDateOccupied,
LastDateOccupied,AmountCharged,TaxRate,PaymentTotal)
VALUES
		(1,'10-01-2020','BG20392223131','07-01-2020','09-01-2020',100,2,200),
		(1,'10-01-2020','BG203922231211','07-01-2020','09-01-2020',100,2,200),
		(1,'10-01-2020','BG20392223931','07-01-2020','09-01-2020',100,2,200)

		CREATE TABLE 	Occupancies (
		Id INT PRIMARY KEY IDENTITY, 
		EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL, 
		DateOccupied DATETIME2 NOT NULL, 
		AccountNumber NVARCHAR(40) FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL, 
		RoomNumber SMALLINT REFERENCES Rooms(RoomNumber) NOT NULL, 
		RateApplied DECIMAL(2,1), 
		PhoneCharge DECIMAL(6,2), 
		Notes NTEXT
		)

		INSERT INTO Occupancies(EmployeeId,DateOccupied,AccountNumber,RoomNumber)
		VALUES
				(1,'02-15-2020','BG20392223131',10),
				(2,'03-02-2020','BG203922231211',11),
				(3,'11-02-2020','BG20392223931',12)