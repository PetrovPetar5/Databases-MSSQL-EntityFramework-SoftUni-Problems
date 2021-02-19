namespace RemoveVillain
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
            var villainIdInput = int.Parse(Console.ReadLine());
            var villainIdQuery = @"SELECT Name FROM Villains WHERE Id = @villainId";
            using var villainId = new SqlCommand(villainIdQuery, connection);
            villainId.Parameters.AddWithValue("@villainId", villainIdInput);
            string? name = (string)villainId.ExecuteScalar();
            if (name == null)
            {
                Console.WriteLine("No such villain was found.");
                return;
            }

            var deleteMinionsQuery = @"DELETE FROM MinionsVillains 
      WHERE VillainId = @villainId";
            using var deleteMinions = new SqlCommand(deleteMinionsQuery, connection);
            deleteMinions.Parameters.AddWithValue("@villainId", villainIdInput);
            var affectedRows = deleteMinions.ExecuteNonQuery();
            var deleteVillainQuery = @"DELETE FROM Villains
      WHERE Id = @villainId";
            using var deleteVillain = new SqlCommand(deleteVillainQuery, connection);
            deleteVillain.Parameters.AddWithValue("@villainId", villainIdInput);
            deleteVillain.ExecuteNonQuery();
            Console.WriteLine($"{name} was deleted.");
            Console.WriteLine($"{affectedRows} minions were released.");
        }
    }
}
