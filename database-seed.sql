-- =============================================================================
-- BeautySquad Database Seed Script
-- Populates test data for all roles and tables
-- =============================================================================

-- Clear existing data (Optional - Uncomment if needed)
-- DELETE FROM PerformanceMetrics;
-- DELETE FROM Approvals;
-- DELETE FROM ContentVersions;
-- DELETE FROM ContentSubmissions;
-- DELETE FROM CampaignDeliverables;
-- DELETE FROM Campaigns;
-- DELETE FROM Collaborations;
-- DELETE FROM SocialProfiles;
-- DELETE FROM InfluencerTags;
-- DELETE FROM Influencers;
-- DELETE FROM UserRoles;
-- DELETE FROM Users;
-- DELETE FROM Tags;
-- DELETE FROM Roles;

-- =============================================================================
-- 1. ROLES (Core Roles)
-- =============================================================================
SET IDENTITY_INSERT Roles ON;
INSERT INTO Roles (Id, Name) VALUES 
(1, 'Admin'),
(2, 'Brand'),
(3, 'Influencer');
SET IDENTITY_INSERT Roles OFF;

-- =============================================================================
-- 2. TEST USERS
-- =============================================================================

-- Admin User
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt)
VALUES 
(
  'a0000000-0000-0000-0000-000000000001',
  'admin@beautysquad.com',
  -- Password: Admin@123 (bcrypt hash)
  '$2a$11$n2RU7pL.g1bOkJ9S3yI8N.C0AeF7rK6jJ5mN0oP1qS2tU3vW4xY5z',
  'Admin User',
  1,
  NULL
);

-- Brand Users (2 brands)
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt)
VALUES 
(
  'b0000000-0000-0000-0000-000000000001',
  'brand@glowskin.com',
  -- Password: Brand@123
  '$2a$11$m1QT6oK.f0anJi8R2xH7M.B9AdE6qJ5iI4lM9nO0pQ1rS2uV3wX4y',
  'Sarah Mitchell - GlowSkin',
  1,
  NULL
),
(
  'b0000000-0000-0000-0000-000000000002',
  'brand@beautyessence.com',
  -- Password: Brand@456
  '$2a$11$l0PS5nJ.e9ZmHh7Q1wG6L.A8AcD5pI4hH3kL8mN9oO0pQ1rS2tU3v',
  'James Chen - BeautyEssence',
  1,
  NULL
);

-- Influencer Users (5 influencers)
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt)
VALUES 
(
  'i0000000-0000-0000-0000-000000000001',
  'emma.watson@email.com',
  -- Password: Influencer@123
  '$2a$11$w8VsZ3tN.l6gTpXqYnM8P.H5DjK2wQ7mN9oR4sT1uV5xW2yZ8aB9c',
  'Emma Watson',
  1,
  NULL
),
(
  'i0000000-0000-0000-0000-000000000002',
  'liam.jones@email.com',
  -- Password: Influencer@456
  '$2a$11$v7UsY2sM.k5fSoWpXmL7O.G4CiJ1vP6lM8nQ3rS0tU4wV1xY7zC8d',
  'Liam Jones',
  1,
  NULL
),
(
  'i0000000-0000-0000-0000-000000000003',
  'sophia.lee@email.com',
  -- Password: Influencer@789
  '$2a$11$u6TrX1rL.j4eRnVoWlK6N.F3BhI0uO5kL7mP2qRzS3tU0vW6yB7e',
  'Sophia Lee',
  1,
  NULL
),
(
  'i0000000-0000-0000-0000-000000000004',
  'marcus.brown@email.com',
  -- Password: Influencer@012
  '$2a$11$t5SqW0qK.i3dQmUnVkJ5M.E2AgH9tN4jK6lO1pQyR2sT9uV5wA6f',
  'Marcus Brown',
  1,
  NULL
),
(
  'i0000000-0000-0000-0000-000000000005',
  'olivia.patel@email.com',
  -- Password: Influencer@345
  '$2a$11$s4RpV9pJ.h2cPlTmUjI4L.D1AfG8sM3iJ5kN0oZqR1rS8tU4vZ5g',
  'Olivia Patel',
  1,
  NULL
);

