-- BeautySquad Database Schema Creation Script
-- Creates all tables for the application

-- Drop existing tables (if needed)
-- Uncomment the IF EXISTS lines to drop existing tables:
/*
IF OBJECT_ID('dbo.PerformanceMetrics', 'U') IS NOT NULL DROP TABLE dbo.PerformanceMetrics;
IF OBJECT_ID('dbo.Approvals', 'U') IS NOT NULL DROP TABLE dbo.Approvals;
IF OBJECT_ID('dbo.ContentVersions', 'U') IS NOT NULL DROP TABLE dbo.ContentVersions;
IF OBJECT_ID('dbo.ContentSubmissions', 'U') IS NOT NULL DROP TABLE dbo.ContentSubmissions;
IF OBJECT_ID('dbo.CampaignDeliverables', 'U') IS NOT NULL DROP TABLE dbo.CampaignDeliverables;
IF OBJECT_ID('dbo.Collaborations', 'U') IS NOT NULL DROP TABLE dbo.Collaborations;
IF OBJECT_ID('dbo.Campaigns', 'U') IS NOT NULL DROP TABLE dbo.Campaigns;
IF OBJECT_ID('dbo.InfluencerTags', 'U') IS NOT NULL DROP TABLE dbo.InfluencerTags;
IF OBJECT_ID('dbo.SocialProfiles', 'U') IS NOT NULL DROP TABLE dbo.SocialProfiles;
IF OBJECT_ID('dbo.Influencers', 'U') IS NOT NULL DROP TABLE dbo.Influencers;
IF OBJECT_ID('dbo.UserRoles', 'U') IS NOT NULL DROP TABLE dbo.UserRoles;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Tags', 'U') IS NOT NULL DROP TABLE dbo.Tags;
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL DROP TABLE dbo.Roles;
*/

-- =============================================================================
-- Create Tables
-- =============================================================================

-- Roles Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
CREATE TABLE [dbo].[Roles] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Name] NVARCHAR(100) NOT NULL UNIQUE
);

-- Users Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
CREATE TABLE [dbo].[Users] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [Email] NVARCHAR(256) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(MAX) NOT NULL,
    [FullName] NVARCHAR(MAX),
    [IsActive] BIT NOT NULL DEFAULT 1,
    [LastLoginAt] DATETIME2
);

-- UserRoles Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
CREATE TABLE [dbo].[UserRoles] (
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_UserRoles_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_UserRoles_Roles FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id]) ON DELETE CASCADE
);

-- Tags Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
CREATE TABLE [dbo].[Tags] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Name] NVARCHAR(100) NOT NULL UNIQUE
);

-- Influencers Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Influencers]') AND type in (N'U'))
CREATE TABLE [dbo].[Influencers] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [FullName] NVARCHAR(MAX) NOT NULL,
    [Bio] NVARCHAR(MAX),
    [Email] NVARCHAR(256),
    [Phone] NVARCHAR(20),
    [Geography] NVARCHAR(MAX),
    [AdvocacyStatus] INT NOT NULL,
    [Notes] NVARCHAR(MAX),
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [CreatedAt] DATETIME2 NOT NULL,
    [ModifiedAt] DATETIME2
);

-- InfluencerTags Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InfluencerTags]') AND type in (N'U'))
CREATE TABLE [dbo].[InfluencerTags] (
    [InfluencerId] UNIQUEIDENTIFIER NOT NULL,
    [TagId] INT NOT NULL,
    CONSTRAINT PK_InfluencerTags PRIMARY KEY (InfluencerId, TagId),
    CONSTRAINT FK_InfluencerTags_Influencers FOREIGN KEY ([InfluencerId]) REFERENCES [dbo].[Influencers]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_InfluencerTags_Tags FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags]([Id]) ON DELETE CASCADE
);

