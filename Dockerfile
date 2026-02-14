# Multi-stage build for Migration Runner

FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 as build-migrator

WORKDIR /src

# Copy solution and project files
COPY ["src/IAT.Domain/IAT.Domain.csproj", "src/IAT.Domain/"]
COPY ["src/IAT.Infrastructure/IAT.Infrastructure.csproj", "src/IAT.Infrastructure/"]
COPY ["src/IAT.Application/IAT.Application.csproj", "src/IAT.Application/"]
COPY ["src/IAT.MigrationRunner/IAT.MigrationRunner.csproj", "src/IAT.MigrationRunner/"]

# Restore NuGet packages
RUN nuget restore src/IAT.Domain/IAT.Domain.csproj && \
    nuget restore src/IAT.Infrastructure/IAT.Infrastructure.csproj && \
    nuget restore src/IAT.Application/IAT.Application.csproj && \
    nuget restore src/IAT.MigrationRunner/IAT.MigrationRunner.csproj

# Copy all source files
COPY src/ src/

# Build the migration runner
RUN msbuild src/IAT.MigrationRunner/IAT.MigrationRunner.csproj /p:Configuration=Release /p:Platform=AnyCPU

# Runtime stage
FROM mcr.microsoft.com/dotnet/framework/runtime:4.8

WORKDIR /app

# Copy built migration runner
COPY --from=build-migrator /src/src/IAT.MigrationRunner/bin/Release /app

# Add MSSQL tools
RUN powershell -Command \
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; \
    Invoke-WebRequest -Uri 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile nuget.exe

# Set environment variables
ENV DATABASE_CONNECTION_STRING="Server=sql-server,1433;Database=BeautySquadDb;User Id=sa;Password=BeautySquad@123!;"

# Default command is to run the migration runner
ENTRYPOINT ["IAT.MigrationRunner.exe"]