-- =============================================================================
-- 3. USER ROLES (Assign roles to users)
-- =============================================================================

-- Admin role
INSERT INTO UserRoles (UserId, RoleId)
VALUES ('a0000000-0000-0000-0000-000000000001', 1);

-- Brand roles
INSERT INTO UserRoles (UserId, RoleId)
VALUES 
('b0000000-0000-0000-0000-000000000001', 2),
('b0000000-0000-0000-0000-000000000002', 2);

-- Influencer roles
INSERT INTO UserRoles (UserId, RoleId)
VALUES 
('i0000000-0000-0000-0000-000000000001', 3),
('i0000000-0000-0000-0000-000000000002', 3),
('i0000000-0000-0000-0000-000000000003', 3),
('i0000000-0000-0000-0000-000000000004', 3),
('i0000000-0000-0000-0000-000000000005', 3);

-- =============================================================================
-- 4. TAGS (Content Categories)
-- =============================================================================
SET IDENTITY_INSERT Tags ON;
INSERT INTO Tags (Id, Name) VALUES 
(1, 'Skincare'),
(2, 'Makeup'),
(3, 'Haircare'),
(4, 'Wellness'),
(5, 'Beauty'),
(6, 'Eco-Friendly'),
(7, 'Luxury'),
(8, 'Affordable'),
(9, 'Natural'),
(10, 'Vegan');
SET IDENTITY_INSERT Tags OFF;

-- =============================================================================
-- 5. INFLUENCERS
-- =============================================================================
INSERT INTO Influencers (Id, FullName, Bio, Email, Phone, Geography, AdvocacyStatus, Notes, IsDeleted, CreatedAt, ModifiedAt)
VALUES 
(
  'inf0000000-0000-0000-0000-000000000001',
  'Emma Watson',
  'Beauty and skincare enthusiast with 250K followers. Specializing in eco-friendly and sustainable beauty products.',
  'emma.watson@email.com',
  '+1-555-0101',
  'Los Angeles, USA',
  1, -- Active
  'High engagement rate, verified across all platforms',
  0,
  CAST('2024-01-15' AS DATETIME2),
  CAST('2024-01-15' AS DATETIME2)
),
(
  'inf0000000-0000-0000-0000-000000000002',
  'Liam Jones',
  'Men''s grooming expert. 180K TikTok followers, trending content creator.',
  'liam.jones@email.com',
  '+1-555-0102',
  'New York, USA',
  1, -- Active
  'Strong TikTok presence, great for viral campaigns',
  0,
  CAST('2024-01-20' AS DATETIME2),
  CAST('2024-01-20' AS DATETIME2)
),
(
  'inf0000000-0000-0000-0000-000000000003',
  'Sophia Lee',
  'Luxury beauty and fashion influencer. 500K Instagram followers. Brand partnerships.',
  'sophia.lee@email.com',
  '+1-555-0103',
  'Toronto, Canada',
  1, -- Active
  'Premium brand collaborations, luxury segment specialist',
  0,
  CAST('2024-02-01' AS DATETIME2),
  CAST('2024-02-01' AS DATETIME2)
),
(
  'inf0000000-0000-0000-0000-000000000004',
  'Marcus Brown',
  'Inclusive beauty advocate. Celebrating diversity in cosmetics.',
  'marcus.brown@email.com',
  '+1-555-0104',
  'Atlanta, USA',
  1, -- Active
  'Strong community engagement, diversity-focused content',
  0,
  CAST('2024-02-05' AS DATETIME2),
  CAST('2024-02-05' AS DATETIME2)
),
(
  'inf0000000-0000-0000-0000-000000000005',
  'Olivia Patel',
  'Wellness and natural beauty creator. 300K TikTok followers.',
  'olivia.patel@email.com',
  '+1-555-0105',
  'Austin, USA',
  0, -- Prospect
  'Growing creator, potential for long-term partnerships',
  0,
  CAST('2024-02-10' AS DATETIME2),
  CAST('2024-02-10' AS DATETIME2)
);

