# BeautySquad API - Testing Guide

## Prerequisites

### Tools Needed
- Postman (or curl/Thunder Client)
- Visual Studio or VS Code with C# extension
- SQL Server Management Studio (optional, for DB verification)

### Environment Setup
1. Build the solution: `dotnet build src/`
2. Update Web.config connection string
3. Run database migrations: `Update-Database` (or `dotnet ef database update`)
4. Start the API: `dotnet run` (from src/IAT.WebApi)

## Quick Health Check

### 1. Verify Server is Running

```bash
curl http://localhost:9000/swagger/
```

Should return Swagger UI HTML. If successful ✅, continue.

## API Testing Sequence

Test in this order to build up hierarchical data:

### Phase 1: Campaigns (Foundation)

#### Test 1.1: Create a Campaign
```
POST http://localhost:9000/api/campaigns
Content-Type: application/json

{
  "name": "Summer 2024 Campaign",
  "description": "Our flagship summer beauty campaign",
  "start": "2024-06-01T00:00:00Z",
  "end": "2024-08-31T23:59:59Z",
  "deliverables": []
}
```

**Expected Response:** 201 Created with campaign data including ID

**Save the ID:** `CAMPAIGN_ID = <returned-id>`

#### Test 1.2: Get Campaign
```
GET http://localhost:9000/api/campaigns/{CAMPAIGN_ID}
```

**Expected Response:** 200 OK with campaign details

#### Test 1.3: List Campaigns
```
GET http://localhost:9000/api/campaigns
```

**Expected Response:** 200 OK with array of campaigns

#### Test 1.4: Update Campaign
```
PUT http://localhost:9000/api/campaigns/{CAMPAIGN_ID}
Content-Type: application/json

{
  "name": "Summer 2024 Campaign - UPDATED",
  "description": "Updated description",
  "start": "2024-06-01T00:00:00Z",
  "end": "2024-09-15T23:59:59Z",
  "deliverables": []
}
```

**Expected Response:** 200 OK with updated campaign

### Phase 2: Influencers (Resources)

#### Test 2.1: Create Influencer
```
POST http://localhost:9000/api/influencers
Content-Type: application/json

{
  "fullName": "Jessica Beauty",
  "email": "jessica@example.com",
  "bio": "Beauty enthusiast and skincare expert",
  "phone": "+1-555-0001",
  "geography": "New York, USA",
  "tags": ["skincare", "makeup", "beauty"],
  "advocacyStatus": "Active"
}
```

**Expected Response:** 201 Created

**Save the ID:** `INFLUENCER_ID_1 = <returned-id>`

#### Test 2.2: Create Second Influencer
```
POST http://localhost:9000/api/influencers
Content-Type: application/json

{
  "fullName": "Marco's Vlogs",
  "email": "marco@example.com",
  "bio": "Lifestyle and travel content creator",
  "phone": "+1-555-0002",
  "geography": "Los Angeles, USA",
  "tags": ["travel", "lifestyle", "fashion"],
  "advocacyStatus": "Active"
}
```

**Save the ID:** `INFLUENCER_ID_2 = <returned-id>`

#### Test 2.3: List Influencers
```
GET http://localhost:9000/api/influencers
```

**Expected Response:** 200 OK with 2+ influencers

#### Test 2.4: Get Specific Influencer
```
GET http://localhost:9000/api/influencers/{INFLUENCER_ID_1}
```

**Expected Response:** 200 OK with influencer details

### Phase 3: Content Submissions (Workflow Start)

#### Test 3.1: Create Content Submission (Draft)
```
POST http://localhost:9000/api/content-submissions
Content-Type: application/json

{
  "campaignId": "{CAMPAIGN_ID}",
  "influencerId": "{INFLUENCER_ID_1}",
  "title": "Summer Glow Tutorial",
  "caption": "My favorite summer makeup look featuring the new collection!"
}
```

**Expected Response:** 201 Created with state = "Draft"

**Save the ID:** `SUBMISSION_ID_1 = <returned-id>`

