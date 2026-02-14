# BeautySquad API - Quick Start Guide

## What's Included

This .NET Framework WebAPI project provides a complete backend for managing influencer campaigns, content submissions, and performance tracking.

## Build & Run

### Prerequisites
- .NET Framework 4.8
- Visual Studio 2019+ or .NET CLI
- SQL Server (LocalDB)

### Build the Solution

Using Visual Studio:
```
Build → Build Solution (F6)
```

Using .NET CLI:
```bash
dotnet build src/
```

### Run the API

Using Visual Studio:
1. Set `IAT.WebApi` as startup project
2. Press F5 or click Start

Using .NET CLI:
```bash
cd src/IAT.WebApi
dotnet run
```

The API will be available at: `http://localhost:9000/`

## Database Setup

### Create Database & Run Migrations

Using Package Manager Console (NuGet):
```powershell
Update-Database
```

Using .NET CLI:
```bash
dotnet ef database update --startup-project src/IAT.WebApi --project src/IAT.Infrastructure
```

### Connection String

Update the connection string in `Web.config`:
```xml
<connectionStrings>
  <add name="IATConnection" connectionString="Server=(localdb)\mssqllocaldb;Database=BeautySquadDb;Integrated Security=true;" />
</connectionStrings>
```

## Project Structure

- **IAT.Domain** - Entity models and business rules
- **IAT.Infrastructure** - Database context, repositories, unit of work
- **IAT.Application** - DTOs, services, validators, mappings
- **IAT.WebApi** - REST Controllers, configuration, middleware

## Key Files Created

### Configuration
- `Startup.cs` - OWIN middleware & DI setup
- `Web.config` - Connection strings & app settings
- `Program.cs` - Console app entry point

### Controllers
- `CampaignsController.cs` - Campaign CRUD operations
- `InfluencersController.cs` - Influencer management
- `ContentSubmissionsController.cs` - Content submission workflow
- `ApprovalsController.cs` - Approval/rejection workflow
- `PerformanceMetricsController.cs` - Metrics tracking

### Services
- `CampaignService.cs` - Campaign business logic
- `InfluencerService.cs` - Influencer management logic
- `ContentSubmissionService.cs` - Submission lifecycle
- `ApprovalService.cs` - Approval workflow
- `PerformanceMetricsService.cs` - Metrics aggregation

### Middleware
- `GlobalExceptionFilter.cs` - Centralized error handling
- `SimpleDependencyResolver.cs` - IoC container
- `SimpleAssemblyResolver.cs` - Controller discovery

## API Documentation

Once the server is running, access Swagger UI:
```
http://localhost:9000/swagger/
```

## Examples

### Create a Campaign
```http
POST /api/campaigns HTTP/1.1
Content-Type: application/json

{
  "name": "Summer Beauty Collection",
  "description": "Q3 campaign with 5 influencers",
  "start": "2024-06-01T00:00:00Z",
  "end": "2024-08-31T23:59:59Z"
}
```

### Create an Influencer
```http
POST /api/influencers HTTP/1.1
Content-Type: application/json

{
  "fullName": "Jessica Beauty",
  "email": "jessica@example.com",
  "bio": "Beauty enthusiast and skinfcare expert",
  "geography": "United States",
  "tags": ["skincare", "makeup", "travel"],
  "advocacyStatus": "Active"
}
```

### Submit Content
```http
POST /api/content-submissions HTTP/1.1
Content-Type: application/json

{
  "campaignId": "550e8400-e29b-41d4-a716-446655440000",
  "influencerId": "550e8400-e29b-41d4-a716-446655440001",
  "title": "Summer Glow Tutorial",
  "caption": "Here's my go-to summer makeup routine using the new collection!"
}
```

### Approve Content
```http
POST /api/approvals/550e8400-e29b-41d4-a716-446655440002/approve HTTP/1.1
Content-Type: application/json

{
  "reviewerId": "550e8400-e29b-41d4-a716-446655440003",
  "comments": "Great content! Ready to schedule."
}
```

### Record Performance Metrics
```http
POST /api/performance-metrics HTTP/1.1
Content-Type: application/json

{
  "submissionId": "550e8400-e29b-41d4-a716-446655440004",
  "reach": 50000,
  "engagements": 2500,
  "saves": 400,
  "shares": 150,
  "clicks": 300,
  "conversions": 45
}
```

## Common Issues

### Database Connection Failed
- Ensure SQL Server LocalDB is running
- Check connection string in Web.config
- Run `sqllocaldb info` to verify LocalDB instance

### 404 Not Found on Endpoints
- Ensure all controllers are in the Controllers folder
- Check route prefixes in controller decorators
- Verify OWIN startup is running the Startup.cs

### Port Already in Use
- Change port in `Program.cs` (default 9000)
- Or kill the process: `netstat -ano | findstr :9000`

## Development Workflow

1. **Add a feature**: Create service interface & implementation
2. **Add DTOs**: Update DTOs.cs with request/response models
3. **Add validation**: Create validator in Validators.cs
4. **Add mapping**: Update MappingProfile.cs
5. **Register service**: Add to Startup.cs DI container
6. **Create controller**: Implement API endpoints
7. **Test**: Use Swagger UI or Postman

## Debugging

### Enable Debug Logging
In Web.config:
```xml
<system.web>
  <compilation debug="true" targetFramework="4.8" />
  <customErrors mode="Off" />
</system.web>
```

### View Logs
Serilog is configured to log to:
- Console output
- Event log (check Application event viewer)

Check `GlobalExceptionFilter.cs` for error handling details.

## Security Notes

⚠️ **Before Production:**
- [ ] Update JWT secret key in Web.config
- [ ] Enable HTTPS in IIS
- [ ] Implement proper authentication
- [ ] Add role-based authorization to controllers
- [ ] Validate all user inputs
- [ ] Implement rate limiting
- [ ] Use environment-specific connection strings
- [ ] Review CORS policy (currently allows *)
- [ ] Implement API key or OAuth2 authentication

## Next Steps

1. ✅ Database setup & migrations
2. ✅ Run the server locally
3. ✅ Test endpoints with Swagger or Postman
4. ⏳ Implement authentication (JWT tokens)
5. ⏳ Add email notifications
6. ⏳ Create admin dashboard
7. ⏳ Deploy to staging environment
8. ⏳ Performance optimization & caching

## Support

For questions about the API structure or implementation, refer to:
- [README.md](../README.md) - Comprehensive documentation
- `Startup.cs` - Configuration and DI setup
- Individual service files - Business logic implementation
- Controllers - API endpoint specifications

Enjoy building! 🚀
