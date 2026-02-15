DELETE FROM Collaborations;
DELETE FROM PerformanceMetrics;
DELETE FROM Approvals;
DELETE FROM ContentVersions;
DELETE FROM ContentSubmissions;
DELETE FROM CampaignDeliverables;
DELETE FROM Campaigns;
DELETE FROM SocialProfiles;
DELETE FROM InfluencerTags;
DELETE FROM Influencers;
DELETE FROM UserRoles;
DELETE FROM Users;
DELETE FROM Tags;
DELETE FROM Roles;

DBCC CHECKIDENT(Roles,RESEED,0);
DBCC CHECKIDENT(Tags,RESEED,0);
DBCC CHECKIDENT(SocialProfiles,RESEED,0);
DBCC CHECKIDENT(Collaborations,RESEED,0);

SET IDENTITY_INSERT Roles ON;
INSERT INTO Roles (Id, Name) VALUES (1,'Admin'),(2,'Brand'),(3,'Influencer');
SET IDENTITY_INSERT Roles OFF;

INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES ('a0000000-0000-0000-0000-000000000001','admin@beautysquad.com','hash_admin_123','Admin User',1,NULL);
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES ('b0000000-0000-0000-0000-000000000001','brand@glowskin.com','hash_brand_123','Sarah Mitchell',1,NULL);
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES ('b0000000-0000-0000-0000-000000000002','brand@beautyessence.com','hash_brand_456','James Chen',1,NULL);
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES ('i0000000-0000-0000-0000-000000000001','emma.watson@email.com','hash_influencer_123','Emma Watson',1,NULL);
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES ('i0000000-0000-0000-0000-000000000002','liam.jones@email.com','hash_influencer_456','Liam Jones',1,NULL);
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES ('i0000000-0000-0000-0000-000000000003','sophia.lee@email.com','hash_influencer_789','Sophia Lee',1,NULL);
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES ('i0000000-0000-0000-0000-000000000004','marcus.brown@email.com','hash_influencer_012','Marcus Brown',1,NULL);
INSERT INTO Users (Id, Email, PasswordHash, FullName, IsActive, LastLoginAt) VALUES ('i0000000-0000-0000-0000-000000000005','olivia.patel@email.com','hash_influencer_345','Olivia Patel',1,NULL);

INSERT INTO UserRoles (UserId, RoleId) VALUES ('a0000000-0000-0000-0000-000000000001',1);
INSERT INTO UserRoles (UserId, RoleId) VALUES ('b0000000-0000-0000-0000-000000000001',2);
INSERT INTO UserRoles (UserId, RoleId) VALUES ('b0000000-0000-0000-0000-000000000002',2);
INSERT INTO UserRoles (UserId, RoleId) VALUES ('i0000000-0000-0000-0000-000000000001',3);
INSERT INTO UserRoles (UserId, RoleId) VALUES ('i0000000-0000-0000-0000-000000000002',3);
INSERT INTO UserRoles (UserId, RoleId) VALUES ('i0000000-0000-0000-0000-000000000003',3);
INSERT INTO UserRoles (UserId, RoleId) VALUES ('i0000000-0000-0000-0000-000000000004',3);
INSERT INTO UserRoles (UserId, RoleId) VALUES ('i0000000-0000-0000-0000-000000000005',3);

SET IDENTITY_INSERT Tags ON;
INSERT INTO Tags (Id, Name) VALUES (1,'Skincare'),(2,'Makeup'),(3,'Haircare'),(4,'Wellness'),(5,'Beauty'),(6,'Eco-Friendly'),(7,'Luxury'),(8,'Affordable'),(9,'Natural'),(10,'Vegan');
SET IDENTITY_INSERT Tags OFF;

