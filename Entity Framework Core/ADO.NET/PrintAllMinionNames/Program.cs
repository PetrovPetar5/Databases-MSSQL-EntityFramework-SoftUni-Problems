namespace PrintAllMinionNames
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";
            List<string> minions = new List<string>();
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var showAllMinionsQuery = @"SELECT Name FROM Minions";
            using var showMinions = new SqlCommand(showAllMinionsQuery, connection);
            using var result = showMinions.ExecuteReader();
            while (result.Read())
            {
                minions.Add((string)result[0]);
            }

            var counter = 0;
            for (int i = 0; i < minions.Count / 2; i++)
            {
                Console.WriteLine(minions[i]);
                Console.WriteLine(minions[minions.Count - 1 - counter]);
                counter++;
            }

            if (minions.Count % 2 != 0)
            {
                Console.WriteLine(minions[minions.Count / 2]);
            }
        }
    }
}
