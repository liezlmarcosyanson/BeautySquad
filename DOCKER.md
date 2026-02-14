# BeautySquad - Docker Setup & Migration Guide

This guide explains how to set up the BeautySquad API with Docker, run database migrations, and test the setup.

## Prerequisites

### Required
- Docker Desktop (Windows/Mac) or Docker Engine (Linux)
- Docker Compose
- .NET Framework SDK 4.8 (for running migrations locally)

### Optional
- SQL Server Management Studio (for manual database inspection)
- Postman or curl (for API testing)

## Quick Start

### 1. Start the Database Container

```bash
bash docker-setup.sh
```

This script will:
1. ✓ Verify Docker is installed
2. ✓ Stop any existing containers (optional)
3. ✓ Start SQL Server in a Docker container
4. ✓ Wait for SQL Server to be ready (30-60 seconds)
5. ✓ Create the BeautySquadDb database
6. ✓ Attempt to run migrations (if .NET is available)
7. ✓ Test database connectivity

**Expected Output:**
```
✓ Docker found
✓ Docker Compose found
✓ SQL Server container started
✓ SQL Server is ready
✓ Database created
✓ Migrations completed successfully  (if dotnet available)
✓ Database connectivity test passed

Setup Complete!
Database Connection:
  • Server: localhost:1433
  • Username: sa
  • Password: BeautySquad@123!
  • Database: BeautySquadDb
```

### 2. Verify Migrations

Run the migration test script:

```bash
bash docker-migrate.sh
```

This script will:
1. ✓ Check Docker connectivity
2. ✓ Verify SQL Server container is running
3. ✓ Test database connection
4. ✓ Check if BeautySquadDb database exists
5. ✓ Build migration runner
6. ✓ Execute migrations
7. ✓ Verify tables were created

**Expected Output:**
```
=== BeautySquad Database Migration Tests ===

Testing: Docker connectivity
✓ Docker is running

Testing: SQL Server container
✓ SQL Server container is running

Testing: SQL Server connectivity
✓ SQL Server is accessible

Testing: BeautySquadDb database
✓ BeautySquadDb database exists

Testing: Building migration runner
✓ Migration runner built successfully

Testing: Executing database migrations
✓ Migrations executed successfully

=== Verifying Migration Results ===

Testing: Checking for EF Core migration history
✓ EF Core migration history table found

Testing: Table count
✓ Found 14 tables in database

=== Test Summary ===

Testing: Final connectivity check
✓ Database is healthy and accessible

✓ All migration tests passed!
```

### 3. Test the API

Once migrations are complete, run the API:

```bash
cd src/IAT.WebApi
dotnet run
```

Then test endpoints:

```bash
bash docker-test.sh
```

This script will:
1. ✓ Check API health (Swagger)
2. ✓ Test all CRUD endpoints
3. ✓ Test error handling
4. ✓ Verify database connectivity
5. ✓ Run performance checks

## Docker Compose Setup

### Services

The `docker-compose.yml` defines:

**sql-server**
- Image: SQL Server 2019
- Container: beautysquad-sqlserver
- Port: 1433 (mapped to localhost)
- Credentials: sa / BeautySquad@123!
- Database: BeautySquadDb
- Volume: sqlserver-data (persistent)
- Health Check: SQL query every 10 seconds

### Connection String

Update `Web.config` to use Docker database:

```xml
<add name="IATConnection" 
     connectionString="Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;Connection Timeout=30;" 
     providerName="System.Data.SqlClient" />
```

For Docker-to-Docker communication (if app is in container):

```xml
<add name="IATConnection" 
     connectionString="Server=sql-server,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;Connection Timeout=30;" 
     providerName="System.Data.SqlClient" />
```

## Manual Database Operations

### Connect to Database Container

```bash
docker exec -it beautysquad-sqlserver sqlcmd -S localhost -U sa -P 'BeautySquad@123!'
```

### Query Database

```bash
docker exec beautysquad-sqlserver sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -d BeautySquadDb -Q "SELECT COUNT(*) AS TableCount FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo'"
```

### Backup Database

```bash
docker exec beautysquad-sqlserver sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q "BACKUP DATABASE [BeautySquadDb] TO DISK = '/var/opt/mssql/backup/BeautySquadDb.bak'"
```

### View Container Logs

```bash
# All services
docker-compose logs -f

# Just SQL Server
docker-compose logs -f sql-server

# Last 100 lines
docker-compose logs --tail=100
```

## Files Created

### Docker Configuration

- **docker-compose.yml** - Defines SQL Server service
- **Dockerfile** - Builds migration runner image
- **.dockerignore** - Excludes files from Docker context

### Scripts

- **docker-setup.sh** - Starts containers and runs migrations
- **docker-migrate.sh** - Tests migrations and verifies setup
- **docker-test.sh** - Tests API endpoints
- **entrypoint.ps1** - Container startup script (for future API container)

### Database

- **scripts/init-db.sql** - Initial database setup script

### Migration Runner

