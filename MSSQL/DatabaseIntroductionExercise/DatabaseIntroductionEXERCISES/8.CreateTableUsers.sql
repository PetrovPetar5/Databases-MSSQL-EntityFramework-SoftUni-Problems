CREATE TABLE Users(
Id BIGINT PRIMARY KEY IDENTITY, 
Username VARCHAR(30) UNIQUE NOT NULL,
[Password] VARCHAR(26) NOT NULL,
ProfilePicture VARBINARY(MAX) 
CHECK(DATALENGTH(ProfilePicture) <= 900*1024),
LastLoginTime DATETIME2 NOT NULL,
IsDeleted BIT NOT NULL
)

INSERT INTO Users(Username,[Password],ProfilePicture,LastLoginTime,IsDeleted)
VALUES 
		('Dobrogledskiq','Viktoriq02@',null,'2021-01-17 11:33:15',0),
		('Petrovmz','dobrogled02@',null,'2021-01-17 11:13:21',0),
		('petrovpetrov5','lifeIsGood@',null,'2021-01-17 02:33:22',1),
		('giford','goForWin95@',null,'2021-01-17 09:12:11',0),
		('megastore','Ani33@',null,'2021-01-17 21:45:21',1)

