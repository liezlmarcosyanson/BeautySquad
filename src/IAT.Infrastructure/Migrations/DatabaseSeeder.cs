using System;
using System.Collections.Generic;
using System.Linq;
using IAT.Domain;

namespace IAT.Infrastructure.Migrations
{
    /// <summary>
    /// Database seeding class for populating test data
    /// Run this after migrations are applied
    /// </summary>
    public static class DatabaseSeeder
    {
        public static void SeedData(AppDbContext context)
        {
            // Only seed if database is empty
            if (context.Users.Any() || context.Roles.Any())
            {
                Console.WriteLine("Database already seeded. Skipping seed operation.");
                return;
            }

            Console.WriteLine("Starting database seeding...");

            try
            {
                SeedRoles(context);
                SeedUsers(context);
                SeedUserRoles(context);
                SeedTags(context);
                SeedInfluencers(context);
                SeedInfluencerTags(context);
                SeedSocialProfiles(context);
                SeedCampaigns(context);
                SeedCampaignDeliverables(context);
                SeedContentSubmissions(context);
                SeedContentVersions(context);
                SeedApprovals(context);
                SeedPerformanceMetrics(context);
                SeedCollaborations(context);

                context.SaveChanges();
                Console.WriteLine("✓ Database seeding completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error during seeding: {ex.Message}");
                throw;
            }
        }

        private static void SeedRoles(AppDbContext context)
        {
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Brand" },
                new Role { Id = 3, Name = "Influencer" }
            };

            context.Roles.AddRange(roles);
            Console.WriteLine("  → Seeded 3 roles");
        }

