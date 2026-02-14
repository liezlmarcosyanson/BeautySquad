using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using IAT.Infrastructure;

namespace IAT.MigrationRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BeautySquad Entity Framework Migration Runner ===");
            Console.WriteLine();

            try
            {
                // Get connection string from environment or use default
                string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") 
                    ?? "Server=sql-server,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;";

                Console.WriteLine($"Connection String: {MaskConnectionString(connectionString)}");
                Console.WriteLine();

                // Build connection to master database first to create the target database if needed
                var masterConnectionBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                string masterDb = masterConnectionBuilder.InitialCatalog;
                masterConnectionBuilder.InitialCatalog = "master";
                string masterConnectionString = masterConnectionBuilder.ConnectionString;

                Console.WriteLine("Creating database if not exists...");
                using (var masterConnection = new System.Data.SqlClient.SqlConnection(masterConnectionString))
                {
                    masterConnection.Open();
                    string createDbCommand = $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{masterDb}') 
                    BEGIN
                        CREATE DATABASE [{masterDb}]
                    END";
                    
                    using (var cmd = new System.Data.SqlClient.SqlCommand(createDbCommand, masterConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine($"✓ Database '{masterDb}' ready");
                }
                Console.WriteLine();

                // Now create DbContext with the actual connection string
                Console.WriteLine("Creating database context...");
                using (var context = new AppDbContext(connectionString))
                {
                    Console.WriteLine("✓ Database context created");
                    Console.WriteLine();

                    // Run migrations
                    Console.WriteLine("Running Entity Framework migrations...");
                    var migrator = new DbMigrator(new IAT.Infrastructure.Migrations.Configuration(), context);
                    var pendingMigrations = migrator.GetPendingMigrations();

                    if (pendingMigrations != null && pendingMigrations.Count() > 0)
                    {
                        Console.WriteLine($"Found {pendingMigrations.Count()} pending migration(s):");
                        foreach (var migration in pendingMigrations)
                        {
                            Console.WriteLine($"  - {migration}");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Applying migrations...");
                        migrator.Update();
                        Console.WriteLine("✓ Migrations applied successfully");
                    }
                    else
                    {
                        Console.WriteLine("No pending migrations found. Database is up to date.");
                    }

                    // Verify schema tables exist
                    Console.WriteLine();
                    Console.WriteLine("Verifying database schema...");
                    using (var cmd = context.Database.Connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo'";
                        if (context.Database.Connection.State == System.Data.ConnectionState.Closed)
                        {
                            context.Database.Connection.Open();
                        }
                        var tableCount = (int)cmd.ExecuteScalar();
                        Console.WriteLine($"✓ Database contains {tableCount} tables");
                        
                        if (tableCount == 0)
                        {
                            Console.WriteLine("⚠ Warning: No tables found. Attempting to execute migrations...");
                            migrator.Update();
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("=== Migration Complete ===");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Error during migration:");
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine("Details:");
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
                Environment.Exit(1);
            }
        }

        static string MaskConnectionString(string connectionString)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                connectionString, 
                @"(Password=)[^ ;]*", 
                "$1***",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );
        }
    }
}
