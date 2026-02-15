# Test Users & Seed Data Guide

## 📋 Overview

BeautySquad now includes comprehensive test data for all database tables. This guide covers how to populate the database and use the test accounts.

---

## 🚀 Quick Start

### Option 1: Using C# Seeder (Recommended)

The `DatabaseSeeder.cs` class automatically seeds all test data when initialized.

**In `AppDbContext.OnModelCreating()` or Program.cs:**

```csharp
using IAT.Infrastructure.Migrations;

// During application startup
var context = app.Services.GetRequiredService<AppDbContext>();
DatabaseSeeder.SeedData(context);
```

**Or manually in Program.cs:**

```csharp
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();
DatabaseSeeder.SeedData(db);  // Seeds all test data
```

### Option 2: Using SQL Script

For direct database access:

1. Open SQL Server Management Studio
2. Connect to your database
3. Open `database-seed.sql`
4. Execute the entire script
5. Verify data was inserted

---

## 👥 Test User Credentials

### Admin User 🛡️

| Property | Value |
|----------|-------|
| **Email** | admin@beautysquad.com |
| **Password** | Admin@123 |
| **Role** | Admin |
| **Full Name** | Admin User |
| **ID** | a0000000-0000-0000-0000-000000000001 |

**Permissions:**
- Manage all users and roles
- View audit logs
- Configure platform settings
- Restore deleted items
- Access all data

---

### Brand Users 💼

#### Brand 1: GlowSkin

| Property | Value |
|----------|-------|
| **Email** | brand@glowskin.com |
| **Password** | Brand@123 |
| **Role** | Brand |
| **Full Name** | Sarah Mitchell - GlowSkin |
| **ID** | b0000000-0000-0000-0000-000000000001 |

**Associated Campaigns:**
- GlowSkin Summer Radiance Campaign (3 deliverables)
- GlowSkin Spring Skincare Series (3 deliverables)

**Permissions:**
- Create, edit, and delete campaigns
- Assign influencers to deliverables
- Approve/reject content submissions
- View own campaign analytics
- Manage own brand data

---

#### Brand 2: BeautyEssence

| Property | Value |
|----------|-------|
| **Email** | brand@beautyessence.com |
| **Password** | Brand@456 |
| **Role** | Brand |
| **Full Name** | James Chen - BeautyEssence |
| **ID** | b0000000-0000-0000-0000-000000000002 |

**Associated Campaigns:**
- BeautyEssence Wellness Collective (2 deliverables)

**Permissions:**
- Same as Brand 1, but limited to own brand data

---

### Influencer Users ✨

#### 1. Emma Watson

| Property | Value |
|----------|-------|
| **Email** | emma.watson@email.com |
| **Password** | Influencer@123 |
| **Role** | Influencer |
| **Full Name** | Emma Watson |
| **ID** | i0000000-0000-0000-0000-000000000001 |
| **Location** | Los Angeles, USA |
| **Status** | Active |

**Specialization:** Skincare, Eco-Friendly, Natural Products

**Social Profiles:**
| Platform | Handle | Followers |
|----------|--------|-----------|
| Instagram | @emma.watson.beauty | 250K ✓ |
| TikTok | @emmawatsonbeauty | 180K ✓ |
| YouTube | @EmmaWatsonBeauty | 95K ✓ |

**Assigned Deliverables:**
- GlowSkin Summer Radiance: SPF tips carousel (In Progress, Approved)
- BeautyEssence Wellness: Serum review (Planned)

---

#### 2. Liam Jones

| Property | Value |
|----------|-------|
| **Email** | liam.jones@email.com |
| **Password** | Influencer@456 |
| **Role** | Influencer |
| **Full Name** | Liam Jones |
| **ID** | i0000000-0000-0000-0000-000000000002 |
| **Location** | New York, USA |
| **Status** | Active |

**Specialization:** Men's Grooming, Haircare, Affordable Beauty

