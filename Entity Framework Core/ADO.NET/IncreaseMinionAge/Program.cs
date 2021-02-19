namespace IncreaseMinionAge
{
    using System;
    using System.Linq;
    using Microsoft.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var idInput = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var increaseMinionAgeQuery = @"UPDATE Minions
   SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
 WHERE Id = @Id";
            for (int i = 0; i < idInput.Length; i++)
            {
                using var increaseMinionsAge = new SqlCommand(increaseMinionAgeQuery, connection);
                increaseMinionsAge.Parameters.AddWithValue("@Id", idInput[i]);
                increaseMinionsAge.ExecuteNonQuery();
            }

            var returnsAllMinionsQuery = @"SELECT Name, Age FROM Minions";
            using var minionsNameAge = new SqlCommand(returnsAllMinionsQuery, connection);
            using var minions = minionsNameAge.ExecuteReader();
            while (minions.Read())
            {
                Console.WriteLine($"{minions[0]} {minions[1]} ");
            }
        }
    }
}
