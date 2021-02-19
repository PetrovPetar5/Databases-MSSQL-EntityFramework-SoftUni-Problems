namespace AddMinion
{
    using System;
    using Microsoft.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var minionInfo = Console.ReadLine().Split(' ');
                var minionName = minionInfo[1];
                var minionAge = int.Parse(minionInfo[2]);
                var townName = minionInfo[3];
                var townId = GetTownId(connection, townName);
                if (townId == null)
                {
                    string insertTownQuery = @"INSERT INTO Towns (Name) VALUES (@townName)";
                    using (SqlCommand insertTown = new SqlCommand(insertTownQuery, connection))
                    {
                        insertTown.Parameters.AddWithValue("@townName", townName);
                        insertTown.ExecuteNonQuery();
                    }

                    Console.WriteLine($"Town {townName} was added to the database.");
                }

                object? minionId = GetMinionId(connection, minionName);
                    if (minionId == null)
                    {
                        string insertMinionQuery = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@nam, @age, @townId)";
                        using (SqlCommand insertMinion = new SqlCommand(insertMinionQuery, connection))
                        {

                            insertMinion.Parameters.AddWithValue("@nam", minionName);
                            insertMinion.Parameters.AddWithValue("@age", minionAge);
                            insertMinion.Parameters.AddWithValue("@townId", GetTownId(connection, townName));
                            insertMinion.ExecuteNonQuery();
                        }
                    }

                var villainInput = Console.ReadLine().Split(' ');
                var villainName = villainInput[1];
                var villainId = GetVillainId(connection, villainName);
                if (villainId == null)
                {
                    var insertVillainQuery = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
                    using (SqlCommand insertVillain = new SqlCommand(insertVillainQuery, connection))
                    {
                        insertVillain.Parameters.AddWithValue("@villainName", villainName);
                        insertVillain.ExecuteNonQuery();
                        Console.WriteLine($"Villain {villainName} was added to the database.");
                    }
                }

                string insertMinionToVillainQuery = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";
                using (SqlCommand insertMinionToVillain = new SqlCommand(insertMinionToVillainQuery, connection))
                {
                    insertMinionToVillain.Parameters.AddWithValue("@villainId", GetVillainId(connection, villainName));
                    insertMinionToVillain.Parameters.AddWithValue("@minionId", GetMinionId(connection, minionName));
                    insertMinionToVillain.ExecuteNonQuery();
                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                }
            }
        }

        private static object GetTownId(SqlConnection connection, string townName)
        {
            var townIdQuery = @"SELECT Id FROM Towns WHERE Name = @townName";
            using (SqlCommand townIdCommand = new SqlCommand(townIdQuery, connection))
            {
                townIdCommand.Parameters.AddWithValue("@townName", townName);
                object? townId = townIdCommand.ExecuteScalar();

                return townId;
            }
        }

        private static object GetVillainId(SqlConnection connection, string villainName)
        {
            var villainIdQuery = @"SELECT Id FROM Villains WHERE Name = @Name";
            using (SqlCommand villain = new SqlCommand(villainIdQuery, connection))
            {
                villain.Parameters.AddWithValue("@Name", villainName);
                object? villainId = villain.ExecuteScalar();

                return villainId;
            }
        }

        private static object GetMinionId(SqlConnection connection, string minionName)
        {
            var minionIdQuery = @"SELECT Id FROM Minions WHERE Name = @Name";
            using (SqlCommand minionIdCommand = new SqlCommand(minionIdQuery, connection))
            {
                minionIdCommand.Parameters.AddWithValue("@Name", minionName);
                object? minionId = minionIdCommand.ExecuteScalar();

                return minionId;
            }
        }
    }
}