**Social Profiles:**
| Platform | Handle | Followers |
|----------|--------|-----------|
| TikTok | @liamjonesgrooming | 180K ✓ |
| Instagram | @liam.jones.grooming | 120K |
| X/Twitter | @LiamJones | 85K |

**Assigned Deliverables:**
- GlowSkin Spring Series: DIY mask tutorial (Submitted, Approved)

---

#### 3. Sophia Lee

| Property | Value |
|----------|-------|
| **Email** | sophia.lee@email.com |
| **Password** | Influencer@789 |
| **Role** | Influencer |
| **Full Name** | Sophia Lee |
| **ID** | i0000000-0000-0000-0000-000000000003 |
| **Location** | Toronto, Canada |
| **Status** | Active |

**Specialization:** Luxury Beauty, Makeup, Premium Products

**Social Profiles:**
| Platform | Handle | Followers |
|----------|--------|-----------|
| Instagram | @sophialeeluxury | 500K ✓ |
| YouTube | @SophiaLeeLuxury | 250K ✓ |
| TikTok | @sophialeeluxury | 160K ✓ |

**Assigned Deliverables:**
- GlowSkin Summer Radiance: Luxury routine post (In Progress, Draft)

---

#### 4. Marcus Brown

| Property | Value |
|----------|-------|
| **Email** | marcus.brown@email.com |
| **Password** | Influencer@012 |
| **Role** | Influencer |
| **Full Name** | Marcus Brown |
| **ID** | i0000000-0000-0000-0000-000000000004 |
| **Location** | Atlanta, USA |
| **Status** | Active |

**Specialization:** Inclusive Beauty, Diversity-Focused, Vegan Products

**Social Profiles:**
| Platform | Handle | Followers |
|----------|--------|-----------|
| Instagram | @marcusbrownbeauty | 220K ✓ |
| TikTok | @marcusbrowntok | 195K ✓ |
| YouTube | @MarcusBrownBeauty | 75K |

**Assigned Deliverables:**
- GlowSkin Summer Radiance: Story series (Planned)

---

#### 5. Olivia Patel

| Property | Value |
|----------|-------|
| **Email** | olivia.patel@email.com |
| **Password** | Influencer@345 |
| **Role** | Influencer |
| **Full Name** | Olivia Patel |
| **ID** | i0000000-0000-0000-0000-000000000005 |
| **Location** | Austin, USA |
| **Status** | Prospect |

**Specialization:** Wellness, Natural Beauty, Vegan Products

**Social Profiles:**
| Platform | Handle | Followers |
|----------|--------|-----------|
| TikTok | @olivia.wellness | 300K ✓ |
| Instagram | @olivia.patel.wellness | 150K |
| Threads | @OliviaWellness | 45K |

**Assigned Deliverables:**
- BeautyEssence Wellness: UGC video (In Progress, Approved)

---

## 📊 Test Data Summary

### Database Statistics

| Entity | Count | Details |
|--------|-------|---------|
| **Users** | 8 | 1 Admin, 2 Brands, 5 Influencers |
| **Roles** | 3 | Admin, Brand, Influencer |
| **User-Role Assignments** | 8 | Each user assigned appropriate role |
| **Influencers** | 5 | Complete profiles with bios |
| **Tags** | 10 | Skincare, Makeup, Wellness, etc. |
| **Influencer-Tag Links** | 15 | Linked to specializations |
| **Social Profiles** | 15 | Instagram, TikTok, YouTube, X, Threads |
| **Campaigns** | 3 | Active beauty campaigns |
| **Campaign Deliverables** | 6 | Posts, Stories, UGC, Reviews |
| **Content Submissions** | 4 | Draft to Approved states |
| **Content Versions** | 5 | Version history tracking |
| **Approvals** | 3 | All approved by brands |
| **Performance Metrics** | 3 | Engagement, reach, conversions |
| **Collaborations** | 3 | Shoot & event records |