INSERT INTO Influencers (Id, FullName, Bio, Email, Phone, Geography, AdvocacyStatus, Notes, IsDeleted, CreatedAt, ModifiedAt) VALUES ('11111111-1111-1111-1111-111111111111','Emma Watson','Beauty and skincare enthusiast with 250K followers.','emma.watson@email.com','+1-555-0101','Los Angeles, USA',1,'High engagement rate',0,'2024-01-15','2024-01-15');
INSERT INTO Influencers (Id, FullName, Bio, Email, Phone, Geography, AdvocacyStatus, Notes, IsDeleted, CreatedAt, ModifiedAt) VALUES ('22222222-2222-2222-2222-222222222222','Liam Jones','Men grooming expert with 180K TikTok followers.','liam.jones@email.com','+1-555-0102','New York, USA',1,'Strong TikTok presence',0,'2024-01-20','2024-01-20');
INSERT INTO Influencers (Id, FullName, Bio, Email, Phone, Geography, AdvocacyStatus, Notes, IsDeleted, CreatedAt, ModifiedAt) VALUES ('33333333-3333-3333-3333-333333333333','Sophia Lee','Luxury beauty influencer with500K Instagram followers.','sophia.lee@email.com','+1-555-0103','Toronto, Canada',1,'Premium brand specialist',0,'2024-02-01','2024-02-01');
INSERT INTO Influencers (Id, FullName, Bio, Email, Phone, Geography, AdvocacyStatus, Notes, IsDeleted, CreatedAt, ModifiedAt) VALUES ('44444444-4444-4444-4444-444444444444','Marcus Brown','Inclusive beauty advocate celebrating diversity.','marcus.brown@email.com','+1-555-0104','Atlanta, USA',1,'Diversity-focused content',0,'2024-02-05','2024-02-05');
INSERT INTO Influencers (Id, FullName, Bio, Email, Phone, Geography, AdvocacyStatus, Notes, IsDeleted, CreatedAt, ModifiedAt) VALUES ('55555555-5555-5555-5555-555555555555','Olivia Patel','Wellness and natural beauty creator.','olivia.patel@email.com','+1-555-0105','Austin, USA',0,'Growing creator',0,'2024-02-10','2024-02-10');

INSERT INTO InfluencerTags (InfluencerId, TagId) VALUES ('11111111-1111-1111-1111-111111111111',1),('11111111-1111-1111-1111-111111111111',6),('11111111-1111-1111-1111-111111111111',9);
INSERT INTO InfluencerTags (InfluencerId, TagId) VALUES ('22222222-2222-2222-2222-222222222222',3),('22222222-2222-2222-2222-222222222222',2),('22222222-2222-2222-2222-222222222222',8);
INSERT INTO InfluencerTags (InfluencerId, TagId) VALUES ('33333333-3333-3333-3333-333333333333',2),('33333333-3333-3333-3333-333333333333',7),('33333333-3333-3333-3333-333333333333',5);
INSERT INTO InfluencerTags (InfluencerId, TagId) VALUES ('44444444-4444-4444-4444-444444444444',5),('44444444-4444-4444-4444-444444444444',2),('44444444-4444-4444-4444-444444444444',10);
INSERT INTO InfluencerTags (InfluencerId, TagId) VALUES ('55555555-5555-5555-5555-555555555555',4),('55555555-5555-5555-5555-555555555555',9),('55555555-5555-5555-5555-555555555555',10);

