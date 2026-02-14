-- Initialize BeautySquad Database
USE master;
GO

-- Wait for SQL Server to be fully ready
WAITFOR DELAY '00:00:05';
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BeautySquadDb')
BEGIN
    CREATE DATABASE [BeautySquadDb];
    PRINT 'Database BeautySquadDb created successfully.';
END
ELSE
BEGIN
    PRINT 'Database BeautySquadDb already exists.';
END
GO

-- Switch to the new database
USE [BeautySquadDb];
GO

-- Create tables for Entity Framework Code-First migrations
-- Note: Entity Framework will create the actual tables via migrations
-- This script just ensures the database exists and is ready

-- Check if __MigrationHistory table exists (EF marker)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.__MigrationHistory'))
BEGIN
    PRINT 'Waiting for Entity Framework migrations to create tables...';
END
ELSE
BEGIN
    PRINT 'Entity Framework migration history table already exists.';
END
GO

PRINT 'Database initialization complete.';
GO
