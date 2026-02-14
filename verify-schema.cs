using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        var connStr = "Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;Connection Timeout=30;";
        try
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' ORDER BY TABLE_NAME", conn))
                {
                    var reader = cmd.ExecuteReader();
                    Console.WriteLine("\n✓ Database Schema Verification");
                    Console.WriteLine("Tables created:");
                    int count = 0;
                    while (reader.Read())
                    {
                        Console.WriteLine($"  {++count}. {reader["TABLE_NAME"]}");
                    }
                    Console.WriteLine($"\nTotal: {count} tables");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
