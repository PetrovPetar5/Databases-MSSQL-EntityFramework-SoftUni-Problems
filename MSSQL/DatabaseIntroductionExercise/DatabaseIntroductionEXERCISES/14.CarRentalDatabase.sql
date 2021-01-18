CREATE DATABASE CarRental
USE CarRental

CREATE TABLE Categories(
Id INT PRIMARY KEY IDENTITY, 
CategoryName NVARCHAR(35) UNIQUE NOT NULL, 
DailyRate DECIMAL(6,2) NOT NULL, 
WeeklyRate DECIMAL(6,2) NOT NULL, 
MonthlyRate DECIMAL(6,2) NOT NULL, 
WeekendRate DECIMAL(6,2) NOT NULL
)

INSERT INTO Categories(CategoryName,DailyRate,WeeklyRate,MonthlyRate,WeekendRate)
VALUES
		('Cross-Over',10.50,30,99,85),
		('Sport',10,80,20,29),
		('Economic',25,10,35,30)

CREATE TABLE Cars(
Id INT PRIMARY KEY IDENTITY , 
PlateNumber NVARCHAR(10) UNIQUE NOT NULL, 
Manufacturer NVARCHAR(20) NOT NULL, 
Model NVARCHAR(15) NOT NULL, 
CarYear DATETIME2 NOT NULL, 
CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL , 
Doors SMALLINT, 
Picture VARBINARY(MAX), 
Condition NVARCHAR(10), 
Available BIT NOT NULL
)

INSERT INTO Cars(PlateNumber,Manufacturer,Model,CarYear,CategoryId,Available)
VALUES
		('B 0696 HX','VW','GOLF4','1998',3,1),
		('B 0695 HX','Mercedes','S','1998',3,0),
		('CA 0022 HX','Renault','Capture','2014',2,1)

CREATE TABLE Employees(
Id INT PRIMARY KEY IDENTITY, 
FirstName NVARCHAR(10) NOT NULL, 
LastName NVARCHAR(10)NOT NULL, 
Title NVARCHAR(30), 
Notes NTEXT
)

INSERT INTO Employees(FirstName,LastName)
VALUES
		('Petar','Petrov'),
		('Ivan','Stoyanov'),
		('Ilina','Todorova')

CREATE TABLE Customers(
Id INT PRIMARY KEY IDENTITY, 
DriverLicenceNumber NVARCHAR(20) UNIQUE NOT NULL, 
FullName NVARCHAR(30) NOT NULL, 
[Address] NVARCHAR(50) NOT NULL, 
City NVARCHAR(20) NOT NULL, 
ZIPCode NVARCHAR(10) NOT NULL, 
Notes NTEXT
)

INSERT INTO Customers(DriverLicenceNumber,FullName,[Address],City,ZIPCode)
VALUES
		('64321222','Petar Ivanov Petrov','Evlogi Gerogiev 18 fl.2 ap.7','Varna','9000'),
		('64121222','Petar Ivanov Petrov','Evlogi Gerogiev 18 fl.2 ap.7','Varna','9000'),
		('64321221','Petar Ivanov Petrov','Evlogi Gerogiev 18 fl.2 ap.7','Varna','9000')

CREATE TABLE RentalOrders(
Id INT PRIMARY KEY IDENTITY,
EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL, 
CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL, 
CarId INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL, 
TankLevel DECIMAL(5,2) NOT NULL, 
KilometrageStart DECIMAL(9,2) NOT NULL, 
KilometrageEnd DECIMAL(9,2) NOT NULL, 
TotalKilometrage DECIMAL(9,2), 
StartDate DATETIME2 NOT NULL, 
EndDate DATETIME2 NOT NULL, 
TotalDays SMALLINT, 
RateApplied DECIMAL(6,2) NOT NULL, 
TaxRate DECIMAL(6,2) NOT NULL, 
OrderStatus BIT NOT NULL, 
Notes NTEXT
)

INSERT INTO RentalOrders(EmployeeId,CustomerId,CarId,TankLevel,KilometrageStart,
KilometrageEnd,StartDate,EndDate,RateApplied,TaxRate,OrderStatus)
VALUES
		(1,1,1,30,1023.77,2000,'10-02-2020','10-05-2020',10,2,1),
		(2,2,2,30,1023.77,2000,'10-02-2020','10-05-2020',10,2,1),
		(3,3,3,30,1023.77,2000,'10-02-2020','10-05-2020',10,2,1)