-- =============================================================================
-- 6. INFLUENCER TAGS (Link influencers to content categories)
-- =============================================================================
INSERT INTO InfluencerTags (InfluencerId, TagId)
VALUES 
-- Emma Watson (Skincare, Eco-Friendly, Natural)
('inf0000000-0000-0000-0000-000000000001', 1),
('inf0000000-0000-0000-0000-000000000001', 6),
('inf0000000-0000-0000-0000-000000000001', 9),
-- Liam Jones (Haircare, Makeup, Affordable)
('inf0000000-0000-0000-0000-000000000002', 3),
('inf0000000-0000-0000-0000-000000000002', 2),
('inf0000000-0000-0000-0000-000000000002', 8),
-- Sophia Lee (Makeup, Luxury, Beauty)
('inf0000000-0000-0000-0000-000000000003', 2),
('inf0000000-0000-0000-0000-000000000003', 7),
('inf0000000-0000-0000-0000-000000000003', 5),
-- Marcus Brown (Beauty, Makeup, Vegan)
('inf0000000-0000-0000-0000-000000000004', 5),
('inf0000000-0000-0000-0000-000000000004', 2),
('inf0000000-0000-0000-0000-000000000004', 10),
-- Olivia Patel (Wellness, Natural, Vegan)
('inf0000000-0000-0000-0000-000000000005', 4),
('inf0000000-0000-0000-0000-000000000005', 9),
('inf0000000-0000-0000-0000-000000000005', 10);

-- =============================================================================
-- 7. SOCIAL PROFILES
-- =============================================================================
SET IDENTITY_INSERT SocialProfiles ON;
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified)
VALUES
-- Emma Watson
(1, 'inf0000000-0000-0000-0000-000000000001', 0, '@emma.watson.beauty', 'https://instagram.com/emma.watson.beauty', 250000, 8.5, 1),
(2, 'inf0000000-0000-0000-0000-000000000001', 1, '@emmawatsonbeauty', 'https://tiktok.com/@emmawatsonbeauty', 180000, 12.3, 1),
(3, 'inf0000000-0000-0000-0000-000000000001', 2, '@EmmaWatsonBeauty', 'https://youtube.com/@EmmaWatsonBeauty', 95000, 7.8, 1),
-- Liam Jones
(4, 'inf0000000-0000-0000-0000-000000000002', 1, '@liamjonesgrooming', 'https://tiktok.com/@liamjonesgrooming', 180000, 15.2, 1),
(5, 'inf0000000-0000-0000-0000-000000000002', 0, '@liam.jones.grooming', 'https://instagram.com/liam.jones.grooming', 120000, 9.1, 0),
(6, 'inf0000000-0000-0000-0000-000000000002', 3, '@LiamJones', 'https://x.com/LiamJones', 85000, 6.5, 0),
-- Sophia Lee
(7, 'inf0000000-0000-0000-0000-000000000003', 0, '@sophialeeluxury', 'https://instagram.com/sophialeeluxury', 500000, 11.2, 1),
(8, 'inf0000000-0000-0000-0000-000000000003', 2, '@SophiaLeeLuxury', 'https://youtube.com/@SophiaLeeLuxury', 250000, 9.5, 1),
(9, 'inf0000000-0000-0000-0000-000000000003', 1, '@sophialeeluxury', 'https://tiktok.com/@sophialeeluxury', 160000, 13.8, 1),
-- Marcus Brown
(10, 'inf0000000-0000-0000-0000-000000000004', 0, '@marcusbrownbeauty', 'https://instagram.com/marcusbrownbeauty', 220000, 10.6, 1),
(11, 'inf0000000-0000-0000-0000-000000000004', 1, '@marcusbrowntok', 'https://tiktok.com/@marcusbrowntok', 195000, 14.2, 1),
(12, 'inf0000000-0000-0000-0000-000000000004', 2, '@MarcusBrownBeauty', 'https://youtube.com/@MarcusBrownBeauty', 75000, 8.3, 0),
-- Olivia Patel
(13, 'inf0000000-0000-0000-0000-000000000005', 1, '@olivia.wellness', 'https://tiktok.com/@olivia.wellness', 300000, 16.5, 1),
(14, 'inf0000000-0000-0000-0000-000000000005', 0, '@olivia.patel.wellness', 'https://instagram.com/olivia.patel.wellness', 150000, 11.8, 0),
(15, 'inf0000000-0000-0000-0000-000000000005', 4, '@OliviaWellness', 'https://threads.net/@OliviaWellness', 45000, 9.2, 0);
SET IDENTITY_INSERT SocialProfiles OFF;