#### Test 3.2: Create Another Submission
```
POST http://localhost:9000/api/content-submissions
Content-Type: application/json

{
  "campaignId": "{CAMPAIGN_ID}",
  "influencerId": "{INFLUENCER_ID_2}",
  "title": "Beach Vibes Content",
  "caption": "Check out our amazing adventure with this summer collection!"
}
```

**Save the ID:** `SUBMISSION_ID_2 = <returned-id>`

#### Test 3.3: Get Campaign Submissions
```
GET http://localhost:9000/api/content-submissions/campaign/{CAMPAIGN_ID}
```

**Expected Response:** 200 OK - Note: May be empty since submissions are Draft by default

#### Test 3.4: Get Influencer Submissions
```
GET http://localhost:9000/api/content-submissions/influencer/{INFLUENCER_ID_1}
```

**Expected Response:** 200 OK with 1+ submission

#### Test 3.5: Update Submission Caption
```
PUT http://localhost:9000/api/content-submissions/{SUBMISSION_ID_1}/caption
Content-Type: application/json

{
  "caption": "Updated: My absolute favorite summer makeup look!"
}
```

**Expected Response:** 200 OK with updated caption

#### Test 3.6: Submit for Approval
```
POST http://localhost:9000/api/content-submissions/{SUBMISSION_ID_1}/submit
Content-Type: application/json

{}
```

**Expected Response:** 200 OK with state = "Submitted"

#### Test 3.7: Submit Second Content
```
POST http://localhost:9000/api/content-submissions/{SUBMISSION_ID_2}/submit
```

**Expected Response:** 200 OK

### Phase 4: Approvals (Review Workflow)

#### Test 4.1: Get Pending Approvals
```
GET http://localhost:9000/api/approvals/pending/{REVIEWER_ID}
```

Note: Use any GUID for reviewer. Real implementation would need auth context.

**Expected Response:** 200 OK (may be empty)

#### Test 4.2: Approve Content
```
POST http://localhost:9000/api/approvals/{SUBMISSION_ID_1}/approve
Content-Type: application/json

{
  "reviewerId": "00000000-0000-0000-0000-000000000001",
  "comments": "Excellent content! Approved for posting."
}
```

**Expected Response:** 201 Created with approval record

**Save ID:** `APPROVAL_ID_1 = <returned-id>`

#### Test 4.3: Reject Second Submission
```
POST http://localhost:9000/api/approvals/{SUBMISSION_ID_2}/reject
Content-Type: application/json

{
  "reviewerId": "00000000-0000-0000-0000-000000000001",
  "comments": "Need to adjust branding elements. Please resubmit."
}
```

**Expected Response:** 201 Created with rejected decision

#### Test 4.4: Get Submission Approvals
```
GET http://localhost:9000/api/approvals/submission/{SUBMISSION_ID_1}
```

**Expected Response:** 200 OK with approval history

### Phase 5: Performance Metrics (Analytics)

#### Test 5.1: Record Metrics (Initial)
```
POST http://localhost:9000/api/performance-metrics
Content-Type: application/json

{
  "submissionId": "{SUBMISSION_ID_1}",
  "reach": 15000,
  "engagements": 750,
  "saves": 120,
  "shares": 45,
  "clicks": 200,
  "conversions": 25
}
```

**Expected Response:** 201 Created

**Save ID:** `METRIC_ID_1 = <returned-id>`

#### Test 5.2: Record Additional Metrics (Day 2)
```
POST http://localhost:9000/api/performance-metrics
Content-Type: application/json

{
  "submissionId": "{SUBMISSION_ID_1}",
  "reach": 22000,
  "engagements": 1100,
  "saves": 180,
  "shares": 65,
  "clicks": 320,
  "conversions": 40
}
```

**Expected Response:** 201 Created

#### Test 5.3: Get Latest Metrics
```
GET http://localhost:9000/api/performance-metrics/submission/{SUBMISSION_ID_1}/latest
```

**Expected Response:** 200 OK with most recent metrics (22000 reach)

#### Test 5.4: Get All Metrics for Submission
```
GET http://localhost:9000/api/performance-metrics/submission/{SUBMISSION_ID_1}
```

**Expected Response:** 200 OK with array of 2 metric records