-- SocialProfiles Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocialProfiles]') AND type in (N'U'))
CREATE TABLE [dbo].[SocialProfiles] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [InfluencerId] UNIQUEIDENTIFIER NOT NULL,
    [Platform] INT NOT NULL,
    [Handle] NVARCHAR(256) NOT NULL,
    [Url] NVARCHAR(MAX),
    [Followers] BIGINT NOT NULL,
    [EngagementRate] FLOAT NOT NULL,
    [IsVerified] BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_SocialProfiles_Influencers FOREIGN KEY ([InfluencerId]) REFERENCES [dbo].[Influencers]([Id]) ON DELETE CASCADE,
    CONSTRAINT UQ_SocialProfiles_InfluencerPlatform UNIQUE ([InfluencerId], [Platform])
);

-- Campaigns Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Campaigns]') AND type in (N'U'))
CREATE TABLE [dbo].[Campaigns] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [Brief] NVARCHAR(MAX),
    [Objectives] NVARCHAR(MAX),
    [StartDate] DATETIME2 NOT NULL,
    [EndDate] DATETIME2 NOT NULL,
    [Budget] DECIMAL(18, 2) NOT NULL,
    [Status] INT NOT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [CreatedAt] DATETIME2 NOT NULL
);

-- CampaignDeliverables Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CampaignDeliverables]') AND type in (N'U'))
CREATE TABLE [dbo].[CampaignDeliverables] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [CampaignId] UNIQUEIDENTIFIER NOT NULL,
    [Type] INT NOT NULL,
    [Description] NVARCHAR(MAX),
    [DueDate] DATETIME2,
    [AssignedInfluencerId] UNIQUEIDENTIFIER,
    [Status] INT NOT NULL,
    CONSTRAINT FK_CampaignDeliverables_Campaigns FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaigns]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_CampaignDeliverables_Influencers FOREIGN KEY ([AssignedInfluencerId]) REFERENCES [dbo].[Influencers]([Id])
);

-- Collaborations Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Collaborations]') AND type in (N'U'))
CREATE TABLE [dbo].[Collaborations] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [InfluencerId] UNIQUEIDENTIFIER NOT NULL,
    [CampaignId] UNIQUEIDENTIFIER,
    [Title] NVARCHAR(MAX),
    [Date] DATETIME2 NOT NULL,
    [OutcomeNotes] NVARCHAR(MAX),
    CONSTRAINT FK_Collaborations_Influencers FOREIGN KEY ([InfluencerId]) REFERENCES [dbo].[Influencers]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_Collaborations_Campaigns FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaigns]([Id])
);

-- ContentSubmissions Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentSubmissions]') AND type in (N'U'))
CREATE TABLE [dbo].[ContentSubmissions] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [CampaignId] UNIQUEIDENTIFIER NOT NULL,
    [InfluencerId] UNIQUEIDENTIFIER NOT NULL,
    [DeliverableId] UNIQUEIDENTIFIER,
    [Title] NVARCHAR(MAX),
    [Caption] NVARCHAR(MAX),
    [AssetPath] NVARCHAR(MAX),
    [State] INT NOT NULL,
    [CurrentVersionNumber] INT NOT NULL DEFAULT 0,
    [SubmittedAt] DATETIME2,
    CONSTRAINT FK_ContentSubmissions_Campaigns FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaigns]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_ContentSubmissions_Influencers FOREIGN KEY ([InfluencerId]) REFERENCES [dbo].[Influencers]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_ContentSubmissions_CampaignDeliverables FOREIGN KEY ([DeliverableId]) REFERENCES [dbo].[CampaignDeliverables]([Id])
);