-- =============================================================================
-- 8. CAMPAIGNS
-- =============================================================================
INSERT INTO Campaigns (Id, Name, Brief, Objectives, StartDate, EndDate, Budget, Status, IsDeleted, CreatedAt)
VALUES
(
  'camp000-0000-0000-0000-000000000001',
  'GlowSkin Summer Radiance Campaign',
  'Launch our new SPF 50+ sunscreen product line for summer season',
  'Increase brand awareness among 18-35 age group, drive product sales in Q2',
  CAST('2024-03-01' AS DATETIME2),
  CAST('2024-05-31' AS DATETIME2),
  50000.00,
  1, -- Active
  0,
  CAST('2024-02-15' AS DATETIME2)
),
(
  'camp000-0000-0000-0000-000000000002',
  'BeautyEssence Wellness Collective',
  'Partnering with wellness influencers for our new wellness serum launch',
  'Reach wellness audience, build eco-conscious brand image',
  CAST('2024-03-15' AS DATETIME2),
  CAST('2024-06-30' AS DATETIME2),
  75000.00,
  1, -- Active
  0,
  CAST('2024-02-20' AS DATETIME2)
),
(
  'camp000-0000-0000-0000-000000000003',
  'GlowSkin Spring Skincare Series',
  'Educational content series about spring skincare routines',
  'Position brand as skincare expert, increase engagement',
  CAST('2024-02-20' AS DATETIME2),
  CAST('2024-04-30' AS DATETIME2),
  35000.00,
  1, -- Active
  0,
  CAST('2024-02-01' AS DATETIME2)
);

-- =============================================================================
-- 9. CAMPAIGN DELIVERABLES
-- =============================================================================
INSERT INTO CampaignDeliverables (Id, CampaignId, Type, Description, DueDate, AssignedInfluencerId, Status)
VALUES
-- Campaign 1: GlowSkin Summer Radiance
(
  'deliv00-0000-0000-0000-000000000001',
  'camp000-0000-0000-0000-000000000001',
  1, -- Post
  'Instagram carousel post showcasing SPF 50+ application tips and before/after',
  CAST('2024-03-20' AS DATETIME2),
  'inf0000000-0000-0000-0000-000000000001',
  2 -- InProgress
),
(
  'deliv00-0000-0000-0000-000000000002',
  'camp000-0000-0000-0000-000000000001',
  1, -- Post
  'Instagram feed post with product shot and personal summer routine',
  CAST('2024-03-25' AS DATETIME2),
  'inf0000000-0000-0000-0000-000000000003',
  2 -- InProgress
),
(
  'deliv00-0000-0000-0000-000000000003',
  'camp000-0000-0000-0000-000000000001',
  2, -- Story
  '5-part Instagram story series of daily sunscreen application',
  CAST('2024-04-01' AS DATETIME2),
  'inf0000000-0000-0000-0000-000000000004',
  0 -- Planned
),
-- Campaign 2: BeautyEssence Wellness
(
  'deliv00-0000-0000-0000-000000000004',
  'camp000-0000-0000-0000-000000000002',
  3, -- UGC
  'User-generated TikTok video of wellness serum in daily routine',
  CAST('2024-04-01' AS DATETIME2),
  'inf0000000-0000-0000-0000-000000000005',
  2 -- InProgress
),
(
  'deliv00-0000-0000-0000-000000000005',
  'camp000-0000-0000-0000-000000000002',
  4, -- Review
  'Detailed wellness serum product review with benefits breakdown',
  CAST('2024-04-10' AS DATETIME2),
  'inf0000000-0000-0000-0000-000000000001',
  0 -- Planned
),
-- Campaign 3: GlowSkin Spring Series
(
  'deliv00-0000-0000-0000-000000000006',
  'camp000-0000-0000-0000-000000000003',
  3, -- UGC
  'DIY spring skincare mask tutorial',
  CAST('2024-03-10' AS DATETIME2),
  'inf0000000-0000-0000-0000-000000000002',
  3 -- Submitted
);

