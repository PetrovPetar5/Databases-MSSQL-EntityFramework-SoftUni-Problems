CREATE TABLE People(
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(200) NOT NULL,
Picture IMAGE,
Height DECIMAL(3,2),
[Weight] DECIMAL(5,2),
Gender CHAR(1) NOT NULL,
Birthdate DATE NOT NULL,
Biography NVARCHAR(MAX)
)

INSERT INTO People([Name],Picture,Height,[Weight],Gender,Birthdate,Biography)
VALUES
		('Petar Petrov',null,1.83,83.25,'m','1995/10/02','Works as accountant at Immedis.'),
		('Slavin Slavov',null,1.77,85,'m','1995/02/02','Student who studies for mechanic'),
		('Georgi Shopov',null,1.86,86,'m','1995/08/23','Works in a factory'),
		('Krasimir Kurtev',null,1.97,115.25,'m','1995/10/22',' Very kind and funny taxi driver'),
		('Genadi Atanasov',null,1.67,79.25,'m','1995/02/01','Works as a security guard in the local casino')

