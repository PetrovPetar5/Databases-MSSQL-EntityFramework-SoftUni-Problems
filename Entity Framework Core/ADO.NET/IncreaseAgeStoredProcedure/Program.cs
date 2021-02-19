namespace IncreaseAgeStoredProcedure
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
            var minionId = int.Parse(Console.ReadLine());
            var executeProcedureQuery = @"EXECUTE usp_GetOlder @iD";
            using var procCommand = new SqlCommand(executeProcedureQuery, connection);
            procCommand.Parameters.AddWithValue("@iD", minionId);
            procCommand.ExecuteNonQuery();
            var minionNameAgeQuery = @"SELECT Name, Age FROM Minions WHERE Id = @Id";
            using var minionInfo = new SqlCommand(minionNameAgeQuery, connection);
            minionInfo.Parameters.AddWithValue("@Id", minionId);
            using var minions = minionInfo.ExecuteReader();
            while (minions.Read())
            {
                Console.WriteLine($"{minions[0]} – {minions[1]} years old");
            }
        }
    }
}