---

## 🎯 Campaigns Overview

### Campaign 1: GlowSkin Summer Radiance Campaign

**Brand:** Sarah Mitchell (GlowSkin)

**Budget:** $50,000

**Timeline:** 3 months (March - May 2024)

**Objective:** Launch SPF 50+ sunscreen product line

**Deliverables:**
1. **Carousel Post** - Emma Watson - In Progress → Approved
   - Title: "SPF 50+ Application Tips & Tricks"
   - 2 versions submitted
   - Status: Approved, ready to publish
   - Performance: 45K reach, 3.8K engagements
   
2. **Luxury Routine Post** - Sophia Lee - In Progress → Draft
   - Title: "Luxury Summer Skincare Essentials"
   - 1 version (draft, needs revision)
   - Status: Not yet submitted
   
3. **Story Series** - Marcus Brown - Planned
   - Title: "5-part Story Series"
   - Due: 2024-04-01
   - Status: Not yet started

---

### Campaign 2: BeautyEssence Wellness Collective

**Brand:** James Chen (BeautyEssence)

**Budget:** $75,000

**Timeline:** 4 months (March - June 2024)

**Objective:** Launch wellness serum to eco-conscious audience

**Deliverables:**
1. **TikTok UGC Video** - Olivia Patel - In Progress → Approved
   - Title: "Wellness Serum in My Daily Routine"
   - 1 version submitted
   - Status: Approved
   - Performance: 89K reach, 8.9K engagements
   
2. **Product Review** - Emma Watson - Planned
   - Title: "Wellness Serum Review"
   - Due: 2024-04-10
   - Status: Not yet started

---

### Campaign 3: GlowSkin Spring Skincare Series

**Brand:** Sarah Mitchell (GlowSkin)

**Budget:** $35,000

**Timeline:** 2 months (March - April 2024)

**Objective:** Educational series positioning brand as expert

**Deliverables:**
1. **DIY Tutorial** - Liam Jones - Submitted → Approved
   - Title: "DIY Spring Skincare Mask Tutorial"
   - 2 versions (with improvements)
   - Status: Approved, ready to publish
   - Performance: 56K reach, 5.2K engagements

---

## 🔍 Testing Scenarios

### Admin User Testing

Login as `admin@beautysquad.com` / `Admin@123`

**Try:**
- View all users and their roles
- View audit logs (audit trail)
- Configure platform settings
- Manage and restore deleted items
- Access all campaign and content data

---

### Brand User Testing

Login as `brand@glowskin.com` / `Brand@123`

**Try:**
- Create a new campaign
- Edit existing campaigns (GlowSkin ones)
- View campaign deliverables
- Approve/reject content submissions
- View own campaign analytics
- Cannot: see BeautyEssence data, manage users, view audit logs

---

### Influencer User Testing

Login as `emma.watson@email.com` / `Influencer@123`

**Try:**
- Create draft content submission
- Submit content to assigned deliverables
- Upload new versions of submissions
- View own submissions and performance metrics
- View assigned campaigns
- Cannot: create campaigns, approve content, see other influencers' data

---

## 🛠️ Testing Workflows

### Content Creation Workflow

**Influencer Perspective:**
1. Login as influencer (Emma Watson)
2. View "My Assigned Campaigns"
3. See "GlowSkin Summer Radiance" campaign
4. Create draft content for "SPF Application Tips" deliverable
5. Upload content (image, video, caption)
6. Create version v1 with initial caption
7. Edit and versioning (add SPF details)
8. Submit to brand for review

**Brand Perspective:**
1. Login as brand (Sarah Mitchell)
2. Go to campaign "GlowSkin Summer Radiance"
3. See pending submissions
4. Review Emma's carousel post
5. Leave approval comments
6. Approve the submission
7. View performance metrics once published

**Admin Perspective:**
1. Login as admin
2. View approval audit log
3. See all submissions across all brands
4. Track workflow history