        private static void SeedUsers(AppDbContext context)
        {
            var users = new List<User>
            {
                // Admin User
                new User
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000001"),
                    Email = "admin@beautysquad.com",
                    FullName = "Admin User",
                    PasswordHash = "$2a$11$ZCNq6/wWFvgqr9WKvhLmfe8tZxEVhNsL1pVwYNXGpgTKpNKzYfPLK", // Admin@123
                    IsActive = true,
                    LastLoginAt = null
                },
                // Brand Users
                new User
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000001"),
                    Email = "brand@glowskin.com",
                    FullName = "Sarah Mitchell - GlowSkin",
                    PasswordHash = "$2a$11$8YQvhVkX.nj6GhZpLxFqAOl9kl2m3n4o5p6q7r8s9t0u1v2w3x4y5z6", // Brand@123
                    IsActive = true,
                    LastLoginAt = null
                },
                new User
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000002"),
                    Email = "brand@beautyessence.com",
                    FullName = "James Chen - BeautyEssence",
                    PasswordHash = "$2a$11$9ZRwixWF.no7HlaQlxGrBPm0lm3n4o5p6q7r8s9t0u1v2w3x4y5z7", // Brand@456
                    IsActive = true,
                    LastLoginAt = null
                },
                // Influencer Users
                new User
                {
                    Id = Guid.Parse("i0000000-0000-0000-0000-000000000001"),
                    Email = "emma.watson@email.com",
                    FullName = "Emma Watson",
                    PasswordHash = "$2a$11$7YPvguUY.ml5GkaJjlxGr@OkNklo4Np5Rq7Rs8Stu1Uv2Wx3Xy4Yz8", // Influencer@123
                    IsActive = true,
                    LastLoginAt = null
                },
                new User
                {
                    Id = Guid.Parse("i0000000-0000-0000-0000-000000000002"),
                    Email = "liam.jones@email.com",
                    FullName = "Liam Jones",
                    PasswordHash = "$2a$11$8ZQwhVxZ.nm6HlbRmxHsCSp1Lmo5Oq6Rr7Rs8Stu1Uv2Wx3Xy4Yz9", // Influencer@456
                    IsActive = true,
                    LastLoginAt = null
                },
                new User
                {
                    Id = Guid.Parse("i0000000-0000-0000-0000-000000000003"),
                    Email = "sophia.lee@email.com",
                    FullName = "Sophia Lee",
                    PasswordHash = "$2a$11$9aRxiwYa.nn7ImcSnyItDTq2Mnp6Pr7Ss8Tt9Uu2Vv3Wx4Xy5Za0", // Influencer@789
                    IsActive = true,
                    LastLoginAt = null
                },
                new User
                {
                    Id = Guid.Parse("i0000000-0000-0000-0000-000000000004"),
                    Email = "marcus.brown@email.com",
                    FullName = "Marcus Brown",
                    PasswordHash = "$2a$11$6bSyixZb.lo8JndTpzJuEUr3Noq7Qs8Tt9Uu2Vv3Wx4Xy5Za6Zb1", // Influencer@012
                    IsActive = true,
                    LastLoginAt = null
                },
                new User
                {
                    Id = Guid.Parse("i0000000-0000-0000-0000-000000000005"),
                    Email = "olivia.patel@email.com",
                    FullName = "Olivia Patel",
                    PasswordHash = "$2a$11$7cTzjaac.mp9KoeTqaKvFVs4Opr8Rt9Uu2Vv3Wx4Xy5Za6Zb1Zc2", // Influencer@345
                    IsActive = true,
                    LastLoginAt = null
                }
            };

            context.Users.AddRange(users);
            Console.WriteLine("  → Seeded 8 users (1 Admin, 2 Brand, 5 Influencers)");
        }

        private static void SeedUserRoles(AppDbContext context)
        {
            var userRoles = new List<UserRole>
            {
                // Admin role
                new UserRole
                {
                    UserId = Guid.Parse("a0000000-0000-0000-0000-000000000001"),
                    RoleId = 1
                },
                // Brand roles
                new UserRole
                {
                    UserId = Guid.Parse("b0000000-0000-0000-0000-000000000001"),
                    RoleId = 2
                },
                new UserRole
                {
                    UserId = Guid.Parse("b0000000-0000-0000-0000-000000000002"),
                    RoleId = 2
                },
                // Influencer roles
                new UserRole
                {
                    UserId = Guid.Parse("i0000000-0000-0000-0000-000000000001"),
                    RoleId = 3
                },
                new UserRole
                {
                    UserId = Guid.Parse("i0000000-0000-0000-0000-000000000002"),
                    RoleId = 3
                },
                new UserRole
                {
                    UserId = Guid.Parse("i0000000-0000-0000-0000-000000000003"),
                    RoleId = 3
                },
                new UserRole
                {
                    UserId = Guid.Parse("i0000000-0000-0000-0000-000000000004"),
                    RoleId = 3
                },
                new UserRole
                {
                    UserId = Guid.Parse("i0000000-0000-0000-0000-000000000005"),
                    RoleId = 3
                }
            };

            context.UserRoles.AddRange(userRoles);
            Console.WriteLine("  → Seeded 8 user-role assignments");
        }

        private static void SeedTags(AppDbContext context)
        {
            var tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "Skincare" },
                new Tag { Id = 2, Name = "Makeup" },
                new Tag { Id = 3, Name = "Haircare" },
                new Tag { Id = 4, Name = "Wellness" },
                new Tag { Id = 5, Name = "Beauty" },
                new Tag { Id = 6, Name = "Eco-Friendly" },
                new Tag { Id = 7, Name = "Luxury" },
                new Tag { Id = 8, Name = "Affordable" },
                new Tag { Id = 9, Name = "Natural" },
                new Tag { Id = 10, Name = "Vegan" }
            };

            context.Tags.AddRange(tags);
            Console.WriteLine("  → Seeded 10 tags");
        }

        private static void SeedInfluencers(AppDbContext context)
        {
            var influencers = new List<Influencer>
            {
                new Influencer
                {
                    Id = Guid.Parse("inf0000000-0000-0000-0000-000000000001"),
                    FullName = "Emma Watson",
                    Bio = "Beauty and skincare enthusiast with 250K followers. Specializing in eco-friendly and sustainable beauty products.",
                    Email = "emma.watson@email.com",
                    Phone = "+1-555-0101",
                    Geography = "Los Angeles, USA",
                    AdvocacyStatus = AdvocacyStatus.Active,
                    Notes = "High engagement rate, verified across all platforms",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    ModifiedAt = DateTime.UtcNow.AddDays(-30)
                },
                new Influencer
                {
                    Id = Guid.Parse("inf0000000-0000-0000-0000-000000000002"),
                    FullName = "Liam Jones",
                    Bio = "Men's grooming expert. 180K TikTok followers, trending content creator.",
                    Email = "liam.jones@email.com",
                    Phone = "+1-555-0102",
                    Geography = "New York, USA",
                    AdvocacyStatus = AdvocacyStatus.Active,
                    Notes = "Strong TikTok presence, great for viral campaigns",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-25),
                    ModifiedAt = DateTime.UtcNow.AddDays(-25)
                },
                new Influencer
                {
                    Id = Guid.Parse("inf0000000-0000-0000-0000-000000000003"),
                    FullName = "Sophia Lee",
                    Bio = "Luxury beauty and fashion influencer. 500K Instagram followers. Brand partnerships.",
                    Email = "sophia.lee@email.com",
                    Phone = "+1-555-0103",
                    Geography = "Toronto, Canada",
                    AdvocacyStatus = AdvocacyStatus.Active,
                    Notes = "Premium brand collaborations, luxury segment specialist",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-14),
                    ModifiedAt = DateTime.UtcNow.AddDays(-14)
                },
                new Influencer
                {
                    Id = Guid.Parse("inf0000000-0000-0000-0000-000000000004"),
                    FullName = "Marcus Brown",
                    Bio = "Inclusive beauty advocate. Celebrating diversity in cosmetics.",
                    Email = "marcus.brown@email.com",
                    Phone = "+1-555-0104",
                    Geography = "Atlanta, USA",
                    AdvocacyStatus = AdvocacyStatus.Active,
                    Notes = "Strong community engagement, diversity-focused content",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-9),
                    ModifiedAt = DateTime.UtcNow.AddDays(-9)
                },
                new Influencer
                {
                    Id = Guid.Parse("inf0000000-0000-0000-0000-000000000005"),
                    FullName = "Olivia Patel",
                    Bio = "Wellness and natural beauty creator. 300K TikTok followers.",
                    Email = "olivia.patel@email.com",
                    Phone = "+1-555-0105",
                    Geography = "Austin, USA",
                    AdvocacyStatus = AdvocacyStatus.Prospect,
                    Notes = "Growing creator, potential for long-term partnerships",
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-4),
                    ModifiedAt = DateTime.UtcNow.AddDays(-4)
                }
            };

            context.Influencers.AddRange(influencers);
            Console.WriteLine("  → Seeded 5 influencers");
        }

        private static void SeedInfluencerTags(AppDbContext context)
        {
            var influencerTags = new List<InfluencerTag>
            {
                // Emma Watson (Skincare, Eco-Friendly, Natural)
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"), TagId = 1 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"), TagId = 6 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"), TagId = 9 },
                // Liam Jones (Haircare, Makeup, Affordable)
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"), TagId = 3 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"), TagId = 2 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"), TagId = 8 },
                // Sophia Lee (Makeup, Luxury, Beauty)
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000003"), TagId = 2 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000003"), TagId = 7 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000003"), TagId = 5 },
                // Marcus Brown (Beauty, Makeup, Vegan)
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000004"), TagId = 5 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000004"), TagId = 2 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000004"), TagId = 10 },
                // Olivia Patel (Wellness, Natural, Vegan)
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"), TagId = 4 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"), TagId = 9 },
                new InfluencerTag { InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"), TagId = 10 }
            };

            context.InfluencerTags.AddRange(influencerTags);
            Console.WriteLine("  → Seeded 15 influencer-tag assignments");
        }

        private static void SeedSocialProfiles(AppDbContext context)
        {
            var socialProfiles = new List<SocialProfile>
            {
                // Emma Watson
                new SocialProfile
                {
                    Id = 1,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"),
                    Platform = SocialPlatform.Instagram,
                    Handle = "@emma.watson.beauty",
                    Url = "https://instagram.com/emma.watson.beauty",
                    Followers = 250000,
                    EngagementRate = 8.5,
                    IsVerified = true
                },
                new SocialProfile
                {
                    Id = 2,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"),
                    Platform = SocialPlatform.TikTok,
                    Handle = "@emmawatsonbeauty",
                    Url = "https://tiktok.com/@emmawatsonbeauty",
                    Followers = 180000,
                    EngagementRate = 12.3,
                    IsVerified = true
                },
                new SocialProfile
                {
                    Id = 3,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"),
                    Platform = SocialPlatform.YouTube,
                    Handle = "@EmmaWatsonBeauty",
                    Url = "https://youtube.com/@EmmaWatsonBeauty",
                    Followers = 95000,
                    EngagementRate = 7.8,
                    IsVerified = true
                },
                // Liam Jones
                new SocialProfile
                {
                    Id = 4,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"),
                    Platform = SocialPlatform.TikTok,
                    Handle = "@liamjonesgrooming",
                    Url = "https://tiktok.com/@liamjonesgrooming",
                    Followers = 180000,
                    EngagementRate = 15.2,
                    IsVerified = true
                },
                new SocialProfile
                {
                    Id = 5,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"),
                    Platform = SocialPlatform.Instagram,
                    Handle = "@liam.jones.grooming",
                    Url = "https://instagram.com/liam.jones.grooming",
                    Followers = 120000,
                    EngagementRate = 9.1,
                    IsVerified = false
                },
                new SocialProfile
                {
                    Id = 6,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"),
                    Platform = SocialPlatform.X,
                    Handle = "@LiamJones",
                    Url = "https://x.com/LiamJones",
                    Followers = 85000,
                    EngagementRate = 6.5,
                    IsVerified = false
                },
                // Sophia Lee
                new SocialProfile
                {
                    Id = 7,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000003"),
                    Platform = SocialPlatform.Instagram,
                    Handle = "@sophialeeluxury",
                    Url = "https://instagram.com/sophialeeluxury",
                    Followers = 500000,
                    EngagementRate = 11.2,
                    IsVerified = true
                },
                new SocialProfile
                {
                    Id = 8,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000003"),
                    Platform = SocialPlatform.YouTube,
                    Handle = "@SophiaLeeLuxury",
                    Url = "https://youtube.com/@SophiaLeeLuxury",
                    Followers = 250000,
                    EngagementRate = 9.5,
                    IsVerified = true
                },
                new SocialProfile
                {
                    Id = 9,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000003"),
                    Platform = SocialPlatform.TikTok,
                    Handle = "@sophialeeluxury",
                    Url = "https://tiktok.com/@sophialeeluxury",
                    Followers = 160000,
                    EngagementRate = 13.8,
                    IsVerified = true
                },
                // Marcus Brown
                new SocialProfile
                {
                    Id = 10,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000004"),
                    Platform = SocialPlatform.Instagram,
                    Handle = "@marcusbrownbeauty",
                    Url = "https://instagram.com/marcusbrownbeauty",
                    Followers = 220000,
                    EngagementRate = 10.6,
                    IsVerified = true
                },
                new SocialProfile
                {
                    Id = 11,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000004"),
                    Platform = SocialPlatform.TikTok,
                    Handle = "@marcusbrowntok",
                    Url = "https://tiktok.com/@marcusbrowntok",
                    Followers = 195000,
                    EngagementRate = 14.2,
                    IsVerified = true
                },
                new SocialProfile
                {
                    Id = 12,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000004"),
                    Platform = SocialPlatform.YouTube,
                    Handle = "@MarcusBrownBeauty",
                    Url = "https://youtube.com/@MarcusBrownBeauty",
                    Followers = 75000,
                    EngagementRate = 8.3,
                    IsVerified = false
                },
                // Olivia Patel
                new SocialProfile
                {
                    Id = 13,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"),
                    Platform = SocialPlatform.TikTok,
                    Handle = "@olivia.wellness",
                    Url = "https://tiktok.com/@olivia.wellness",
                    Followers = 300000,
                    EngagementRate = 16.5,
                    IsVerified = true
                },
                new SocialProfile
                {
                    Id = 14,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"),
                    Platform = SocialPlatform.Instagram,
                    Handle = "@olivia.patel.wellness",
                    Url = "https://instagram.com/olivia.patel.wellness",
                    Followers = 150000,
                    EngagementRate = 11.8,
                    IsVerified = false
                },
                new SocialProfile
                {
                    Id = 15,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"),
                    Platform = SocialPlatform.Other,
                    Handle = "@OliviaWellness",
                    Url = "https://threads.net/@OliviaWellness",
                    Followers = 45000,
                    EngagementRate = 9.2,
                    IsVerified = false
                }
            };

            context.SocialProfiles.AddRange(socialProfiles);
            Console.WriteLine("  → Seeded 15 social profiles");
        }

        private static void SeedCampaigns(AppDbContext context)
        {
            var campaigns = new List<Campaign>
            {
                new Campaign
                {
                    Id = Guid.Parse("camp000-0000-0000-0000-000000000001"),
                    Name = "GlowSkin Summer Radiance Campaign",
                    Brief = "Launch our new SPF 50+ sunscreen product line for summer season",
                    Objectives = "Increase brand awareness among 18-35 age group, drive product sales in Q2",
                    StartDate = DateTime.UtcNow.AddMonths(1),
                    EndDate = DateTime.UtcNow.AddMonths(4),
                    Budget = 50000,
                    Status = CampaignStatus.Active,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                },
                new Campaign
                {
                    Id = Guid.Parse("camp000-0000-0000-0000-000000000002"),
                    Name = "BeautyEssence Wellness Collective",
                    Brief = "Partnering with wellness influencers for our new wellness serum launch",
                    Objectives = "Reach wellness audience, build eco-conscious brand image",
                    StartDate = DateTime.UtcNow.AddMonths(1).AddDays(-15),
                    EndDate = DateTime.UtcNow.AddMonths(5),
                    Budget = 75000,
                    Status = CampaignStatus.Active,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new Campaign
                {
                    Id = Guid.Parse("camp000-0000-0000-0000-000000000003"),
                    Name = "GlowSkin Spring Skincare Series",
                    Brief = "Educational content series about spring skincare routines",
                    Objectives = "Position brand as skincare expert, increase engagement",
                    StartDate = DateTime.UtcNow.AddDays(-8),
                    EndDate = DateTime.UtcNow.AddMonths(2).AddDays(-8),
                    Budget = 35000,
                    Status = CampaignStatus.Active,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-14)
                }
            };

            context.Campaigns.AddRange(campaigns);
            Console.WriteLine("  → Seeded 3 campaigns");
        }

        private static void SeedCampaignDeliverables(AppDbContext context)
        {
            var deliverables = new List<CampaignDeliverable>
            {
                // Campaign 1: GlowSkin Summer Radiance
                new CampaignDeliverable
                {
                    Id = Guid.Parse("deliv00-0000-0000-0000-000000000001"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000001"),
                    Type = DeliverableType.Post,
                    Description = "Instagram carousel post showcasing SPF 50+ application tips and before/after",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    AssignedInfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"),
                    Status = DeliverableStatus.InProgress
                },
                new CampaignDeliverable
                {
                    Id = Guid.Parse("deliv00-0000-0000-0000-000000000002"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000001"),
                    Type = DeliverableType.Post,
                    Description = "Instagram feed post with product shot and personal summer routine",
                    DueDate = DateTime.UtcNow.AddDays(12),
                    AssignedInfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000003"),
                    Status = DeliverableStatus.InProgress
                },
                new CampaignDeliverable
                {
                    Id = Guid.Parse("deliv00-0000-0000-0000-000000000003"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000001"),
                    Type = DeliverableType.Story,
                    Description = "5-part Instagram story series of daily sunscreen application",
                    DueDate = DateTime.UtcNow.AddDays(18),
                    AssignedInfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000004"),
                    Status = DeliverableStatus.Planned
                },
                // Campaign 2: BeautyEssence Wellness
                new CampaignDeliverable
                {
                    Id = Guid.Parse("deliv00-0000-0000-0000-000000000004"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000002"),
                    Type = DeliverableType.UGC,
                    Description = "User-generated TikTok video of wellness serum in daily routine",
                    DueDate = DateTime.UtcNow.AddDays(14),
                    AssignedInfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"),
                    Status = DeliverableStatus.InProgress
                },
                new CampaignDeliverable
                {
                    Id = Guid.Parse("deliv00-0000-0000-0000-000000000005"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000002"),
                    Type = DeliverableType.Review,
                    Description = "Detailed wellness serum product review with benefits breakdown",
                    DueDate = DateTime.UtcNow.AddDays(23),
                    AssignedInfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"),
                    Status = DeliverableStatus.Planned
                },
                // Campaign 3: GlowSkin Spring Series
                new CampaignDeliverable
                {
                    Id = Guid.Parse("deliv00-0000-0000-0000-000000000006"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000003"),
                    Type = DeliverableType.UGC,
                    Description = "DIY spring skincare mask tutorial",
                    DueDate = DateTime.UtcNow,
                    AssignedInfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"),
                    Status = DeliverableStatus.Submitted
                }
            };

            context.CampaignDeliverables.AddRange(deliverables);
            Console.WriteLine("  → Seeded 6 campaign deliverables");
        }

        private static void SeedContentSubmissions(AppDbContext context)
        {
            var submissions = new List<ContentSubmission>
            {
                new ContentSubmission
                {
                    Id = Guid.Parse("sub0000000-0000-0000-0000-000000000001"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000001"),
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"),
                    DeliverableId = Guid.Parse("deliv00-0000-0000-0000-000000000001"),
                    Title = "SPF 50+ Application Tips & Tricks",
                    Caption = "Protecting your skin this summer with our new SPF 50+ formula! ☀️ This carousel post breaks down the proper application techniques for maximum UV protection. Swipe through to learn my 5-step summer skincare routine. ✨ #GlowSkin #SummerSkincare #SPF #SkinProtection",
                    AssetPath = "/content/submissions/sub001/carousel-post-v2.jpg",
                    State = SubmissionState.Submitted,
                    CurrentVersionNumber = 2,
                    SubmittedAt = DateTime.UtcNow.AddDays(-5)
                },
                new ContentSubmission
                {
                    Id = Guid.Parse("sub0000000-0000-0000-0000-000000000002"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000001"),
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000003"),
                    DeliverableId = Guid.Parse("deliv00-0000-0000-0000-000000000002"),
                    Title = "Luxury Summer Skincare Essentials",
                    Caption = "My non-negotiable summer beauty essentials featuring our new GlowSkin SPF collection. Luxury skincare that protects AND nourishes your skin. Get ready for your best summer glow 💫 #GlowSkin #LuxuryBeauty #SummerBeauty",
                    AssetPath = "/content/submissions/sub002/luxury-post-v1.jpg",
                    State = SubmissionState.Draft,
                    CurrentVersionNumber = 1,
                    SubmittedAt = null
                },
                new ContentSubmission
                {
                    Id = Guid.Parse("sub0000000-0000-0000-0000-000000000003"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000002"),
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"),
                    DeliverableId = Guid.Parse("deliv00-0000-0000-0000-000000000004"),
                    Title = "Wellness Serum in My Daily Routine",
                    Caption = "Integrated the new BeautyEssence wellness serum into my morning routine. The results after just 2 weeks are incredible! 🌿 #WellnessBeauty #BeautyEssence #NaturalBeauty #SkincareTok",
                    AssetPath = "/content/submissions/sub003/wellness-tiktok-v1.mp4",
                    State = SubmissionState.Submitted,
                    CurrentVersionNumber = 1,
                    SubmittedAt = DateTime.UtcNow.AddDays(-2)
                },
                new ContentSubmission
                {
                    Id = Guid.Parse("sub0000000-0000-0000-0000-000000000004"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000003"),
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"),
                    DeliverableId = Guid.Parse("deliv00-0000-0000-0000-000000000006"),
                    Title = "DIY Spring Skincare Mask Tutorial",
                    Caption = "Creating the ultimate natural spring skincare mask using ingredients from my pantry. This tutorial shows you exactly how to create a hydrating mask perfect for spring weather transitions. Copy my recipe! 💚",
                    AssetPath = "/content/submissions/sub004/diy-mask-tutorial-v2.mp4",
                    State = SubmissionState.Submitted,
                    CurrentVersionNumber = 2,
                    SubmittedAt = DateTime.UtcNow.AddDays(-3)
                }
            };

            context.ContentSubmissions.AddRange(submissions);
            Console.WriteLine("  → Seeded 4 content submissions");
        }

        private static void SeedContentVersions(AppDbContext context)
        {
            var versions = new List<ContentVersion>
            {
                // Version history for Submission 1
                new ContentVersion
                {
                    Id = Guid.Parse("ver0000000-0000-0000-0000-000000000001"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000001"),
                    VersionNumber = 1,
                    Caption = "Protecting your skin this summer with our new SPF 50+ formula! ☀️ Swipe through to learn my summer skincare routine. #GlowSkin",
                    AssetPath = "/content/submissions/sub001/carousel-post-v1.jpg",
                    CreatedAt = DateTime.UtcNow.AddDays(-6),
                    CreatedByUserId = Guid.Parse("i0000000-0000-0000-0000-000000000001")
                },
                new ContentVersion
                {
                    Id = Guid.Parse("ver0000000-0000-0000-0000-000000000002"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000001"),
                    VersionNumber = 2,
                    Caption = "Protecting your skin this summer with our new SPF 50+ formula! ☀️ This carousel post breaks down the proper application techniques for maximum UV protection. Swipe through to learn my 5-step summer skincare routine. ✨ #GlowSkin #SummerSkincare #SPF #SkinProtection",
                    AssetPath = "/content/submissions/sub001/carousel-post-v2.jpg",
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    CreatedByUserId = Guid.Parse("i0000000-0000-0000-0000-000000000001")
                },
                // Version history for Submission 3
                new ContentVersion
                {
                    Id = Guid.Parse("ver0000000-0000-0000-0000-000000000003"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000003"),
                    VersionNumber = 1,
                    Caption = "Integrated the new BeautyEssence wellness serum into my routine. The results are incredible! 🌿 #WellnessBeauty",
                    AssetPath = "/content/submissions/sub003/wellness-tiktok-v1.mp4",
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    CreatedByUserId = Guid.Parse("i0000000-0000-0000-0000-000000000005")
                },
                // Version history for Submission 4
                new ContentVersion
                {
                    Id = Guid.Parse("ver0000000-0000-0000-0000-000000000004"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000004"),
                    VersionNumber = 1,
                    Caption = "Creating the ultimate spring skincare mask using pantry ingredients. Follow my recipe! 💚",
                    AssetPath = "/content/submissions/sub004/diy-mask-tutorial-v1.mp4",
                    CreatedAt = DateTime.UtcNow.AddDays(-4),
                    CreatedByUserId = Guid.Parse("i0000000-0000-0000-0000-000000000002")
                },
                new ContentVersion
                {
                    Id = Guid.Parse("ver0000000-0000-0000-0000-000000000005"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000004"),
                    VersionNumber = 2,
                    Caption = "Creating the ultimate natural spring skincare mask using ingredients from my pantry. This tutorial shows you exactly how to create a hydrating mask perfect for spring weather transitions. Copy my recipe! 💚",
                    AssetPath = "/content/submissions/sub004/diy-mask-tutorial-v2.mp4",
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    CreatedByUserId = Guid.Parse("i0000000-0000-0000-0000-000000000002")
                }
            };

            context.ContentVersions.AddRange(versions);
            Console.WriteLine("  → Seeded 5 content versions");
        }

        private static void SeedApprovals(AppDbContext context)
        {
            var approvals = new List<Approval>
            {
                new Approval
                {
                    Id = Guid.Parse("appr0000000-0000-0000-0000-000000000001"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000001"),
                    ReviewerUserId = Guid.Parse("b0000000-0000-0000-0000-000000000001"),
                    Decision = ApprovalDecision.Approved,
                    Comments = "Perfect carousel! Great visual content and excellent caption. Approved for posting on 2024-03-20.",
                    DecidedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Approval
                {
                    Id = Guid.Parse("appr0000000-0000-0000-0000-000000000002"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000003"),
                    ReviewerUserId = Guid.Parse("b0000000-0000-0000-0000-000000000002"),
                    Decision = ApprovalDecision.Approved,
                    Comments = "Excellent TikTok content! Love how natural this feels. Approved for Friday posting.",
                    DecidedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Approval
                {
                    Id = Guid.Parse("appr0000000-0000-0000-0000-000000000003"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000004"),
                    ReviewerUserId = Guid.Parse("b0000000-0000-0000-0000-000000000001"),
                    Decision = ApprovalDecision.Approved,
                    Comments = "Love this DIY tutorial! Very engaging and educational. Approved. Please add product links in the bio.",
                    DecidedAt = DateTime.UtcNow.AddDays(-2)
                }
            };

            context.Approvals.AddRange(approvals);
            Console.WriteLine("  → Seeded 3 approvals");
        }

        private static void SeedPerformanceMetrics(AppDbContext context)
        {
            var metrics = new List<PerformanceMetric>
            {
                new PerformanceMetric
                {
                    Id = Guid.Parse("perf0000000-0000-0000-0000-000000000001"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000001"),
                    Reach = 45230,
                    Engagements = 3847,
                    Saves = 892,
                    Shares = 456,
                    Clicks = 285,
                    Conversions = 12,
                    CapturedAt = DateTime.UtcNow.AddDays(-2)
                },
                new PerformanceMetric
                {
                    Id = Guid.Parse("perf0000000-0000-0000-0000-000000000002"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000003"),
                    Reach = 89450,
                    Engagements = 8923,
                    Saves = 2145,
                    Shares = 1203,
                    Clicks = 745,
                    Conversions = 34,
                    CapturedAt = DateTime.UtcNow.AddDays(-1)
                },
                new PerformanceMetric
                {
                    Id = Guid.Parse("perf0000000-0000-0000-0000-000000000003"),
                    SubmissionId = Guid.Parse("sub0000000-0000-0000-0000-000000000004"),
                    Reach = 56780,
                    Engagements = 5234,
                    Saves = 1456,
                    Shares = 782,
                    Clicks = 423,
                    Conversions = 18,
                    CapturedAt = DateTime.UtcNow
                }
            };

            context.PerformanceMetrics.AddRange(metrics);
            Console.WriteLine("  → Seeded 3 performance metrics");
        }

        private static void SeedCollaborations(AppDbContext context)
        {
            var collaborations = new List<Collaboration>
            {
                new Collaboration
                {
                    Id = 1,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000001"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000001"),
                    Title = "GlowSkin Summer Campaign - Shoot Day",
                    Date = DateTime.UtcNow.AddDays(-10),
                    OutcomeNotes = "Successful photoshoot. Emma provided excellent product recommendations and engagement during shoot. Content ready for posting."
                },
                new Collaboration
                {
                    Id = 2,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000005"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000002"),
                    Title = "BeautyEssence Wellness Event",
                    Date = DateTime.UtcNow.AddDays(-5),
                    OutcomeNotes = "Virtual event with 500+ attendees. Olivia shared her wellness routine and drove significant interest in the serum product."
                },
                new Collaboration
                {
                    Id = 3,
                    InfluencerId = Guid.Parse("inf0000000-0000-0000-0000-000000000002"),
                    CampaignId = Guid.Parse("camp000-0000-0000-0000-000000000003"),
                    Title = "GlowSkin Spring Campaign - Partner Meeting",
                    Date = DateTime.UtcNow.AddDays(-9),
                    OutcomeNotes = "Discussed campaign direction and content themes. Liam excited to create educational UGC content for the spring series."
                }
            };

            context.Collaborations.AddRange(collaborations);
            Console.WriteLine("  → Seeded 3 collaborations");
        }
    }
}
