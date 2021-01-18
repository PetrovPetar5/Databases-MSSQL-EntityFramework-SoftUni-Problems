CREATE DATABASE Movies

CREATE TABLE Directors (
Id INT PRIMARY KEY IDENTITY,
DirectorName NVARCHAR(50) NOT NULL,
Notes NTEXT
)

CREATE TABLE Genres(
Id INT PRIMARY KEY IDENTITY,
GenreName NVARCHAR(25) NOT NULL,
Notes NTEXT
)

CREATE TABLE Categories(
Id INT PRIMARY KEY IDENTITY,
CategoryName NVARCHAR(30) NOT NULL,
Notes NTEXT
)


CREATE TABLE Movies(
Id INT PRIMARY KEY IDENTITY,
Title NVARCHAR(50) NOT NULL,
DirectorId INT FOREIGN KEY  REFERENCES Directors(Id) NOT NULL,
CopyrightYear DATETIME2 NOT NULL,
[Length] TIME NOT NULL,
GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
Rating DECIMAL(2,1),
Notes NTEXT
)

INSERT INTO  Directors(DirectorName,Notes)
VALUES
		('Shawn Golden','Has won the best movie award for 2015.'),
		('Steven Spielsberg','Has been nominated for the best director in the past 10 years.'),
		('Georgi Cholakov',null),
		('Petar Dimitrov',null),
		('Asen Baltechki','Tried to be a good director but have not impressed so far.')

INSERT INTO Genres (GenreName,Notes)
VALUES
		('Action','One man army.'),
		('Horror','Bad music taste inside.'),
		('Romantic','True love will happen.'),
		('Documentary','Interesting but not funny.'),
		('Thriller','Someone will die.')

		INSERT INTO Categories(CategoryName)
		VALUES 
				('Best supportive role.'),
				('Best woman performance.'),
				('Best man performance.'),
				('Best movie song.'),
				('Best effects.')

		INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
		VALUES		
				('The lord of the rings',2,'2001','02:13',2,1,5.5,null),
				('American pie',4,'2000','01:23',1,4,3.5,null),
				('List of Shindler',5,'2004','02:13',2,1,6.5,null),
				('The lord of the rings',2,'2001','02:13',2,1,5.5,null),
				('The lord of the rings',2,'2001','02:12',2,1,5.5,null)

