namespace MinionNames
{
    using System;
    using Microsoft.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";
            int id = int.Parse(Console.ReadLine());
            string commandString = @"SELECT Name FROM Villains WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand getsVillainName = new SqlCommand(commandString, connection))
                {
                    getsVillainName.Parameters.AddWithValue("@Id", id);
                    var nameResult = getsVillainName.ExecuteScalar();
                    if (nameResult == null)
                    {
                        Console.WriteLine($"No villain with ID {id} exists in the database.");
                    }
                    else
                    {
                        Console.WriteLine($"Villain: {nameResult}");
                        string villainMinions = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";
                        using (SqlCommand getsVillainMinions = new SqlCommand(villainMinions, connection))
                        {
                            getsVillainMinions.Parameters.AddWithValue("@Id", id);
                            using (var minionsInfo = getsVillainMinions.ExecuteReader())
                            {
                                if (!minionsInfo.HasRows)
                                {
                                    Console.WriteLine("(no minions)");
                                }
                                else
                                {
                                    while (minionsInfo.Read())
                                    {
                                        Console.WriteLine($"{minionsInfo[0]}. {minionsInfo[1]} {minionsInfo[2]}");
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
