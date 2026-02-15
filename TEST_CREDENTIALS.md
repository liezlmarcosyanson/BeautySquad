# Test Credentials Quick Reference

Copy and paste these to test the application quickly.

---

## 🛡️ ADMIN USER

```
Email:    admin@beautysquad.com
Password: Admin@123
Role:     Admin
Access:   All data, all features, user management
```

---

## 💼 BRAND USERS

### GlowSkin Brand

```
Email:    brand@glowskin.com
Password: Brand@123
Role:     Brand
Company:  GlowSkin Cosmetics
Manager:  Sarah Mitchell
Access:   GlowSkin campaigns only, content approval, analytics
```

### BeautyEssence Brand

```
Email:    brand@beautyessence.com
Password: Brand@456
Role:     Brand
Company:  BeautyEssence
Manager:  James Chen
Access:   BeautyEssence campaigns only, content approval, analytics
```

---

## ✨ INFLUENCER USERS

### Emma Watson (Skincare Specialist)

```
Email:    emma.watson@email.com
Password: Influencer@123
Role:     Influencer
Location: Los Angeles, USA
Followers: 250K Instagram, 180K TikTok
Access:   Assigned campaigns, draft & submit content, personal analytics
```

### Liam Jones (Men's Grooming Expert)

```
Email:    liam.jones@email.com
Password: Influencer@456
Role:     Influencer
Location: New York, USA
Followers: 180K TikTok, 120K Instagram
Access:   Assigned campaigns, draft & submit content, personal analytics
```

### Sophia Lee (Luxury Beauty)

```
Email:    sophia.lee@email.com
Password: Influencer@789
Role:     Influencer
Location: Toronto, Canada
Followers: 500K Instagram, 250K YouTube
Access:   Assigned campaigns, draft & submit content, personal analytics
```

### Marcus Brown (Inclusive Beauty)

```
Email:    marcus.brown@email.com
Password: Influencer@012
Role:     Influencer
Location: Atlanta, USA
Followers: 220K Instagram, 195K TikTok
Access:   Assigned campaigns, draft & submit content, personal analytics
```

### Olivia Patel (Wellness Creator)

```
Email:    olivia.patel@email.com
Password: Influencer@345
Role:     Influencer
Location: Austin, USA
Followers: 300K TikTok, 150K Instagram
Status:   Prospect (not yet Ambassador)
Access:   Assigned campaigns, draft & submit content, personal analytics
```

---

## 🔐 All Credentials in Table Format

| Email | Password | Role | Name |
|-------|----------|------|------|
| admin@beautysquad.com | Admin@123 | Admin | Admin User |
| brand@glowskin.com | Brand@123 | Brand | Sarah Mitchell |
| brand@beautyessence.com | Brand@456 | Brand | James Chen |
| emma.watson@email.com | Influencer@123 | Influencer | Emma Watson |
| liam.jones@email.com | Influencer@456 | Influencer | Liam Jones |
| sophia.lee@email.com | Influencer@789 | Influencer | Sophia Lee |
| marcus.brown@email.com | Influencer@012 | Influencer | Marcus Brown |
| olivia.patel@email.com | Influencer@345 | Influencer | Olivia Patel |

---

## 🎯 Quick Test Scenarios

### Scenario 1: Brand Reviews Content
1. Login: `brand@glowskin.com` / `Brand@123`
2. Navigate to campaign "GlowSkin Summer Radiance"
3. Find submission from Emma Watson
4. Approve or reject with comments
5. Check approval audit trail

### Scenario 2: Influencer Submits Content
1. Login: `emma.watson@email.com` / `Influencer@123`
2. Go to "My Assigned Campaigns"
3. Create draft content
4. Submit for review
5. Check version history

### Scenario 3: Admin Reviews All Activity
1. Login: `admin@beautysquad.com` / `Admin@123`
2. View all users (admin, brands, influencers)
3. Check audit logs
4. View all campaigns across all brands
5. Manage roles and permissions

### Scenario 4: Test Data Isolation
1. Login: `brand@glowskin.com` / `Brand@123`
2. Try to access BeautyEssence campaigns → Should be blocked
3. Logout
4. Login: `brand@beautyessence.com` / `Brand@456`
5. Verify can only see BeautyEssence data

---

## 📱 Popular Test Combinations

**New User Testing:**
- Influencer: `olivia.patel@email.com` / `Influencer@345` (Prospect status)

**High Engagement Testing:**
- Influencer: `liam.jones@email.com` / `Influencer@456` (180K+ TikTok)
- Influencer: `olivia.patel@email.com` / `Influencer@345` (300K TikTok)

**Luxury Segment Testing:**
- Influencer: `sophia.lee@email.com` / `Influencer@789` (500K IG)
- Brand: `brand@glowskin.com` / `Brand@123` (SPF campaign)

**Compliance Testing:**
- Admin: `admin@beautysquad.com` / `Admin@123` (Audit logs)
- Brand: `brand@glowskin.com` / `Brand@123` (Approvals)
- Influencer: `emma.watson@email.com` / `Influencer@123` (Submissions)

---

## 💾 Seeding Instructions

### Method 1: Auto-Seed on Startup (Recommended)

```csharp
// In Program.cs
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();
DatabaseSeeder.SeedData(db);  // ← Adds all test data
```

### Method 2: SQL Script

1. Open SSMS
2. Connect to BeautySquad database
3. Execute `database-seed.sql`
4. All 8 users + test data will be populated

### Method 3: Manual via Entity Framework

```csharp
var context = new AppDbContext();
DatabaseSeeder.SeedData(context);
context.SaveChanges();
```

---

## ✅ Verification Checklist

After seeding, verify:

- [ ] 8 users exist in Users table
- [ ] 3 roles exist in Roles table
- [ ] 5 influencers with complete profiles
- [ ] 3 active campaigns
- [ ] 15 social profiles (Instagram, TikTok, YouTube, X, Threads)
- [ ] 6 campaign deliverables
- [ ] 4 content submissions (various states)
- [ ] 3 approvals (all approved)
- [ ] 3 performance metrics
- [ ] Admin can login → Full access
- [ ] Brand can login → Own data only
- [ ] Influencer can login → Assigned campaigns only

---

## 🚀 Next Steps

1. **Run seeder** on application startup
2. **Test each user type** with credentials above
3. **Verify role-based access** works correctly
4. **Check data isolation** between brands
5. **Monitor performance metrics** display
6. **Review approval workflows**
7. **Test content versioning**
8. **Validate social profile data**

---

**Environment:** Development/Testing

**Database:** SQL Server (Docker)

**Connection String:** `Server=localhost,1433;Database=beautysquad-db;...`

**Last Updated:** February 14, 2024
