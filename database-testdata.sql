-- BeautySquad Test Data Seeding Script
-- Populate BeautySquadDb with comprehensive test data for all roles

-- =============================================================================
-- 1. ROLES (Admin, Brand, Influencer)
-- =============================================================================
SET IDENTITY_INSERT Roles ON;
INSERT INTO Roles (Id, Name) VALUES 
(1, 'Admin'),
(2, 'Brand'),
(3, 'Influencer');
SET IDENTITY_INSERT Roles OFF;

-- =============================================================================
-- 2. TEST USERS (8 total: 1 Admin, 2 Brands, 5 Influencers)
-- =============================================================================
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES 
(CAST('a0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 'admin@beautysquad.com', 'hash_admin_123', 'Admin User', 1, NULL),
(CAST('b0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 'brand@glowskin.com', 'hash_brand_123', 'Sarah Mitchell - GlowSkin', 1, NULL),
(CAST('b0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 'brand@beautyessence.com', 'hash_brand_456', 'James Chen - BeautyEssence', 1, NULL),
(CAST('i0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 'emma.watson@email.com', 'hash_influencer_123', 'Emma Watson', 1, NULL),
(CAST('i0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 'liam.jones@email.com', 'hash_influencer_456', 'Liam Jones', 1, NULL),
(CAST('i0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), 'sophia.lee@email.com', 'hash_influencer_789', 'Sophia Lee', 1, NULL),
(CAST('i0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), 'marcus.brown@email.com', 'hash_influencer_012', 'Marcus Brown', 1, NULL),
(CAST('i0000000-0000-0000-0000-000000000005' AS UNIQUEIDENTIFIER), 'olivia.patel@email.com', 'hash_influencer_345', 'Olivia Patel', 1, NULL);

-- =============================================================================
-- 3. USER ROLES (Role assignments for all users)
-- =============================================================================
INSERT INTO UserRoles (UserId, RoleId) VALUES 
(CAST('a0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 1),
(CAST('b0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 2),
(CAST('b0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 2),
(CAST('i0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 3),
(CAST('i0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 3),
(CAST('i0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), 3),
(CAST('i0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), 3),
(CAST('i0000000-0000-0000-0000-000000000005' AS UNIQUEIDENTIFIER), 3);

-- =============================================================================
-- 4. TAGS (10 beauty-related tags)
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
-- 5. INFLUENCERS (5 profiles with detailed information)
-- =============================================================================
INSERT INTO Influencers (Id, FullName, Bio, Email, Phone, Geography, AdvocacyStatus, Notes, IsDeleted, CreatedAt, ModifiedAt) VALUES
(CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 'Emma Watson', 'Beauty and skincare enthusiast with 250K followers. Specializing in eco-friendly and sustainable beauty products.', 'emma.watson@email.com', '+1-555-0101', 'Los Angeles, USA', 1, 'High engagement rate, verified across all platforms', 0, '2024-01-15', '2024-01-15'),
(CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 'Liam Jones', 'Men''s grooming expert. 180K TikTok followers, trending content creator.', 'liam.jones@email.com', '+1-555-0102', 'New York, USA', 1, 'Strong TikTok presence, great for viral campaigns', 0, '2024-01-20', '2024-01-20'),
(CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 'Sophia Lee', 'Luxury beauty and fashion influencer. 500K Instagram followers. Brand partnerships.', 'sophia.lee@email.com', '+1-555-0103', 'Toronto, Canada', 1, 'Premium brand collaborations, luxury segment specialist', 0, '2024-02-01', '2024-02-01'),
(CAST('44444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 'Marcus Brown', 'Inclusive beauty advocate. Celebrating diversity in cosmetics.', 'marcus.brown@email.com', '+1-555-0104', 'Atlanta, USA', 1, 'Strong community engagement, diversity-focused content', 0, '2024-02-05', '2024-02-05'),
(CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 'Olivia Patel', 'Wellness and natural beauty creator. 300K TikTok followers.', 'olivia.patel@email.com', '+1-555-0105', 'Austin, USA', 0, 'Growing creator, potential for long-term partnerships', 0, '2024-02-10', '2024-02-10');

-- =============================================================================
-- 6. INFLUENCER TAGS (Categorizing influencers by expertise)
-- =============================================================================
INSERT INTO InfluencerTags (InfluencerId, TagId) VALUES
(CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 1),
(CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 6),
(CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 9),
(CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 3),
(CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 2),
(CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 8),
(CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 2),
(CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 7),
(CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 5),
(CAST('44444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 5),
(CAST('44444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 2),
(CAST('44444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 10),
(CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 4),
(CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 9),
(CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 10);

-- =============================================================================
-- 7. SOCIAL PROFILES (15 social accounts across 5 platforms)
-- =============================================================================
SET IDENTITY_INSERT SocialProfiles ON;
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES
(1, CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 0, '@emma.watson.beauty', 'https://instagram.com/emma.watson.beauty', 250000, 8.5, 1),
(2, CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 1, '@emmawatsonbeauty', 'https://tiktok.com/@emmawatsonbeauty', 180000, 12.3, 1),
(3, CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 2, '@EmmaWatsonBeauty', 'https://youtube.com/@EmmaWatsonBeauty', 95000, 7.8, 1),
(4, CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 1, '@liamjonesgrooming', 'https://tiktok.com/@liamjonesgrooming', 180000, 15.2, 1),
(5, CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 0, '@liam.jones.grooming', 'https://instagram.com/liam.jones.grooming', 120000, 9.1, 0),
(6, CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 3, '@LiamJones', 'https://x.com/LiamJones', 85000, 6.5, 0),
(7, CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 0, '@sophialeeluxury', 'https://instagram.com/sophialeeluxury', 500000, 11.2, 1),
(8, CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 2, '@SophiaLeeLuxury', 'https://youtube.com/@SophiaLeeLuxury', 250000, 9.5, 1),
(9, CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 1, '@sophialeeluxury', 'https://tiktok.com/@sophialeeluxury', 160000, 13.8, 1),
(10, CAST('44444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 0, '@marcusbrownbeauty', 'https://instagram.com/marcusbrownbeauty', 220000, 10.6, 1),
(11, CAST('44444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 1, '@marcusbrowntok', 'https://tiktok.com/@marcusbrowntok', 195000, 14.2, 1),
(12, CAST('44444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 2, '@MarcusBrownBeauty', 'https://youtube.com/@MarcusBrownBeauty', 75000, 8.3, 0),
(13, CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 1, '@olivia.wellness', 'https://tiktok.com/@olivia.wellness', 300000, 16.5, 1),
(14, CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 0, '@olivia.patel.wellness', 'https://instagram.com/olivia.patel.wellness', 150000, 11.8, 0),
(15, CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 4, '@OliviaWellness', 'https://threads.net/@OliviaWellness', 45000, 9.2, 0);
SET IDENTITY_INSERT SocialProfiles OFF;

-- =============================================================================
-- 8. CAMPAIGNS (3 active campaigns from 2 brands)
-- =============================================================================
INSERT INTO Campaigns (Id, Name, Brief, Objectives, StartDate, EndDate, Budget, Status, IsDeleted, CreatedAt) VALUES
(CAST('c0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 'GlowSkin Summer Radiance', 'Launch SPF 50+ sunscreen', 'Increase awareness, drive sales', '2024-03-01', '2024-05-31', 50000.00, 1, 0, '2024-02-15'),
(CAST('c0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 'BeautyEssence Wellness', 'Wellness serum launch', 'Reach wellness audience', '2024-03-15', '2024-06-30', 75000.00, 1, 0, '2024-02-20'),
(CAST('c0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), 'GlowSkin Spring Series', 'Spring skincare education', 'Position as expert', '2024-02-20', '2024-04-30', 35000.00, 1, 0, '2024-02-01');

-- =============================================================================
-- 9. CAMPAIGN DELIVERABLES (6 required deliverables assigned to influencers)
-- =============================================================================
INSERT INTO CampaignDeliverables (Id, CampaignId, Type, Description, DueDate, AssignedInfluencerId, Status) VALUES
(CAST('d0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 1, 'Carousel post showcasing SPF tips', '2024-03-20', CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 2),
(CAST('d0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 1, 'Luxury routine post', '2024-03-25', CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 2),
(CAST('d0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 2, 'Story series', '2024-04-01', CAST('44444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 0),
(CAST('d0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 3, 'TikTok UGC video', '2024-04-01', CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 2),
(CAST('d0000000-0000-0000-0000-000000000005' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 4, 'Product review', '2024-04-10', CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 0),
(CAST('d0000000-0000-0000-0000-000000000006' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), 3, 'DIY mask tutorial', '2024-03-10', CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 3);

-- =============================================================================
-- 10. CONTENT SUBMISSIONS (4 submissions in various states)
-- =============================================================================
INSERT INTO ContentSubmissions (Id, CampaignId, InfluencerId, DeliverableId, Title, Caption, AssetPath, State, CurrentVersionNumber, SubmittedAt) VALUES
(CAST('s0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), CAST('d0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 'SPF Application Tips', 'Protecting your skin...', '/content/sub001.jpg', 2, 2, '2024-03-15'),
(CAST('s0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), CAST('33333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), CAST('d0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 'Luxury Essentials', 'My essentials...', '/content/sub002.jpg', 1, 1, NULL),
(CAST('s0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), CAST('d0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), 'Wellness Routine', 'Integrated serum...', '/content/sub003.mp4', 2, 1, '2024-04-02'),
(CAST('s0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), CAST('d0000000-0000-0000-0000-000000000006' AS UNIQUEIDENTIFIER), 'DIY Mask Tutorial', 'Ultimate mask...', '/content/sub004.mp4', 2, 2, '2024-03-08');

-- =============================================================================
-- 11. CONTENT VERSIONS (5 versions tracking content iterations)
-- =============================================================================
INSERT INTO ContentVersions (Id, SubmissionId, VersionNumber, Caption, AssetPath, CreatedAt, CreatedByUserId) VALUES
(CAST('v0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 1, 'Initial caption', '/content/sub001_v1.jpg', '2024-03-14', CAST('i0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER)),
(CAST('v0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 2, 'Updated caption', '/content/sub001_v2.jpg', '2024-03-15', CAST('i0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER)),
(CAST('v0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), 1, 'Wellness caption', '/content/sub003_v1.mp4', '2024-04-02', CAST('i0000000-0000-0000-0000-000000000005' AS UNIQUEIDENTIFIER)),
(CAST('v0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), 1, 'DIY caption v1', '/content/sub004_v1.mp4', '2024-03-07', CAST('i0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER)),
(CAST('v0000000-0000-0000-0000-000000000005' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), 2, 'DIY caption v2', '/content/sub004_v2.mp4', '2024-03-08', CAST('i0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER));

-- =============================================================================
-- 12. APPROVALS (3 brand approvals of influencer content)
-- =============================================================================
INSERT INTO Approvals (Id, SubmissionId, ReviewerUserId, Decision, Comments, DecidedAt) VALUES
(CAST('a0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), CAST('b0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 0, 'Perfect content!', '2024-03-15'),
(CAST('a0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), CAST('b0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 0, 'Excellent TikTok!', '2024-04-03'),
(CAST('a0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), CAST('b0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 0, 'Love this tutorial!', '2024-03-09');

-- =============================================================================
-- 13. PERFORMANCE METRICS (3 performance records for published content)
-- =============================================================================
INSERT INTO PerformanceMetrics (Id, SubmissionId, Reach, Engagements, Saves, Shares, Clicks, Conversions, CapturedAt) VALUES
(CAST('p0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 45230, 3847, 892, 456, 285, 12, '2024-03-20'),
(CAST('p0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), 89450, 8923, 2145, 1203, 745, 34, '2024-04-04'),
(CAST('p0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), CAST('s0000000-0000-0000-0000-000000000004' AS UNIQUEIDENTIFIER), 56780, 5234, 1456, 782, 423, 18, '2024-03-11');

-- =============================================================================
-- 14. COLLABORATIONS (3 collaboration records)
-- =============================================================================
SET IDENTITY_INSERT Collaborations ON;
INSERT INTO Collaborations (Id, InfluencerId, CampaignId, Title, Date, OutcomeNotes) VALUES
(1, CAST('11111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000001' AS UNIQUEIDENTIFIER), 'Shoot Day', '2024-03-10', 'Successful photoshoot'),
(2, CAST('55555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000002' AS UNIQUEIDENTIFIER), 'Wellness Event', '2024-03-20', 'Virtual event success'),
(3, CAST('22222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), CAST('c0000000-0000-0000-0000-000000000003' AS UNIQUEIDENTIFIER), 'Partner Meeting', '2024-02-25', 'Discussed campaign direction');
SET IDENTITY_INSERT Collaborations OFF;

PRINT '';
PRINT '✓ ALL TEST DATA SUCCESSFULLY INSERTED!';
PRINT '';
PRINT 'Test Credentials:';
PRINT '────────────────────────────────────────────────────────────';
PRINT 'Admin: admin@beautysquad.com / Admin@123';
PRINT '';
PRINT 'Brand Accounts:';
PRINT '  1. brand@glowskin.com / Brand@123';
PRINT '  2. brand@beautyessence.com / Brand@456';
PRINT '';
PRINT 'Influencer Accounts:';
PRINT '  1. emma.watson@email.com / Influencer@123';
PRINT '  2. liam.jones@email.com / Influencer@456';
PRINT '  3. sophia.lee@email.com / Influencer@789';
PRINT '  4. marcus.brown@email.com / Influencer@012';
PRINT '  5. olivia.patel@email.com / Influencer@345';
PRINT '────────────────────────────────────────────────────────────';
PRINT '';
PRINT 'Data Summary:';
PRINT '  - 3 Roles (Admin, Brand, Influencer)';
PRINT '  - 8 Users (1 Admin, 2 Brands, 5 Influencers)';
PRINT '  - 10 Tags';
PRINT '  - 5 Influencer Profiles';
PRINT '  - 15 Social Profiles';
PRINT '  - 3 Active Campaigns';
PRINT '  - 6 Campaign Deliverables';
PRINT '  - 4 Content Submissions';
PRINT '  - 5 Content Versions';
PRINT '  - 3 Approvals';
PRINT '  - 3 Performance Metrics';
PRINT '  - 3 Collaborations';
PRINT '';
PRINT 'Database seeding is complete and ready for testing!';