SET IDENTITY_INSERT SocialProfiles ON;
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (1,'11111111-1111-1111-1111-111111111111',0,'@emma.watson.beauty','https://instagram.com/emma.watson.beauty',250000,8.5,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (2,'11111111-1111-1111-1111-111111111111',1,'@emmawatsonbeauty','https://tiktok.com/@emmawatsonbeauty',180000,12.3,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (3,'11111111-1111-1111-1111-111111111111',2,'@EmmaWatsonBeauty','https://youtube.com/@EmmaWatsonBeauty',95000,7.8,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (4,'22222222-2222-2222-2222-222222222222',1,'@liamjonesgrooming','https://tiktok.com/@liamjonesgrooming',180000,15.2,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (5,'22222222-2222-2222-2222-222222222222',0,'@liam.jones.grooming','https://instagram.com/liam.jones.grooming',120000,9.1,0);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (6,'22222222-2222-2222-2222-222222222222',3,'@LiamJones','https://x.com/LiamJones',85000,6.5,0);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (7,'33333333-3333-3333-3333-333333333333',0,'@sophialeeluxury','https://instagram.com/sophialeeluxury',500000,11.2,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (8,'33333333-3333-3333-3333-333333333333',2,'@SophiaLeeLuxury','https://youtube.com/@SophiaLeeLuxury',250000,9.5,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (9,'33333333-3333-3333-3333-333333333333',1,'@sophialeeluxury','https://tiktok.com/@sophialeeluxury',160000,13.8,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (10,'44444444-4444-4444-4444-444444444444',0,'@marcusbrownbeauty','https://instagram.com/marcusbrownbeauty',220000,10.6,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (11,'44444444-4444-4444-4444-444444444444',1,'@marcusbrowntok','https://tiktok.com/@marcusbrowntok',195000,14.2,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (12,'44444444-4444-4444-4444-444444444444',2,'@MarcusBrownBeauty','https://youtube.com/@MarcusBrownBeauty',75000,8.3,0);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (13,'55555555-5555-5555-5555-555555555555',1,'@olivia.wellness','https://tiktok.com/@olivia.wellness',300000,16.5,1);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (14,'55555555-5555-5555-5555-555555555555',0,'@olivia.patel.wellness','https://instagram.com/olivia.patel.wellness',150000,11.8,0);
INSERT INTO SocialProfiles (Id, InfluencerId, Platform, Handle, Url, Followers, EngagementRate, IsVerified) VALUES (15,'55555555-5555-5555-5555-555555555555',4,'@OliviaWellness','https://threads.net/@OliviaWellness',45000,9.2,0);
SET IDENTITY_INSERT SocialProfiles OFF;

INSERT INTO Campaigns (Id, Name, Brief, Objectives, StartDate, EndDate, Budget, Status, IsDeleted, CreatedAt) VALUES ('c0000000-0000-0000-0000-000000000001','GlowSkin Summer Radiance','Launch SPF 50+ sunscreen','Increase awareness, drive sales','2024-03-01','2024-05-31',50000.00,1,0,'2024-02-15');
INSERT INTO Campaigns (Id, Name, Brief, Objectives, StartDate, EndDate, Budget, Status, IsDeleted, CreatedAt) VALUES ('c0000000-0000-0000-0000-000000000002','BeautyEssence Wellness','Wellness serum launch','Reach wellness audience','2024-03-15','2024-06-30',75000.00,1,0,'2024-02-20');
INSERT INTO Campaigns (Id, Name, Brief, Objectives, StartDate, EndDate, Budget, Status, IsDeleted, CreatedAt) VALUES ('c0000000-0000-0000-0000-000000000003','GlowSkin Spring Series','Spring skincare education','Position as expert','2024-02-20','2024-04-30',35000.00,1,0,'2024-02-01');

INSERT INTO CampaignDeliverables (Id, CampaignId, Type, Description, DueDate, AssignedInfluencerId, Status) VALUES ('d0000000-0000-0000-0000-000000000001','c0000000-0000-0000-0000-000000000001',1,'Carousel post showcasing SPF tips','2024-03-20','11111111-1111-1111-1111-111111111111',2);
INSERT INTO CampaignDeliverables (Id, CampaignId, Type, Description, DueDate, AssignedInfluencerId, Status) VALUES ('d0000000-0000-0000-0000-000000000002','c0000000-0000-0000-0000-000000000001',1,'Luxury routine post','2024-03-25','33333333-3333-3333-3333-333333333333',2);
INSERT INTO CampaignDeliverables (Id, CampaignId, Type, Description, DueDate, AssignedInfluencerId, Status) VALUES ('d0000000-0000-0000-0000-000000000003','c0000000-0000-0000-0000-000000000001',2,'Story series','2024-04-01','44444444-4444-4444-4444-444444444444',0);
INSERT INTO CampaignDeliverables (Id, CampaignId, Type, Description, DueDate, AssignedInfluencerId, Status) VALUES ('d0000000-0000-0000-0000-000000000004','c0000000-0000-0000-0000-000000000002',3,'TikTok UGC video','2024-04-01','55555555-5555-5555-5555-555555555555',2);
INSERT INTO CampaignDeliverables (Id, CampaignId, Type, Description, DueDate, AssignedInfluencerId, Status) VALUES ('d0000000-0000-0000-0000-000000000005','c0000000-0000-0000-0000-000000000002',4,'Product review','2024-04-10','11111111-1111-1111-1111-111111111111',0);
INSERT INTO CampaignDeliverables (Id, CampaignId, Type, Description, DueDate, AssignedInfluencerId, Status) VALUES ('d0000000-0000-0000-0000-000000000006','c0000000-0000-0000-0000-000000000003',3,'DIY mask tutorial','2024-03-10','22222222-2222-2222-2222-222222222222',3);

INSERT INTO ContentSubmissions (Id, CampaignId, InfluencerId, DeliverableId, Title, Caption, AssetPath, State, CurrentVersionNumber, SubmittedAt) VALUES ('s0000000-0000-0000-0000-000000000001','c0000000-0000-0000-0000-000000000001','11111111-1111-1111-1111-111111111111','d0000000-0000-0000-0000-000000000001','SPF Application Tips','Protecting your skin...', '/content/sub001.jpg',2,2,'2024-03-15');
INSERT INTO ContentSubmissions (Id, CampaignId, InfluencerId, DeliverableId, Title, Caption, AssetPath, State, CurrentVersionNumber, SubmittedAt) VALUES ('s0000000-0000-0000-0000-000000000002','c0000000-0000-0000-0000-000000000001','33333333-3333-3333-3333-333333333333','d0000000-0000-0000-0000-000000000002','Luxury Essentials','My essentials...','/content/sub002.jpg',1,1,NULL);
INSERT INTO ContentSubmissions (Id, CampaignId, InfluencerId, DeliverableId, Title, Caption, AssetPath, State, CurrentVersionNumber, SubmittedAt) VALUES ('s0000000-0000-0000-0000-000000000003','c0000000-0000-0000-0000-000000000002','55555555-5555-5555-5555-555555555555','d0000000-0000-0000-0000-000000000004','Wellness Routine','Integrated serum...','/content/sub003.mp4',2,1,'2024-04-02');
INSERT INTO ContentSubmissions (Id, CampaignId, InfluencerId, DeliverableId, Title, Caption, AssetPath, State, CurrentVersionNumber, SubmittedAt) VALUES ('s0000000-0000-0000-0000-000000000004','c0000000-0000-0000-0000-000000000003','22222222-2222-2222-2222-222222222222','d0000000-0000-0000-0000-000000000006','DIY Mask Tutorial','Ultimate mask...','/content/sub004.mp4',2,2,'2024-03-08');

INSERT INTO ContentVersions (Id, SubmissionId, VersionNumber, Caption, AssetPath, CreatedAt, CreatedByUserId) VALUES ('v0000000-0000-0000-0000-000000000001','s0000000-0000-0000-0000-000000000001',1,'Initial caption','/content/sub001_v1.jpg','2024-03-14','i0000000-0000-0000-0000-000000000001');
INSERT INTO ContentVersions (Id, SubmissionId, VersionNumber, Caption, AssetPath, CreatedAt, CreatedByUserId) VALUES ('v0000000-0000-0000-0000-000000000002','s0000000-0000-0000-0000-000000000001',2,'Updated caption','/content/sub001_v2.jpg','2024-03-15','i0000000-0000-0000-0000-000000000001');
INSERT INTO ContentVersions (Id, SubmissionId, VersionNumber, Caption, AssetPath, CreatedAt, CreatedByUserId) VALUES ('v0000000-0000-0000-0000-000000000003','s0000000-0000-0000-0000-000000000003',1,'Wellness caption','/content/sub003_v1.mp4','2024-04-02','i0000000-0000-0000-0000-000000000005');
INSERT INTO ContentVersions (Id, SubmissionId, VersionNumber, Caption, AssetPath, CreatedAt, CreatedByUserId) VALUES ('v0000000-0000-0000-0000-000000000004','s0000000-0000-0000-0000-000000000004',1,'DIY caption v1','/content/sub004_v1.mp4','2024-03-07','i0000000-0000-0000-0000-000000000002');
INSERT INTO ContentVersions (Id, SubmissionId, VersionNumber, Caption, AssetPath, CreatedAt, CreatedByUserId) VALUES ('v0000000-0000-0000-0000-000000000005','s0000000-0000-0000-0000-000000000004',2,'DIY caption v2','/content/sub004_v2.mp4','2024-03-08','i0000000-0000-0000-0000-000000000002');

INSERT INTO Approvals (Id, SubmissionId, ReviewerUserId, Decision, Comments, DecidedAt) VALUES ('a0000000-0000-0000-0000-000000000001','s0000000-0000-0000-0000-000000000001','b0000000-0000-0000-0000-000000000001',0,'Perfect content!','2024-03-15');
INSERT INTO Approvals (Id, SubmissionId, ReviewerUserId, Decision, Comments, DecidedAt) VALUES ('a0000000-0000-0000-0000-000000000002','s0000000-0000-0000-0000-000000000003','b0000000-0000-0000-0000-000000000002',0,'Excellent TikTok!','2024-04-03');
INSERT INTO Approvals (Id, SubmissionId, ReviewerUserId, Decision, Comments, DecidedAt) VALUES ('a0000000-0000-0000-0000-000000000003','s0000000-0000-0000-0000-000000000004','b0000000-0000-0000-0000-000000000001',0,'Love this tutorial!','2024-03-09');

INSERT INTO PerformanceMetrics (Id, SubmissionId, Reach, Engagements, Saves, Shares, Clicks, Conversions, CapturedAt) VALUES ('p0000000-0000-0000-0000-000000000001','s0000000-0000-0000-0000-000000000001',45230,3847,892,456,285,12,'2024-03-20');
INSERT INTO PerformanceMetrics (Id, SubmissionId, Reach, Engagements, Saves, Shares, Clicks, Conversions, CapturedAt) VALUES ('p0000000-0000-0000-0000-000000000002','s0000000-0000-0000-0000-000000000003',89450,8923,2145,1203,745,34,'2024-04-04');
INSERT INTO PerformanceMetrics (Id, SubmissionId, Reach, Engagements, Saves, Shares, Clicks, Conversions, CapturedAt) VALUES ('p0000000-0000-0000-0000-000000000003','s0000000-0000-0000-0000-000000000004',56780,5234,1456,782,423,18,'2024-03-11');

SET IDENTITY_INSERT Collaborations ON;
INSERT INTO Collaborations (Id, InfluencerId, CampaignId, Title, Date, OutcomeNotes) VALUES (1,'11111111-1111-1111-1111-111111111111','c0000000-0000-0000-0000-000000000001','Shoot Day','2024-03-10','Successful photoshoot');
INSERT INTO Collaborations (Id, InfluencerId, CampaignId, Title, Date, OutcomeNotes) VALUES (2,'55555555-5555-5555-5555-555555555555','c0000000-0000-0000-0000-000000000002','Wellness Event','2024-03-20','Virtual event success');
INSERT INTO Collaborations (Id, InfluencerId, CampaignId, Title, Date, OutcomeNotes) VALUES (3,'22222222-2222-2222-2222-222222222222','c0000000-0000-0000-0000-000000000003','Partner Meeting','2024-02-25','Discussed campaign direction');
SET IDENTITY_INSERT Collaborations OFF;

SELECT 'SUCCESS: All test data inserted!' AS Status;
