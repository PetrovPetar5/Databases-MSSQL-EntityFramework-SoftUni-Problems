namespace ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string country = Console.ReadLine();
                string citiesQuery = @"UPDATE Towns
   SET Name = UPPER(Name)
 WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";
                using (SqlCommand updateCities = new SqlCommand(citiesQuery, connection))
                {
                    updateCities.Parameters.AddWithValue("@countryName", country);
                    var updatedCities = updateCities.ExecuteNonQuery();
                    if (updatedCities == 0)
                    {
                        Console.WriteLine("No town names were affected.");
                    }
                    else
                    {
                        string citiesAffected = @"SELECT t.Name 
   FROM Towns as t
   JOIN Countries AS c ON c.Id = t.CountryCode
  WHERE c.Name = @countryName";
                        using (SqlCommand affectedCities = new SqlCommand(citiesAffected, connection))
                        {
                            affectedCities.Parameters.AddWithValue("@countryName", country);
                            using (var result = affectedCities.ExecuteReader())
                            {
                                List<string> countries = new List<string>();
                                while (result.Read())
                                {
                                    countries.Add(result[0].ToString());
                                }

                                Console.WriteLine($"{updatedCities} town names were affected.");
                                Console.WriteLine($"[{ String.Join(", ", countries)}]");
                            }
                        }
                    }
                }
            }
        }
    }
}
