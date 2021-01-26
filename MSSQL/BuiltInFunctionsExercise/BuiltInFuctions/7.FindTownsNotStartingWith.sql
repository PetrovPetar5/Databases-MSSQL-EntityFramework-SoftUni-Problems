SELECT TownID, [Name] FROM Towns
WHERE LEFT([Name],1) NOT IN('R','B','D')
ORDER BY [Name] ASC

SELECT TownID, [Name] FROM Towns
WHERE SUBSTRING([Name],1,1) NOT IN('R','B','D')
ORDER BY [Name] ASC

SELECT TownID, [Name] FROM Towns
WHERE [Name] NOT LIKE '[RBD]%'
ORDER BY [Name] ASC