-- ContentVersions Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentVersions]') AND type in (N'U'))
CREATE TABLE [dbo].[ContentVersions] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [SubmissionId] UNIQUEIDENTIFIER NOT NULL,
    [VersionNumber] INT NOT NULL,
    [Caption] NVARCHAR(MAX),
    [AssetPath] NVARCHAR(MAX),
    [CreatedAt] DATETIME2 NOT NULL,
    [CreatedByUserId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_ContentVersions_ContentSubmissions FOREIGN KEY ([SubmissionId]) REFERENCES [dbo].[ContentSubmissions]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_ContentVersions_Users FOREIGN KEY ([CreatedByUserId]) REFERENCES [dbo].[Users]([Id])
);

-- Approvals Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Approvals]') AND type in (N'U'))
CREATE TABLE [dbo].[Approvals] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [SubmissionId] UNIQUEIDENTIFIER NOT NULL,
    [ReviewerUserId] UNIQUEIDENTIFIER NOT NULL,
    [Decision] INT NOT NULL,
    [Comments] NVARCHAR(MAX),
    [DecidedAt] DATETIME2 NOT NULL,
    CONSTRAINT FK_Approvals_ContentSubmissions FOREIGN KEY ([SubmissionId]) REFERENCES [dbo].[ContentSubmissions]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_Approvals_Users FOREIGN KEY ([ReviewerUserId]) REFERENCES [dbo].[Users]([Id])
);

-- PerformanceMetrics Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PerformanceMetrics]') AND type in (N'U'))
CREATE TABLE [dbo].[PerformanceMetrics] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [SubmissionId] UNIQUEIDENTIFIER NOT NULL,
    [Reach] INT NOT NULL,
    [Engagements] INT NOT NULL,
    [Saves] INT NOT NULL,
    [Shares] INT NOT NULL,
    [Clicks] INT NOT NULL,
    [Conversions] INT NOT NULL,
    [CapturedAt] DATETIME2 NOT NULL,
    CONSTRAINT FK_PerformanceMetrics_ContentSubmissions FOREIGN KEY ([SubmissionId]) REFERENCES [dbo].[ContentSubmissions]([Id]) ON DELETE CASCADE
);

-- Create Indexes
CREATE NONCLUSTERED INDEX IX_Users_Email ON [dbo].[Users]([Email]);
CREATE NONCLUSTERED INDEX IX_Tags_Name ON [dbo].[Tags]([Name]);
CREATE NONCLUSTERED INDEX IX_UserRoles_UserId ON [dbo].[UserRoles]([UserId]);
CREATE NONCLUSTERED INDEX IX_UserRoles_RoleId ON [dbo].[UserRoles]([RoleId]);
CREATE NONCLUSTERED INDEX IX_SocialProfiles_InfluencerId ON [dbo].[SocialProfiles]([InfluencerId]);
CREATE NONCLUSTERED INDEX IX_InfluencerTags_InfluencerId ON [dbo].[InfluencerTags]([InfluencerId]);
CREATE NONCLUSTERED INDEX IX_InfluencerTags_TagId ON [dbo].[InfluencerTags]([TagId]);
CREATE NONCLUSTERED INDEX IX_CampaignDeliverables_CampaignId ON [dbo].[CampaignDeliverables]([CampaignId]);
CREATE NONCLUSTERED INDEX IX_ContentSubmissions_CampaignId ON [dbo].[ContentSubmissions]([CampaignId]);
CREATE NONCLUSTERED INDEX IX_ContentSubmissions_InfluencerId ON [dbo].[ContentSubmissions]([InfluencerId]);
CREATE NONCLUSTERED INDEX IX_ContentVersions_SubmissionId ON [dbo].[ContentVersions]([SubmissionId]);
CREATE NONCLUSTERED INDEX IX_Approvals_SubmissionId ON [dbo].[Approvals]([SubmissionId]);
CREATE NONCLUSTERED INDEX IX_Approvals_ReviewerUserId ON [dbo].[Approvals]([ReviewerUserId]);
CREATE NONCLUSTERED INDEX IX_PerformanceMetrics_SubmissionId ON [dbo].[PerformanceMetrics]([SubmissionId]);

PRINT 'Database schema created successfully!';
