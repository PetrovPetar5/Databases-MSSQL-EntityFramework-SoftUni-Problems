SELECT TownID, [Name] FROM Towns
WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name] ASC

SELECT TownID, [Name] FROM Towns
WHERE LEFT([Name],1) IN ('M', 'K', 'E','B')
ORDER BY [Name] ASC

SELECT TownID, [Name] FROM Towns
WHERE SUBSTRING([Name],1,1) IN ('M', 'K', 'E','B')
ORDER BY [Name] ASC
