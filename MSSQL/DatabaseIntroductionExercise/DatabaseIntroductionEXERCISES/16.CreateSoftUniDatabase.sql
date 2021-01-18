CREATE DATABASE SoftUni
USE SoftUni

CREATE TABLE Towns(
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(30) NOT NULL
)

CREATE TABLE Addresses(
Id INT PRIMARY KEY, 
AddressText NVARCHAR(100), 
TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments(
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(20) Not null
)

CREATE TABLE Employees(
Id INT PRIMARY KEY IDENTITY, 
FirstName NVARCHAR(10) NOT NULL, 
MiddleName NVARCHAR(10), 
LastName NVARCHAR(10) NOT NULL, 
JobTitle NVARCHAR(20) NOT NULL, 
DepartmentId INT FOREIGN KEY REFERENCES Departments(Id) NOT NULL,
 HireDate DATETIME2 NOT NULL, 
 Salary DECIMAL(6,2) NOT NULL, 
 AddressId INT FOREIGN KEY REFERENCES  Addresses(Id)
 )