-- =============================================================================
-- 10. CONTENT SUBMISSIONS
-- =============================================================================
INSERT INTO ContentSubmissions (Id, CampaignId, InfluencerId, DeliverableId, Title, Caption, AssetPath, State, CurrentVersionNumber, SubmittedAt)
VALUES
(
  'sub0000000-0000-0000-0000-000000000001',
  'camp000-0000-0000-0000-000000000001',
  'inf0000000-0000-0000-0000-000000000001',
  'deliv00-0000-0000-0000-000000000001',
  'SPF 50+ Application Tips & Tricks',
  'Protecting your skin this summer with our new SPF 50+ formula! ☀️ This carousel post breaks down the proper application techniques for maximum UV protection. Swipe through to learn my 5-step summer skincare routine. ✨ #GlowSkin #SummerSkincare #SPF #SkinProtection',
  '/content/submissions/sub001/carousel-post-v2.jpg',
  2, -- Submitted
  2,
  CAST('2024-03-15' AS DATETIME2)
),
(
  'sub0000000-0000-0000-0000-000000000002',
  'camp000-0000-0000-0000-000000000001',
  'inf0000000-0000-0000-0000-000000000003',
  'deliv00-0000-0000-0000-000000000002',
  'Luxury Summer Skincare Essentials',
  'My non-negotiable summer beauty essentials featuring our new GlowSkin SPF collection. Luxury skincare that protects AND nourishes your skin. Get ready for your best summer glow 💫 #GlowSkin #LuxuryBeauty #SummerBeauty',
  '/content/submissions/sub002/luxury-post-v1.jpg',
  1, -- Draft
  1,
  NULL
),
(
  'sub0000000-0000-0000-0000-000000000003',
  'camp000-0000-0000-0000-000000000002',
  'inf0000000-0000-0000-0000-000000000005',
  'deliv00-0000-0000-0000-000000000004',
  'Wellness Serum in My Daily Routine',
  'Integrated the new BeautyEssence wellness serum into my morning routine. The results after just 2 weeks are incredible! 🌿 #WellnessBeauty #BeautyEssence #NaturalBeauty #SkincareTok',
  '/content/submissions/sub003/wellness-tiktok-v1.mp4',
  2, -- Submitted
  1,
  CAST('2024-04-02' AS DATETIME2)
),
(
  'sub0000000-0000-0000-0000-000000000004',
  'camp000-0000-0000-0000-000000000003',
  'inf0000000-0000-0000-0000-000000000002',
  'deliv00-0000-0000-0000-000000000006',
  'DIY Spring Skincare Mask Tutorial',
  'Creating the ultimate natural spring skincare mask using ingredients from my pantry. This tutorial shows you exactly how to create a hydrating mask perfect for spring weather transitions. Copy my recipe! 💚',
  '/content/submissions/sub004/diy-mask-tutorial-v2.mp4',
  2, -- Submitted
  2,
  CAST('2024-03-08' AS DATETIME2)
);