#### Test 5.5: Get Summary Statistics
```
GET http://localhost:9000/api/performance-metrics/submission/{SUBMISSION_ID_1}/summary
```

**Expected Response:** 200 OK with:
```json
{
  "count": 2,
  "totalReach": 37000,
  "totalEngagements": 1850,
  "totalSaves": 300,
  "totalShares": 110,
  "totalClicks": 520,
  "totalConversions": 65,
  "averageEngagementRate": 0.05,
  "firstCaptured": "2024-02-14T...",
  "lastCaptured": "2024-02-14T..."
}
```

### Phase 6: Error Handling Tests

#### Test 6.1: Invalid Campaign ID
```
GET http://localhost:9000/api/campaigns/00000000-0000-0000-0000-000000000000
```

**Expected Response:** 404 Not Found

#### Test 6.2: Missing Required Field
```
POST http://localhost:9000/api/campaigns
Content-Type: application/json

{
  "description": "No campaign name provided"
}
```

**Expected Response:** 400 Bad Request with validation errors

#### Test 6.3: Invalid Request Body
```
POST http://localhost:9000/api/content-submissions
Content-Type: application/json

{
  "campaignId": "invalid-guid",
  "influencerId": "00000000-0000-0000-0000-000000000000"
}
```

**Expected Response:** 400 Bad Request

## Validation Checklist

After running all tests, verify:

- [ ] All 201 responses created resources with IDs
- [ ] All 200 responses returned expected data
- [ ] All 404 responses were for non-existent resources
- [ ] All 400 responses were for invalid data
- [ ] Submission state progressed: Draft → Submitted → Approved/Rejected
- [ ] Metrics accumulated correctly (2 records)
- [ ] Summary statistics calculated properly
- [ ] No 500 error responses
- [ ] Swagger documentation is accessible

## Database Verification (Optional)

Run these SQL queries to verify data was persisted:

```sql
-- Check campaigns
SELECT COUNT(*) FROM Campaign;

-- Check influencers
SELECT COUNT(*) FROM Influencer;

-- Check submissions
SELECT * FROM ContentSubmission;

-- Check approvals
SELECT * FROM Approval;

-- Check metrics
SELECT COUNT(*) FROM PerformanceMetric;
```

## Postman Collection (JSON)

Save this as `BeautySquad.postman_collection.json`:

```json
{
  "info": {
    "name": "BeautySquad API",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "Campaigns",
      "item": [
        {
          "name": "Create Campaign",
          "request": {
            "method": "POST",
            "url": "http://localhost:9000/api/campaigns"
          }
        }
      ]
    }
  ]
}
```

## Performance Testing

### Load Test with Apache Bench

```bash
ab -n 100 -c 10 http://localhost:9000/api/campaigns
```

Should handle concurrent requests without errors.

## Next Testing Steps

1. ✅ Functional testing (completed above)
2. ⏳ Security testing (JWT, CORS, SQL injection)
3. ⏳ Performance testing (load, stress, endurance)
4. ⏳ Integration testing (database rollback, transactions)
5. ⏳ User acceptance testing (UAT scenarios)

## Troubleshooting

### All Tests Return 500 Internal Server Error

**Cause:** Database connection issue  
**Solution:** 
1. Check Web.config connection string
2. Run migrations: `Update-Database`
3. Verify SQL Server is running

### Controllers not found (404)

**Cause:** Routes not registered  
**Solution:**
1. Rebuild solution: `dotnet build`
2. Restart API server
3. Check Startup.cs route configuration

### Validation errors on valid data

**Cause:** Validator rules too strict  
**Solution:**
1. Check Validators.cs rules
2. Adjust rules or test data
3. Verify DTO property names match request body

## Performance Benchmarks

Expected response times (on local dev machine):

- GET /api/campaigns: < 50ms
- POST /api/campaigns: < 100ms (includes DB write)
- GET /api/content-submissions: < 50ms
- POST /api/performance-metrics: < 100ms

If significantly slower, check:
1. Database performance
2. LazyLoadingEnabled setting
3. N+1 query problems

---

**Happy Testing!** 🧪

Report any issues encountered during testing to improve the implementation.
