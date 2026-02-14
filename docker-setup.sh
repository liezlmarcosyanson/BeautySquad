#!/bin/bash

# Docker Setup & Migration Script for BeautySquad API
# This script:
# 1. Starts SQL Server in Docker
# 2. Waits for it to be ready
# 3. Runs Entity Framework migrations

set -e

PROJECT_NAME="beautysquad"
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo "=================================="
echo "BeautySquad Docker Database Setup"
echo "=================================="
echo ""

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m'

# Function to print colored output
print_status() {
    echo -e "${GREEN}[✓]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[!]${NC} $1"
}

print_error() {
    echo -e "${RED}[✗]${NC} $1"
}

# Check if Docker is installed
if ! command -v docker &> /dev/null; then
    print_error "Docker is not installed. Please install Docker first."
    exit 1
fi

print_status "Docker found"

# Check if Docker Compose is available (as plugin or standalone)
if command -v docker-compose &> /dev/null; then
    DOCKER_COMPOSE="docker-compose"
    print_status "Docker Compose (standalone) found"
elif docker compose version &> /dev/null; then
    DOCKER_COMPOSE="docker compose"
    print_status "Docker Compose (plugin) found"
else
    print_error "Docker Compose is not installed. Please install Docker Compose first."
    exit 1
fi

echo ""

# Check if .NET tools are available for migration
if ! command -v dotnet &> /dev/null; then
    print_error ".NET SDK is not installed. Please install .NET Framework SDK."
    print_warning "You can still start the database container, but migrations must be run differently."
fi

# Clean up previous containers (optional)
print_warning "Checking for existing containers..."
if docker ps -a | grep -q beautysquad-sqlserver; then
    read -p "SQL Server container already exists. Remove it? (y/n) " -n 1 -r
    echo ""
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        print_warning "Removing existing container..."
        $DOCKER_COMPOSE -f docker-compose.yml down -v 2>/dev/null || docker stop beautysquad-sqlserver beautysquad-api 2>/dev/null || true
        print_status "Containers removed"
    fi
fi

echo ""
print_warning "Starting SQL Server container..."
cd "$SCRIPT_DIR"

# Start containers
$DOCKER_COMPOSE -f docker-compose.yml up -d

print_status "SQL Server container started"
echo ""

# Wait for SQL Server to be ready
print_warning "Waiting for SQL Server to initialize (this may take 30-60 seconds)..."
COUNTER=0
MAX_ATTEMPTS=120

while [ $COUNTER -lt $MAX_ATTEMPTS ]; do
    if docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q 'SELECT 1' &>/dev/null; then
        print_status "SQL Server is ready"
        break
    fi
    COUNTER=$((COUNTER + 1))
    if [ $((COUNTER % 20)) -eq 0 ]; then
        echo "  Waiting... ($COUNTER seconds elapsed)"
    fi
    sleep 1
done

if [ $COUNTER -eq $MAX_ATTEMPTS ]; then
    print_error "SQL Server failed to start after $MAX_ATTEMPTS seconds"
    echo ""
    print_warning "Showing logs:"
    $DOCKER_COMPOSE logs sql-server | tail -50
    exit 1
fi

echo ""

# Create initial database
print_warning "Creating BeautySquadDb database..."
docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' <<EOF
CREATE DATABASE [BeautySquadDb]
GO
USE [BeautySquadDb]
GO
PRINT 'Database BeautySquadDb created'
GO
EOF

print_status "Database created"
echo ""

# Run migrations using dotnet
if command -v dotnet &> /dev/null; then
    print_warning "Running Entity Framework migrations..."
    
    # Set connection string environment variable
    export DATABASE_CONNECTION_STRING="Server=localhost,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;"
    
    cd "$SCRIPT_DIR"
    
    # Try to build and run migration runner
    if [ -d "src/IAT.MigrationRunner" ]; then
        if dotnet build src/IAT.MigrationRunner/IAT.MigrationRunner.csproj -c Release 2>/dev/null; then
            if dotnet run -p src/IAT.MigrationRunner -c Release 2>/dev/null; then
                print_status "Migrations completed successfully"
            else
                print_warning "Migrations may not have run (could retry manually)"
            fi
        else
            print_warning "Could not build migration runner"
        fi
    else
        print_warning "Migration runner project not found at src/IAT.MigrationRunner"
        print_warning "You can create migrations using Package Manager Console:"
        print_warning "  1. Open Package Manager Console in Visual Studio"
        print_warning "  2. Update-Database"
    fi
else
    print_warning "dotnet CLI not found, skipping automatic migrations"
    print_warning "Run migrations manually using Visual Studio Package Manager Console:"
    print_warning "  Update-Database"
fi

echo ""

# Test database connection
print_warning "Testing database connectivity..."
if docker exec beautysquad-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -Q 'SELECT @@VERSION' | head -5; then
    print_status "Database connectivity test passed"
else
    print_error "Database connectivity test failed"
    exit 1
fi

echo ""

# Show container status
print_status "Container Status:"
$DOCKER_COMPOSE -f docker-compose.yml ps

echo ""
echo "=================================="
echo "Setup Complete!"
echo "=================================="
echo ""
echo "Database Connection:"
echo "  • Server: localhost:1433"
echo "  • Username: sa"
echo "  • Password: BeautySquad@123!"
echo "  • Database: BeautySquadDb"
echo ""
echo "Useful commands:"
echo "  • View logs: $DOCKER_COMPOSE logs -f"
echo "  • View DB logs: $DOCKER_COMPOSE logs -f sql-server"
echo "  • Connect to DB: docker exec -it beautysquad-sqlserver sqlcmd -S localhost -U sa -P 'BeautySquad@123!' -d BeautySquadDb"
echo "  • Stop services: $DOCKER_COMPOSE down"
echo "  • Stop and remove data: $DOCKER_COMPOSE down -v"
echo ""
echo "Next steps:"
echo "  1. Update your Web.config with the connection string above"
echo "  2. Run the application: dotnet run -p src/IAT.WebApi"
echo "  3. Access Swagger: http://localhost:9000/swagger/"
echo "  4. Run tests: bash docker-test.sh"
echo ""
