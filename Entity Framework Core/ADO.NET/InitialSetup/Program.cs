namespace InitialSetup
{
    using System;
    using Microsoft.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                string databaseCreateCommand = "CREATE DATABASE MinionsDB";
                string[] createTableCommand = GetTableStatements();
                for (int i = 0; i < createTableCommand.Length; i++)
                {
                    ExecuteNonQuery(createTableCommand[i], connection);
                }

                string[] insertStatements = GetInsertStatements();
                foreach (var query in insertStatements)
                {
                    ExecuteNonQuery(query, connection);
                }
            }
        }

        private static void ExecuteNonQuery(string command, SqlConnection connection)
        {
            using (SqlCommand databaseCommand = new SqlCommand(command, connection))
            {
                databaseCommand.ExecuteNonQuery();
            }
        }

        private static string[] GetInsertStatements()
        {
            var result = new string[]
            {
                "INSERT INTO Countries([Name]) VALUES ('Bulgaria'),('United Kingdom'),('Greece'),('Italy'),('USA')",
                "INSERT INTO Towns([Name],CountryCode) VALUES ('Sofia',1),('Liverpool',2),('Athens',3),('Rome',4),('Boston',5)",
                "INSERT INTO EvilnessFactors([Name]) VALUES ('Super good'),('Good'),('Bad'),('Evil'),('Super evil')",
                "INSERT INTO Minions([Name],Age,TownId) VALUES ('Georgi',26,1),('John',22,2),('Vasilis',67,3),('Pietro',24,4),('Michael',26,5)",
                "INSERT INTO Villains([Name],EvilnessFactorId) VALUES ('Ivan',1),('Zoran',2),('Dimitrakis',3),('Alessandro',4),('Bethany',5)",
                "INSERT INTO MinionsVillains(MinionId,VillainId) VALUES (1,1),(2,2),(3,3),(4,4),(5,5)"
            };

            return result;
        }
        private static string[] GetTableStatements()
        {
            var result = new string[]
            {
                "CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL)",
                "CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL,CountryCode INT REFERENCES Countries(Id))",
                "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL)",
                "CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL,Age INT Not Null,TownId INT REFERENCES Towns(Id))",
                "CREATE TABLE Villains(Id INT PRIMARY KEY IDENTITY, [Name] NVARCHAR(50) NOT NULL,EvilnessFactorId INT REFERENCES EvilnessFactors(Id))",
                "CREATE TABLE MinionsVillains(MinionId INT References Minions(Id), VillainId INT REFERENCES Villains(Id) PRIMARY KEY(MinionId,VillainId))"
            };

            return result;
        }
    }
}
