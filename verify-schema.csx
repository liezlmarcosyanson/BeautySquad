#r "nuget: System.Data.SqlClient, 4.8.5"
using System.Data.SqlClient;

var connStr = "Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;Connection Timeout=30;";
using (var conn = new SqlConnection(connStr))
{
    conn.Open();
    using (var cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' ORDER BY TABLE_NAME", conn))
    {
        var reader = cmd.ExecuteReader();
        WriteLine("\n✓ Database Schema Verification");
        WriteLine("Tables created:");
        int count = 0;
        while (reader.Read())
        {
            WriteLine($"  {++count}. {reader["TABLE_NAME"]}");
        }
        WriteLine($"\nTotal: {count} tables");
    }
}
