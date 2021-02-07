CREATE TABLE Planets
(
	Id INT IDENTITY PRIMARY KEY ,
	[Name] VARCHAR(30) NOT NULL
)

CREATE TABLE Spaceports
(
	Id INT IDENTITY PRIMARY KEY ,
	[Name] VARCHAR(50) NOT NULL,
	PlanetId INT FOREIGN KEY REFERENCES Planets(Id) NOT NULL
)

CREATE TABLE Spaceships
(
	Id INT IDENTITY PRIMARY KEY ,
	[Name] VARCHAR(50) NOT NULL,
	Manufacturer VARCHAR(30) NOT NULL,
	LightSpeedRate INT DEFAULT 0
)

CREATE TABLE Colonists
(
	Id INT IDENTITY PRIMARY KEY ,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	Ucn VARCHAR(10) UNIQUE NOT NULL,
	BirthDate DATE NOT NULL
)

CREATE TABLE Journeys
(
	Id INT IDENTITY PRIMARY KEY ,
	JourneyStart DATETIME2 NOT NULL,
	JourneyEnd DATETIME2 NOT NULL,
	Purpose VARCHAR(11) CHECK(Purpose IN ('Military','Educational','Technical','Medical')),
	DestinationSpaceportId INT REFERENCES Spaceports(Id) NOT NULL,
	SpaceshipId INT REFERENCES Spaceships(Id) NOT NULL
)

CREATE TABLE TravelCards
(
	Id INT IDENTITY PRIMARY KEY ,
	CardNumber CHAR(10) UNIQUE NOT NULL CHECK(LEN(CardNumber) = 10),
	JobDuringJourney VARCHAR(8) CHECK(JobDuringJourney IN ('Cleaner','Cook','Trooper','Engineer','Pilot')),
	ColonistId INT REFERENCES Colonists(Id) NOT NULL,
	JourneyId INT REFERENCES Journeys(Id) NOT NULL
)