-- =============================================================================
-- 11. CONTENT VERSIONS
-- =============================================================================
INSERT INTO ContentVersions (Id, SubmissionId, VersionNumber, Caption, AssetPath, CreatedAt, CreatedByUserId)
VALUES
-- Version history for Submission 1 (SPF Application Tips)
(
  'ver0000000-0000-0000-0000-000000000001',
  'sub0000000-0000-0000-0000-000000000001',
  1,
  'Protecting your skin this summer with our new SPF 50+ formula! ☀️ Swipe through to learn my summer skincare routine. #GlowSkin',
  '/content/submissions/sub001/carousel-post-v1.jpg',
  CAST('2024-03-14' AS DATETIME2),
  'i0000000-0000-0000-0000-000000000001'
),
(
  'ver0000000-0000-0000-0000-000000000002',
  'sub0000000-0000-0000-0000-000000000001',
  2,
  'Protecting your skin this summer with our new SPF 50+ formula! ☀️ This carousel post breaks down the proper application techniques for maximum UV protection. Swipe through to learn my 5-step summer skincare routine. ✨ #GlowSkin #SummerSkincare #SPF #SkinProtection',
  '/content/submissions/sub001/carousel-post-v2.jpg',
  CAST('2024-03-15' AS DATETIME2),
  'i0000000-0000-0000-0000-000000000001'
),
-- Version history for Submission 3 (Wellness Serum)
(
  'ver0000000-0000-0000-0000-000000000003',
  'sub0000000-0000-0000-0000-000000000003',
  1,
  'Integrated the new BeautyEssence wellness serum into my routine. The results are incredible! 🌿 #WellnessBeauty',
  '/content/submissions/sub003/wellness-tiktok-v1.mp4',
  CAST('2024-04-02' AS DATETIME2),
  'i0000000-0000-0000-0000-000000000005'
),
-- Version history for Submission 4 (DIY Mask Tutorial)
(
  'ver0000000-0000-0000-0000-000000000004',
  'sub0000000-0000-0000-0000-000000000004',
  1,
  'Creating the ultimate spring skincare mask using pantry ingredients. Follow my recipe! 💚',
  '/content/submissions/sub004/diy-mask-tutorial-v1.mp4',
  CAST('2024-03-07' AS DATETIME2),
  'i0000000-0000-0000-0000-000000000002'
),
(
  'ver0000000-0000-0000-0000-000000000005',
  'sub0000000-0000-0000-0000-000000000004',
  2,
  'Creating the ultimate natural spring skincare mask using ingredients from my pantry. This tutorial shows you exactly how to create a hydrating mask perfect for spring weather transitions. Copy my recipe! 💚',
  '/content/submissions/sub004/diy-mask-tutorial-v2.mp4',
  CAST('2024-03-08' AS DATETIME2),
  'i0000000-0000-0000-0000-000000000002'
);

-- =============================================================================
-- 12. APPROVALS
-- =============================================================================
INSERT INTO Approvals (Id, SubmissionId, ReviewerUserId, Decision, Comments, DecidedAt)
VALUES
(
  'appr0000000-0000-0000-0000-000000000001',
  'sub0000000-0000-0000-0000-000000000001',
  'b0000000-0000-0000-0000-000000000001',
  0, -- Approved
  'Perfect carousel! Great visual content and excellent caption. Approved for posting on 2024-03-20.',
  CAST('2024-03-15' AS DATETIME2)
),
(
  'appr0000000-0000-0000-0000-000000000002',
  'sub0000000-0000-0000-0000-000000000003',
  'b0000000-0000-0000-0000-000000000002',
  0, -- Approved
  'Excellent TikTok content! Love how natural this feels. Approved for Friday posting.',
  CAST('2024-04-03' AS DATETIME2)
),
(
  'appr0000000-0000-0000-0000-000000000003',
  'sub0000000-0000-0000-0000-000000000004',
  'b0000000-0000-0000-0000-000000000001',
  0, -- Approved
  'Love this DIY tutorial! Very engaging and educational. Approved. Please add product links in the bio.',
  CAST('2024-03-09' AS DATETIME2)
);

