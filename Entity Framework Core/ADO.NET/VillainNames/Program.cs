namespace VillainNames
{
    using System;
    using Microsoft.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            string commandString = @"SELECT V.[Name],COUNT(*) AS MinionsCount FROM MinionsVillains AS MV
JOIN Villains as V ON MV.VillainId = V.Id
GROUP BY V.[Name]
HAVING COUNT(*) > 3
ORDER BY COUNT(*) DESC";
            using var command = new SqlCommand(commandString, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var villainName = reader[0];
                var minionsCount = reader[1];
                Console.WriteLine($"{villainName} - {minionsCount}");
            }
        }
    }
}
