
--1. Create Database 
CREATE TABLE Users(
Id INT IDENTITY PRIMARY KEY,
Username VARCHAR(30) NOT NULL,
[Password] VARCHAR(30) NOT NULL,
Email VARCHAR(50) NOT NULL)

CREATE TABLE Repositories(
Id INT IDENTITY PRIMARY KEY,
[Name] VARCHAR(50) NOT NULL)



CREATE TABLE RepositoriesContributors(
RepositoryId INT REFERENCES Repositories(Id) NOT NULL,
ContributorId INT REFERENCES Users(Id) NOT NULL
PRIMARY KEY(RepositoryId,ContributorId))


CREATE TABLE Issues(
Id INT IDENTITY PRIMARY KEY,
Title VARCHAR(255) NOT NULL,
IssueStatus CHAR(6) NOT NULL,
RepositoryId INT REFERENCES Repositories(Id) NOT NULL,
AssigneeId INT REFERENCES Users(Id) NOT NULL)


CREATE TABLE Commits(
Id INT IDENTITY PRIMARY KEY,
Message VARCHAR(255) NOT NULL,
IssueId INT REFERENCES Issues(Id) NOT NULL,
RepositoryId INT REFERENCES Repositories(Id) NOT NULL,
ContributorId INT REFERENCES Users(Id) NOT NULL)

CREATE TABLE Files(
Id INT IDENTITY PRIMARY KEY,
[Name] VARCHAR(100) NOT NULL,
Size DECIMAL(18,2) NOT NULL,
ParentId INT REFERENCES Files(Id),
CommitId INT REFERENCES Commits(Id) NOT NULL)

--2. Insert
INSERT INTO Files ([Name],Size,ParentId,CommitId)
VALUES
		('Trade.idk',2598.0 ,1 ,1 ),
		('menu.net', 9238.31,2, 2),
		('Administrate.soshy', 1246.93, 3,3 ),
		('Controller.php',7353.15 , 4,4 ),
		('Find.java',9957.86 ,5 , 5),
		('Controller.json',14034.87 ,3 ,6 ),
		('Operate.xix',7662.92 ,7 ,7 )

		INSERT INTO Issues (Title,IssueStatus,RepositoryId,AssigneeId)
VALUES
		('Critical Problem with HomeController.cs file','open',1 ,4),
		('Typo fix in Judge.html','open',4 ,3),
		('Implement documentation for UsersService.cs','closed',8 ,2),
		('Unreachable code in Index.cs','open', 9,8)

--3.Update
UPDATE Issues
		SET IssueStatus = 'closed'
		WHERE AssigneeId = 6
	
	--4.Delete
		DELETE FROM Issues
WHERE RepositoryId = (SELECT Id FROM Repositories WHERE Repositories.[Name] LIKE 'Softuni-Teamwork')

DELETE FROM RepositoriesContributors
WHERE RepositoryId = (SELECT Id FROM Repositories WHERE Repositories.[Name] LIKE 'Softuni-Teamwork')

--5.Commits
SELECT Id,[Message],RepositoryId,ContributorId from Commits
ORDER BY Id ASC, [Message] ASC, RepositoryId ASC, ContributorId ASC	

--6.FrontÅnd
SELECT Id,[Name],Size FROM Files AS F
WHERE F.Size > 1000 AND F.[Name] LIKE '%html%'
ORDER BY F.Size DESC, F.Id ASC, F.[Name] ASC

--7.Issue Assignment
SELECT I.Id,Concat(U.Username,' ', ':',' ' ,I.Title) AS IssueAssignee FROM Issues AS I
JOIN Users AS U ON I.AssigneeId = U.Id
ORDER BY I.Id DESC, I.AssigneeId ASC

--8.Single Files
SELECT F.Id,F.[Name], CONCAT(F.Size,'KB') AS Size FROM Files AS F
LEFT JOIN Files AS FI ON F.Id = FI.ParentId
WHERE FI.ParentId IS NULL
ORDER BY F.Id ASC, F.[Name] ASC,F.Size DESC

--9Commits in Repositories
SELECT TOP(5) R.Id,R.[Name], COUNT(*) AS Commits FROM RepositoriesContributors AS RC
  JOIN Repositories AS R ON RC.RepositoryId = R.Id
  JOIN Commits AS C ON RC.RepositoryId = C.RepositoryId
GROUP BY R.Id, R.[Name]
ORDER BY Commits DESC, R.Id ASC, R.[Name] ASC

--10.Average Size
SELECT U.Username, AVG(F.size) AS Size  FROM Commits AS C 
JOIN Users AS U  ON C.ContributorId = U.Id
JOIN Files AS F ON F.CommitId = C.Id
GROUP BY U.Username,C.ContributorId
order by AVG(F.size) desc, U.Username asc

--11.All User Commits
CREATE FUNCTION [dbo].[udf_AllUserCommits](@username VARCHAR(30))
RETURNS INT
AS
BEGIN
	RETURN (SELECT COUNT(C.Id) FROM Commits AS C
	JOIN Users AS U ON U.Id = C.ContributorId
	WHERE U.Username = @username)
END

--12.Search for Files
CREATE  PROCEDURE  usp_SearchForFiles(@fileExtension VARCHAR(MAX))
AS
	SELECT Id,[Name], CONCAT(Size,'KB') AS Size FROM Files AS F
	WHERE F.[Name] LIKE '%' + @fileExtension + ''
	ORDER BY Id ASC,[Name] ASC,f.Size DESC