-- =============================================================================
-- 13. PERFORMANCE METRICS
-- =============================================================================
INSERT INTO PerformanceMetrics (Id, SubmissionId, Reach, Engagements, Saves, Shares, Clicks, Conversions, CapturedAt)
VALUES
-- Metrics for SPF Application Tips (post has been live for 5 days)
(
  'perf0000000-0000-0000-0000-000000000001',
  'sub0000000-0000-0000-0000-000000000001',
  45230,
  3847,
  892,
  456,
  285,
  12,
  CAST('2024-03-20' AS DATETIME2)
),
-- Metrics for Wellness Serum video (just posted 2 days ago)
(
  'perf0000000-0000-0000-0000-000000000002',
  'sub0000000-0000-0000-0000-000000000003',
  89450,
  8923,
  2145,
  1203,
  745,
  34,
  CAST('2024-03-04' AS DATETIME2)
),
-- Metrics for DIY Mask Tutorial (posted 3 days ago)
(
  'perf0000000-0000-0000-0000-000000000003',
  'sub0000000-0000-0000-0000-000000000004',
  56780,
  5234,
  1456,
  782,
  423,
  18,
  CAST('2024-03-11' AS DATETIME2)
);

-- =============================================================================
-- 14. COLLABORATIONS
-- =============================================================================
SET IDENTITY_INSERT Collaborations ON;
INSERT INTO Collaborations (Id, InfluencerId, CampaignId, Title, Date, OutcomeNotes)
VALUES
(
  1,
  'inf0000000-0000-0000-0000-000000000001',
  'camp000-0000-0000-0000-000000000001',
  'GlowSkin Summer Campaign - Shoot Day',
  CAST('2024-03-10' AS DATETIME2),
  'Successful photoshoot. Emma provided excellent product recommendations and engagement during shoot. Content ready for posting.'
),
(
  2,
  'inf0000000-0000-0000-0000-000000000005',
  'camp000-0000-0000-0000-000000000002',
  'BeautyEssence Wellness Event',
  CAST('2024-03-20' AS DATETIME2),
  'Virtual event with 500+ attendees. Olivia shared her wellness routine and drove significant interest in the serum product.'
),
(
  3,
  'inf0000000-0000-0000-0000-000000000002',
  'camp000-0000-0000-0000-000000000003',
  'GlowSkin Spring Campaign - Partner Meeting',
  CAST('2024-02-25' AS DATETIME2),
  'Discussed campaign direction and content themes. Liam excited to create educational UGC content for the spring series.'
);
SET IDENTITY_INSERT Collaborations OFF;

-- =============================================================================
-- All test data successfully inserted!
-- =============================================================================
-- 
-- Test User Credentials:
-- ─────────────────────────────────────────────────────────────
-- 
-- ADMIN USER:
--   Email: admin@beautysquad.com
--   Password: Admin@123
--   Role: Admin
-- 
-- BRAND USERS:
--   Email: brand@glowskin.com
--   Password: Brand@123
--   Role: Brand
--   Email: brand@beautyessence.com
--   Password: Brand@456
--   Role: Brand
-- 
-- INFLUENCER USERS:
--   Email: emma.watson@email.com
--   Password: Influencer@123
--   Role: Influencer
--   Email: liam.jones@email.com
--   Password: Influencer@456
--   Role: Influencer
--   Email: sophia.lee@email.com
--   Password: Influencer@789
--   Role: Influencer
--   Email: marcus.brown@email.com
--   Password: Influencer@012
--   Role: Influencer
--   Email: olivia.patel@email.com
--   Password: Influencer@345
--   Role: Influencer
-- 
-- ─────────────────────────────────────────────────────────────
-- Test Data Summary:
-- ✓ 1 Admin user
-- ✓ 2 Brand users
-- ✓ 5 Influencer users
-- ✓ 10 Tags (content categories)
-- ✓ 5 Influencers (with profiles and tags)
-- ✓ 15 Social profiles (Instagram, TikTok, YouTube, X, Threads)
-- ✓ 3 Campaigns
-- ✓ 6 Campaign Deliverables
-- ✓ 4 Content Submissions
-- ✓ 5 Content Versions (revision history)
-- ✓ 3 Approvals (with brand reviews)
-- ✓ 3 Performance Metrics
-- ✓ 3 Collaborations
-- =============================================================================
