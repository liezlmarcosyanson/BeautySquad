using IAT.Domain;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace IAT.Infrastructure.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<IAT.Infrastructure.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(IAT.Infrastructure.AppDbContext context)
        {
            var roles = new[] { "Admin", "BrandManager", "CampaignManager", "Influencer", "Legal" };
            foreach (var r in roles)
            {
                if (!context.Roles.Any(x => x.Name == r))
                {
                    context.Roles.Add(new Role { Name = r });
                }
            }

            context.SaveChanges();

            if (!context.Users.Any(u => u.Email == "admin@iat.local"))
            {
                var admin = new User
                {
                    Email = "admin@iat.local",
                    PasswordHash = "PLEASE_FORCE_PASSWORD_RESET",
                    FullName = "System Admin",
                    IsActive = true
                };
                context.Users.Add(admin);
                context.SaveChanges();

                var adminRole = context.Roles.First(r => r.Name == "Admin");
                context.UserRoles.Add(new UserRole { UserId = admin.Id, RoleId = adminRole.Id });
            }

            context.SaveChanges();

            if (!context.Tags.Any(t => t.Name == "Sustainability"))
            {
                context.Tags.Add(new Tag { Name = "Sustainability" });
                context.Tags.Add(new Tag { Name = "Beauty" });
            }
            context.SaveChanges();

            if (!context.Influencers.Any())
            {
                var inf = new Influencer
                {
                    FullName = "Jane Doe",
                    Bio = "Eco-conscious beauty influencer",
                    Email = "jane@example.com",
                    AdvocacyStatus = AdvocacyStatus.Active
                };
                context.Influencers.Add(inf);
                context.SaveChanges();

                var t1 = context.Tags.First(t => t.Name == "Sustainability");
                var t2 = context.Tags.First(t => t.Name == "Beauty");
                context.InfluencerTags.Add(new InfluencerTag { InfluencerId = inf.Id, TagId = t1.Id });
                context.InfluencerTags.Add(new InfluencerTag { InfluencerId = inf.Id, TagId = t2.Id });

                context.SocialProfiles.Add(new SocialProfile { InfluencerId = inf.Id, Platform = SocialPlatform.Instagram, Handle = "@janedoe", Url = "https://instagram.com/janedoe", Followers = 120000, EngagementRate = 3.2 });
                context.SocialProfiles.Add(new SocialProfile { InfluencerId = inf.Id, Platform = SocialPlatform.TikTok, Handle = "@janedoe", Url = "https://tiktok.com/@janedoe", Followers = 45000, EngagementRate = 5.4 });
                context.SaveChanges();
            }

            if (!context.Campaigns.Any())
            {
                var campaign = new Campaign
                {
                    Name = "Spring Launch",
                    Brief = "Launch of spring product line",
                    Objectives = "Awareness & conversions",
                    StartDate = DateTime.UtcNow.Date,
                    EndDate = DateTime.UtcNow.AddDays(30).Date,
                    Budget = 10000,
                    Status = CampaignStatus.Draft
                };
                context.Campaigns.Add(campaign);
                context.SaveChanges();

                var d1 = new CampaignDeliverable { CampaignId = campaign.Id, Type = DeliverableType.Post, Description = "Main feed post", DueDate = DateTime.UtcNow.AddDays(7), Status = DeliverableStatus.Planned };
                var d2 = new CampaignDeliverable { CampaignId = campaign.Id, Type = DeliverableType.Story, Description = "3-frame story", DueDate = DateTime.UtcNow.AddDays(5), Status = DeliverableStatus.Planned };
                context.CampaignDeliverables.Add(d1);
                context.CampaignDeliverables.Add(d2);
                context.SaveChanges();

                var influencer = context.Influencers.FirstOrDefault();
                if (influencer != null)
                {
                    var submission = new ContentSubmission { CampaignId = campaign.Id, InfluencerId = influencer.Id, DeliverableId = d1.Id, Title = "Draft Post", Caption = "#sponsored - draft", State = SubmissionState.Draft };
                    context.ContentSubmissions.Add(submission);
                    context.SaveChanges();
                }
            }

            if (!context.PerformanceMetrics.Any())
            {
                var submission = context.ContentSubmissions.FirstOrDefault();
                if (submission != null)
                {
                    context.PerformanceMetrics.Add(new PerformanceMetric { SubmissionId = submission.Id, Reach = 10000, Engagements = 800, Clicks = 120, Conversions = 5 });
                    context.PerformanceMetrics.Add(new PerformanceMetric { SubmissionId = submission.Id, Reach = 12000, Engagements = 900, Clicks = 150, Conversions = 10 });
                    context.SaveChanges();
                }
            }
        }
    }
}