---

## 📈 Performance Data Included

### Current Submission Performance

**Highest Performing:**
- Olivia Patel's TikTok (Wellness Serum): 89K reach, 16.5% engagement rate
- Emma Watson's Carousel (SPF Tips): 45K reach, 8.5% engagement rate
- Liam's DIY Tutorial (Spring Mask): 56K reach, 9.2% engagement rate

**Metrics Tracked:**
- Reach: Total impressions
- Engagements: Likes, comments, reactions
- Saves: Bookmarked/saved content
- Shares: Forwarded to others
- Clicks: Link clicks
- Conversions: Sales, sign-ups, etc.

---

## 🔐 Password Hashing

**Note:** All passwords are hashed using BCrypt with cost factor 11.

**For Development Only:**
- Passwords are stored as bcrypt hashes
- Never commit plain text passwords
- All passwords follow the format: `[Role]@[123/456/789/012/345]`

### Credential Format

```
Role-Based Passwords:
├── admin@beautysquad.com → Admin@123
├── brand@glowskin.com → Brand@123
├── brand@beautyessence.com → Brand@456
├── emma.watson@email.com → Influencer@123
├── liam.jones@email.com → Influencer@456
├── sophia.lee@email.com → Influencer@789
├── marcus.brown@email.com → Influencer@012
└── olivia.patel@email.com → Influencer@345
```

---

## ⚠️ Important Notes

### Data Integrity

- ✅ All UUIDs and IDs are consistent across relations
- ✅ Foreign key relationships are properly set
- ✅ Dates are set to realistic values (past and future)
- ✅ Status enums match domain model definitions
- ✅ Soft deletes (IsDeleted) are implemented

### Reset Instructions

**To clear and reseed the database:**

```sql
-- SQL Server
EXEC sp_executesql N'
    DELETE FROM PerformanceMetrics;
    DELETE FROM Approvals;
    DELETE FROM ContentVersions;
    DELETE FROM ContentSubmissions;
    DELETE FROM CampaignDeliverables;
    DELETE FROM Campaigns;
    DELETE FROM Collaborations;
    DELETE FROM SocialProfiles;
    DELETE FROM InfluencerTags;
    DELETE FROM Influencers;
    DELETE FROM UserRoles;
    DELETE FROM Users;
    DELETE FROM Tags;
    DELETE FROM Roles;
'
```

Then re-run the seed script.

---

## 🧪 QA Testing Checklist

- [ ] All users can login with correct credentials
- [ ] Role-based access control working (Admin sees all, Brand sees own, Influencer sees assigned)
- [ ] Campaign data displays correctly
- [ ] Influencer social profiles load with correct follower counts
- [ ] Content submission workflow functions (draft → submit → approve)
- [ ] Version history tracking works
- [ ] Performance metrics display accurately
- [ ] Data isolation verified (Brand 1 cannot see Brand 2 data)
- [ ] Approval comments and decisions stored correctly
- [ ] Soft delete functionality tested
- [ ] Collaboration records display with campaign linkage

---

## 📝 Additional Resources

- Domain Models: [src/IAT.Domain/DomainModels.cs](src/IAT.Domain/DomainModels.cs)
- Seeder Code: [src/IAT.Infrastructure/Migrations/DatabaseSeeder.cs](src/IAT.Infrastructure/Migrations/DatabaseSeeder.cs)
- SQL Script: [database-seed.sql](database-seed.sql)
- RBAC System: [RBAC_GUIDE.md](RBAC_GUIDE.md)
- Architecture: [RBAC_ARCHITECTURE.md](RBAC_ARCHITECTURE.md)

---

**Last Updated:** February 14, 2024

**Next Steps:**
1. Run `DatabaseSeeder.SeedData(context)` on application startup
2. Test login with all user types
3. Execute testing workflows
4. Verify RBAC enforcement
5. Monitor performance metrics
