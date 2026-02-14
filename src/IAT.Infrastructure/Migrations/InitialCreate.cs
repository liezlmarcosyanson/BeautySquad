namespace IAT.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Approvals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentSubmissionId = c.Guid(nullable: false),
                        SubmittedBy = c.Guid(nullable: false),
                        Decision = c.Int(nullable: false),
                        Comments = c.String(),
                        SubmittedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentSubmissions", t => t.ContentSubmissionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SubmittedBy, cascadeDelete: false)
                .Index(t => t.ContentSubmissionId)
                .Index(t => t.SubmittedBy);
            
            CreateTable(
                "dbo.ContentSubmissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CampaignId = c.Guid(nullable: false),
                        InfluencerId = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        SubmittedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campaigns", t => t.CampaignId, cascadeDelete: true)
                .ForeignKey("dbo.Influencers", t => t.InfluencerId, cascadeDelete: true)
                .Index(t => t.CampaignId)
                .Index(t => t.InfluencerId);
            
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        BrandId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.BrandId, cascadeDelete: false)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.CampaignDeliverables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CampaignId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                        DueDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campaigns", t => t.CampaignId, cascadeDelete: true)
                .Index(t => t.CampaignId);
            
            CreateTable(
                "dbo.ContentVersions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentSubmissionId = c.Guid(nullable: false),
                        Caption = c.String(),
                        Hashtags = c.String(),
                        VersionNumber = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentSubmissions", t => t.ContentSubmissionId, cascadeDelete: true)
                .Index(t => t.ContentSubmissionId);
            
            CreateTable(
                "dbo.InfluencerTags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InfluencerId = c.Guid(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Influencers", t => t.InfluencerId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.InfluencerId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Influencers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Bio = c.String(),
                        IsVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PerformanceMetrics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentSubmissionId = c.Guid(nullable: false),
                        Likes = c.Int(nullable: false),
                        Comments = c.Int(nullable: false),
                        Shares = c.Int(nullable: false),
                        Clicks = c.Int(nullable: false),
                        Impressions = c.Int(nullable: false),
                        RecordedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentSubmissions", t => t.ContentSubmissionId, cascadeDelete: true)
                .Index(t => t.ContentSubmissionId);
            
            CreateTable(
                "dbo.SocialProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InfluencerId = c.Guid(nullable: false),
                        Platform = c.Int(nullable: false),
                        Handle = c.String(nullable: false, maxLength: 256),
                        FollowerCount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Influencers", t => t.InfluencerId, cascadeDelete: true)
                .Index(t => t.InfluencerId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        PasswordHash = c.String(nullable: false),
                        FullName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        LastLoginAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Campaigns", "BrandId", "dbo.Users");
            DropForeignKey("dbo.SocialProfiles", "InfluencerId", "dbo.Influencers");
            DropForeignKey("dbo.Influencers", "UserId", "dbo.Users");
            DropForeignKey("dbo.PerformanceMetrics", "ContentSubmissionId", "dbo.ContentSubmissions");
            DropForeignKey("dbo.InfluencerTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.InfluencerTags", "InfluencerId", "dbo.Influencers");
            DropForeignKey("dbo.ContentVersions", "ContentSubmissionId", "dbo.ContentSubmissions");
            DropForeignKey("dbo.CampaignDeliverables", "CampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.ContentSubmissions", "InfluencerId", "dbo.Influencers");
            DropForeignKey("dbo.ContentSubmissions", "CampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.Approvals", "SubmittedBy", "dbo.Users");
            DropForeignKey("dbo.Approvals", "ContentSubmissionId", "dbo.ContentSubmissions");
            
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.SocialProfiles", new[] { "InfluencerId" });
            DropIndex("dbo.Influencers", new[] { "UserId" });
            DropIndex("dbo.PerformanceMetrics", new[] { "ContentSubmissionId" });
            DropIndex("dbo.InfluencerTags", new[] { "TagId" });
            DropIndex("dbo.InfluencerTags", new[] { "InfluencerId" });
            DropIndex("dbo.ContentVersions", new[] { "ContentSubmissionId" });
            DropIndex("dbo.CampaignDeliverables", new[] { "CampaignId" });
            DropIndex("dbo.ContentSubmissions", new[] { "InfluencerId" });
            DropIndex("dbo.ContentSubmissions", new[] { "CampaignId" });
            DropIndex("dbo.Approvals", new[] { "SubmittedBy" });
            DropIndex("dbo.Approvals", new[] { "ContentSubmissionId" });
            
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Tags");
            DropTable("dbo.Roles");
            DropTable("dbo.SocialProfiles");
            DropTable("dbo.PerformanceMetrics");
            DropTable("dbo.Influencers");
            DropTable("dbo.InfluencerTags");
            DropTable("dbo.ContentVersions");
            DropTable("dbo.CampaignDeliverables");
            DropTable("dbo.ContentSubmissions");
            DropTable("dbo.Campaigns");
            DropTable("dbo.Approvals");
        }
    }
}
