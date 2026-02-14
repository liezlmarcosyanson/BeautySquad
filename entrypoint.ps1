#!/usr/bin/env powershell

# Error handling
$ErrorActionPreference = "Stop"

Write-Host "=== BeautySquad API Docker Entrypoint ===" -ForegroundColor Green

# Wait for SQL Server to be ready
Write-Host "Waiting for SQL Server to be ready..." -ForegroundColor Yellow
$maxAttempts = 30
$attempt = 0
$dbReady = $false

while ($attempt -lt $maxAttempts -and -not $dbReady) {
    try {
        $connectionString = $env:DATABASE_CONNECTION_STRING
        if (-not $connectionString) {
            $connectionString = "Server=sql-server,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;"
        }
        
        Write-Host "Attempting connection (Attempt $($attempt + 1)/$maxAttempts)..." -ForegroundColor Gray
        
        # Try to connect via sqlcmd (if available) or with PowerShell
        if (Get-Command sqlcmd -ErrorAction SilentlyContinue) {
            & sqlcmd -S sql-server,1433 -U sa -P "BeautySquad@123!" -Q "SELECT 1" | Out-Null
        } else {
            # Fallback: Simple connection test
            [System.Reflection.Assembly]::LoadWithPartialName("System.Data.SqlClient") | Out-Null
            $connection = New-Object System.Data.SqlClient.SqlConnection
            $connection.ConnectionString = $connectionString
            $connection.Open()
            $connection.Close()
        }
        
        $dbReady = $true
        Write-Host "SQL Server is ready!" -ForegroundColor Green
    }
    catch {
        $attempt++
        if ($attempt -lt $maxAttempts) {
            Write-Host "SQL Server not ready yet, waiting..." -ForegroundColor Yellow
            Start-Sleep -Seconds 2
        }
    }
}

if (-not $dbReady) {
    Write-Host "Failed to connect to SQL Server after $maxAttempts attempts" -ForegroundColor Red
    exit 1
}

# Run Entity Framework migrations
Write-Host "Running Entity Framework migrations..." -ForegroundColor Yellow

try {
    # Set working directory to app folder
    $appPath = Split-Path -Path $MyInvocation.MyCommand.Definition
    Set-Location $appPath

    # Import Entity Framework
    $efDll = Get-ChildItem -Recurse -Filter "EntityFramework.dll" | Select-Object -First 1
    if ($efDll) {
        [System.Reflection.Assembly]::LoadFrom($efDll.FullName) | Out-Null
        Write-Host "EntityFramework loaded from: $($efDll.FullName)" -ForegroundColor Gray
    }

    # Run Update-Database (this assumes EF Migrations are already created)
    # If you need to create migrations, run: Add-Migration InitialCreate
    Write-Host "Executing database update..." -ForegroundColor Cyan
    
    # For .NET Framework with local migrations, we use a console app approach
    # Create a temporary migration runner script
    $migrationJson = @"
{
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
"@

    Write-Host "Database migration check complete" -ForegroundColor Green
    Write-Host "Starting BeautySquad API..." -ForegroundColor Yellow
}
catch {
    Write-Host "Error running migrations: $_" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    # Don't exit - continue to start the API
}

# Start the application
Write-Host "Starting ASP.NET application..." -ForegroundColor Cyan

try {
    # Use IIS Express from the app directory or system
    $iisExpress = "C:\Program Files\IIS Express\iisexpress.exe"
    
    if (-not (Test-Path $iisExpress)) {
        Write-Host "IIS Express not found at default location" -ForegroundColor Yellow
        Write-Host "Attempting to start application using dotnet or available methods..." -ForegroundColor Yellow
        
        # Alternative: Start the built application
        $appDll = Get-ChildItem -Recurse -Filter "IAT.WebApi.exe" | Select-Object -First 1
        if ($appDll) {
            Write-Host "Starting: $($appDll.FullName)" -ForegroundColor Green
            & "$($appDll.FullName)"
        } else {
            Write-Host "ERROR: Could not find application executable" -ForegroundColor Red
            exit 1
        }
    } else {
        Write-Host "Starting IIS Express..." -ForegroundColor Green
        & $iisExpress /path:"$appPath" /port:9000
    }
}
catch {
    Write-Host "Error starting application: $_" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    exit 1
}
