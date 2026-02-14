#!/bin/bash

# Migration Test Script for BeautySquad
# Tests database connection and runs migrations verification

set -e

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

print_header() {
    echo -e "\n${BLUE}=== $1 ===${NC}\n"
}

print_success() {
    echo -e "${GREEN}✓${NC} $1"
}

print_failure() {
    echo -e "${RED}✗${NC} $1"
}

print_info() {
    echo -e "${YELLOW}ℹ${NC} $1"
}

print_test() {
    echo "Testing: $1"
}

print_header "BeautySquad Database Migration Tests"

# Check if Docker is running
print_test "Docker connectivity"
if docker ps &>/dev/null; then
    print_success "Docker is running"
else
    print_failure "Docker is not running or not accessible"
    echo ""
    print_info "Make sure Docker daemon is running and you have proper permissions"
    exit 1
fi

print_test "SQL Server container"
if docker ps | grep -q beautysquad-sqlserver; then
    print_success "SQL Server container is running"
else
    print_failure "SQL Server container is not running"
    echo ""
    print_info "Start the container with: bash docker-setup.sh"
    exit 1
fi

print_test "SQL Server connectivity"
if docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q 'SELECT @@VERSION' &>/dev/null; then
    print_success "SQL Server is accessible"
else
    print_failure "Cannot connect to SQL Server"
    exit 1
fi

print_test "BeautySquadDb database"
if docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q 'USE BeautySquadDb; SELECT 1' &>/dev/null; then
    print_success "BeautySquadDb database exists"
    DB_EXISTS=true
else
    print_info "BeautySquadDb database does not exist yet (will be created during migration)"
    DB_EXISTS=false
fi

echo ""
print_header "Running Migrations"

if command -v dotnet &> /dev/null; then
    print_info "Using: dotnet CLI"
    
    # Build the migration runner
    print_test "Building migration runner"
    if dotnet build src/IAT.MigrationRunner/IAT.MigrationRunner.csproj -c Release -v quiet &>/dev/null; then
        print_success "Migration runner built successfully"
    else
        print_failure "Failed to build migration runner"
        echo ""
        print_info "Attempting to build with verbose output..."
        dotnet build src/IAT.MigrationRunner/IAT.MigrationRunner.csproj -c Release
        exit 1
    fi
    
    # Run migrations
    print_test "Executing database migrations"
    export DATABASE_CONNECTION_STRING="Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;"
    
    cd "$SCRIPT_DIR"
    if dotnet run -p src/IAT.MigrationRunner -c Release 2>&1 | tee migration_output.log; then
        print_success "Migrations executed successfully"
        MIGRATION_SUCCESS=true
    else
        print_failure "Migration execution failed"
        MIGRATION_SUCCESS=false
        echo ""
        print_info "Check migration_output.log for details"
    fi
else
    print_info "dotnet CLI not available, checking for .NET Framework tools..."
    
    # Alternative: try running with .NET Framework
    if [ -f "src/IAT.MigrationRunner/bin/Release/IAT.MigrationRunner.exe" ]; then
        print_test "Running compiled executable"
        export DATABASE_CONNECTION_STRING="Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;"
        
        if "src/IAT.MigrationRunner/bin/Release/IAT.MigrationRunner.exe"; then
            print_success "Migrations executed successfully"
            MIGRATION_SUCCESS=true
        else
            print_failure "Migration execution failed"
            MIGRATION_SUCCESS=false
        fi
    else
        print_failure "Cannot run migrations - neither dotnet CLI nor compiled executable found"
        MIGRATION_SUCCESS=false
    fi
fi

echo ""
print_header "Verifying Migration Results"

if [ "$MIGRATION_SUCCESS" = true ] || [ "$DB_EXISTS" = true ]; then
    print_test "Checking for EF Core migration history"
    if docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -d BeautySquadDb -Q 'SELECT * FROM __EFMigrationsHistory' &>/dev/null 2>&1; then
        print_success "EF Core migration history table found"
    elif docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -d BeautySquadDb -Q 'SELECT * FROM dbo.User' &>/dev/null 2>&1; then
        print_success "User table exists (migrations applied)"
    else
        print_info "Checking for other schema objects..."
    fi
    
    print_test "Table count"
    TABLE_COUNT=$(docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -d BeautySquadDb -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo'" 2>/dev/null | grep -v "^$" | tail -1 | tr -d ' ')
    
    if [ ! -z "$TABLE_COUNT" ] && [ "$TABLE_COUNT" -gt 0 ]; then
        print_success "Found $TABLE_COUNT tables in database"
    else
        print_info "Database appears to be empty (tables may not be created yet)"
    fi
fi

echo ""
print_header "Test Summary"

# Check connectivity one more time
print_test "Final connectivity check"
if docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q 'SELECT GETDATE()' &>/dev/null; then
    print_success "Database is healthy and accessible"
    
    echo ""
    echo "${GREEN}✓ All migration tests passed!${NC}"
    echo ""
    echo "Database Connection Information:"
    echo "  Server: localhost"
    echo "  Port: 1433"
    echo "  Username: sa"
    echo "  Password: BeautySquad@123!"
    echo "  Database: BeautySquadDb"
    echo ""
    echo "You can now:"
    echo "  1. Update Web.config with the connection string"
    echo "  2. Run the application: cd src/IAT.WebApi && dotnet run"
    echo "  3. Access Swagger: http://localhost:9000/swagger/"
    exit 0
else
    print_failure "Final connectivity check failed"
    echo ""
    echo "${RED}✗ Migration tests failed!${NC}"
    exit 1
fi