- **src/IAT.MigrationRunner/** - Standalone migration runner
  - Program.cs - Migration execution logic
  - IAT.MigrationRunner.csproj - Project file

## Running Migrations

### Method 1: Automatic (during docker-setup.sh)

```bash
bash docker-setup.sh
```

The setup script will automatically run migrations if .NET SDK is available.

### Method 2: Manual with dotnet CLI

```bash
# Build the migration runner
dotnet build src/IAT.MigrationRunner/IAT.MigrationRunner.csproj -c Release

# Set connection string
export DATABASE_CONNECTION_STRING="Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;"

# Run migrations
dotnet run -p src/IAT.MigrationRunner -c Release
```

### Method 3: Manual with Visual Studio

1. Open Package Manager Console
2. Set Default Project to `IAT.Infrastructure`
3. Run: `Update-Database`

### Method 4: Via Docker Container

```bash
docker run --network beautysquad-network \
  -e DATABASE_CONNECTION_STRING="Server=sql-server,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;" \
  beautysquad-migrator:latest
```

## Database Verification

After migration, verify tables were created:

```bash
docker exec beautysquad-sqlserver sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -d BeautySquadDb -Q "
SET NOCOUNT ON
SELECT name FROM sys.objects WHERE type = 'U' ORDER BY name
GO
"
```

Expected tables:
```
Approval
Campaign
CampaignDeliverable
Collaboration
ContentSubmission
ContentVersion
Influencer
InfluencerTag
PerformanceMetric
Role
SocialProfile
Tag
User
UserRole
```

## Troubleshooting

### SQL Server Container Won't Start

**Symptom:** Container exits immediately

**Solution:**
```bash
# Check logs
docker-compose logs sql-server

# Remove and recreate
docker-compose down -v
bash docker-setup.sh
```

**Common Issues:**
- Port 1433 already in use
- Not enough system memory
- Docker daemon not running

### Migrations Fail

**Symptom:** Migration runner shows errors

**Solution:**
1. Check database connection string
2. Verify SQL Server is running
3. Check SQL Server logs

```bash
# View detailed error
dotnet run -p src/IAT.MigrationRunner -c Release
```

### Cannot Connect from Application

**Symptom:** Application fails to connect to database

**Check:**
1. Connection string in Web.config
2. Firewall rules (if connecting from another machine)
3. SQL Server credentials

```bash
# Test connection
docker exec beautysquad-sqlserver sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q 'SELECT 1'
```

### Permission Denied Errors

**Solution:** Run docker commands with sudo (Linux) or in admin mode (Windows)

```bash
# Linux
sudo bash docker-setup.sh

# Windows PowerShell
# Run as Administrator
```

## Performance Tips

### Improve Migration Speed

1. **Pre-create database:**
```bash
docker exec beautysquad-sqlserver sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q "CREATE DATABASE [BeautySquadDb]"
```

2. **Build migration runner first:**
```bash
dotnet build src/IAT.MigrationRunner -c Release
```

3. **Use environment variable for connection string:**
```bash
# Avoids recompiling
export DATABASE_CONNECTION_STRING="Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;"
```

### Database Performance

1. **Persistent volume:** Already enabled in docker-compose.yml
2. **Memory limit:** Adjust if needed in docker-compose.yml
3. **Backup strategy:** Regular backups recommended for production

## Cleanup

### Stop All Services

```bash
docker-compose down
```

### Stop and Remove Data

```bash
docker-compose down -v
```

### Remove Docker Images

```bash
docker rmi beautysquad-migrator
docker rmi mcr.microsoft.com/mssql/server:2019-latest
```

## Useful Commands Reference

```bash
# View running containers
docker ps

# View all containers (including stopped)
docker ps -a

# Start services
docker-compose up -d

# Stop services
docker-compose down

# View logs
docker-compose logs -f

# Execute command in container
docker exec beautysquad-sqlserver sqlcmd ...

# Rebuild images
docker-compose up -d --build

# Clean system
docker system prune -a
```

## Production Deployment Notes

⚠️ **Not for production use as-is.** For production:

1. **Security:**
   - Change default password
   - Use environment variables for secrets
   - Enable encryption
   - Restrict network access

2. **Persistence:**
   - Use managed database (Azure SQL, AWS RDS)
   - Implement backup strategy
   - Enable monitoring and alerts

3. **Scalability:**
   - Use container orchestration (Kubernetes)
   - Implement read replicas
   - Add caching layer

4. **Configuration:**
   - Use secrets management (Key Vault, Secrets Manager)
   - Environment-specific configuration
   - Health checks and auto-restart

## Next Steps

1. ✅ Database running and migrations applied
2. ⏳ Start API: `dotnet run -p src/IAT.WebApi`
3. ⏳ Test API: `bash docker-test.sh`
4. ⏳ Access Swagger: http://localhost:9000/swagger/
5. ⏳ Deploy to production

## Support

For issues or questions:

1. Check troubleshooting section above
2. Review Docker logs: `docker-compose logs`
3. Check migration output: `cat migration_output.log`
4. See TESTING.md for API testing guide
5. See README.md for project overview

---

**Happy Migrating!** 🚀
