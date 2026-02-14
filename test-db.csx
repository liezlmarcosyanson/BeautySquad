#!/usr/bin/env dotnet-script

#r "nuget: System.Data.SqlClient, 4.8.5"

using System.Data.SqlClient;

string connStr = "Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;Connection Timeout=30;";

try
{
    using (SqlConnection conn = new SqlConnection(connStr))
    {
        conn.Open();
        Console.WriteLine("✓ Connected to BeautySquadDb");
        
        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as TableCount FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo'", conn))
        {
            var result = cmd.ExecuteScalar();
            Console.WriteLine($"✓ Database has {result} tables");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"✗ Error: {ex.Message}");
}
