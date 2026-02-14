# BeautySquad - Influencer Campaign Management API

A comprehensive .NET Framework WebAPI for managing influencer campaigns, content submissions, and performance tracking.

## Project Structure

```
src/
├── IAT.Domain/                  # Domain models and entities
│   └── DomainModels.cs
├── IAT.Infrastructure/          # Data access and repos
│   ├── AppDbContext.cs
│   ├── UnitOfWork.cs
│   └── Repositories/
└── IAT.Application/             # Business logic and services
│   ├── DTOs.cs
│   ├── MappingProfile.cs
│   ├── Validators.cs
│   └── Services/
│       ├── CampaignService.cs
│       ├── InfluencerService.cs
│       ├── ContentSubmissionService.cs
│       ├── ApprovalService.cs
│       └── PerformanceMetricsService.cs
└── IAT.WebApi/                  # REST API endpoints
    ├── Startup.cs               # OWIN configuration
    ├── Program.cs
    ├── Web.config
    └── Controllers/
        ├── CampaignsController.cs
        ├── InfluencersController.cs
        ├── ContentSubmissionsController.cs
        ├── ApprovalsController.cs
        └── PerformanceMetricsController.cs
```

## Features

### Campaigns
- Create, read, update, and soft-delete campaigns
- Manage campaign deliverables
- Track campaign status (Draft, Active, Completed, Archived)

### Influencers
- Manage influencer profiles with tags and social profiles
- Track advocacy status (Prospect, Active, Ambassador)
- Support for multiple social platforms (Instagram, TikTok, YouTube, X)

### Content Submissions
- Influencers can submit content for campaigns
- Version control for submissions
- Support for multiple content types (Post, Story, UGC, Review, Event)

### Approvals
- Review and approve/reject submissions
- Leave comments on submissions
- Track approval decisions

### Performance Metrics
- Record engagement and reach metrics
- Track conversions and social actions
- Get summary statistics by submission

## API Endpoints

### Campaigns
```
GET    /api/campaigns              - List campaigns
GET    /api/campaigns/{id}         - Get campaign
POST   /api/campaigns              - Create campaign
PUT    /api/campaigns/{id}         - Update campaign
DELETE /api/campaigns/{id}         - Delete campaign
```

### Influencers
```
GET    /api/influencers            - List influencers
GET    /api/influencers/{id}       - Get influencer
POST   /api/influencers            - Create influencer
PUT    /api/influencers/{id}       - Update influencer
DELETE /api/influencers/{id}       - Delete influencer
```

### Content Submissions
```
GET    /api/content-submissions/campaign/{campaignId}     - Get campaign submissions
GET    /api/content-submissions/influencer/{influencerId} - Get influencer submissions
GET    /api/content-submissions/{id}                      - Get submission
POST   /api/content-submissions                           - Create submission
PUT    /api/content-submissions/{id}/caption              - Update caption
POST   /api/content-submissions/{id}/submit               - Submit for approval
DELETE /api/content-submissions/{id}                      - Delete submission
```

### Approvals
```
GET    /api/approvals/{id}                           - Get approval
GET    /api/approvals/submission/{submissionId}      - Get submission approvals
GET    /api/approvals/pending/{reviewerId}           - Get pending approvals
POST   /api/approvals/{submissionId}/approve         - Approve submission
POST   /api/approvals/{submissionId}/reject          - Reject submission
```

### Performance Metrics
```
GET    /api/performance-metrics/{id}                      - Get metric
GET    /api/performance-metrics/submission/{submissionId} - Get all metrics
GET    /api/performance-metrics/submission/{submissionId}/latest        - Get latest metric
GET    /api/performance-metrics/submission/{submissionId}/summary       - Get summary stats
POST   /api/performance-metrics                      - Record metrics
```

## Setup & Configuration

### Prerequisites
- .NET Framework 4.8
- SQL Server (LocalDB or full instance)
- Visual Studio 2019+ or VS Code with C# extension

### Database Setup

1. Update connection string in `Web.config`:
```xml
<connectionStrings>
  <add name="IATConnection" connectionString="Server=YOUR_SERVER;Database=BeautySquadDb;Integrated Security=true;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

2. Create database and run migrations:
```bash
dotnet ef database update -s src/IAT.WebApi
```

3. Or use Package Manager Console:
```
Update-Database
```

### Running the Server

```bash
cd src/IAT.WebApi
dotnet run
```

The API will start at `http://localhost:9000/`

Access Swagger UI at: `http://localhost:9000/swagger/`

### JWT Configuration

Update JWT settings in `Web.config` and `Startup.cs`:
```xml
<add key="JwtSecret" value="your-secret-key-min-32-chars-long-for-hs256" />
```

Generate a token with:
```csharp
var claims = new[] { 
    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
    new Claim(ClaimTypes.Role, "admin")
};
```

## Data Models

### Core Entities

- **Campaign**: Marketing campaigns with deliverables
- **Influencer**: Content creators and brand advocates
- **ContentSubmission**: Content submissions from influencers
- **Approval**: Review decisions on submissions
- **PerformanceMetric**: Engagement and reach metrics
- **User**: System users and reviewers
- **Role**: User roles for authorization
- **Tag**: Content categorization
- **SocialProfile**: Influencer social media accounts
- **Collaboration**: Historical collaboration records

## Status Enums

- **CampaignStatus**: Draft, Active, Completed, Archived
- **DeliverableStatus**: Planned, InProgress, Submitted, Approved, Rejected
- **SubmissionState**: Draft, Submitted, Approved, Rejected
- **AdvocacyStatus**: Prospect, Active, Ambassador
- **ApprovalDecision**: Approved, Rejected
- **SocialPlatform**: Instagram, TikTok, YouTube, X, Other

## Development

### Adding a New Service

1. Create `INewService.cs` interface in `Services/`
2. Implement `NewService.cs` class
3. Add DTOs to `DTOs.cs`
4. Create mappings in `MappingProfile.cs`
5. Register in `Startup.cs` DI container
6. Create `NewController.cs` with CRUD endpoints

### Validation

Add validators in `Validators.cs` using FluentValidation:
```csharp
public class NewEntityValidator : AbstractValidator<NewEntityRequest>
{
    public NewEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
    }
}
```

## Error Handling

The API returns appropriate HTTP status codes:
- `200 OK` - Successful GET/PUT
- `201 Created` - Successful POST
- `204 No Content` - Successful DELETE
- `400 Bad Request` - Validation errors
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server errors

## Deployment

### IIS Deployment
1. Publish the WebApi project
2. Create an IIS Application Pool for .NET Framework 4.8
3. Create a website pointing to the published files
4. Configure database connection in Web.config

### Docker Support
Add a Dockerfile for containerization (optional).

## Future Enhancements

- [ ] Email notifications for approvals
- [ ] Influencer analytics dashboard
- [ ] Campaign ROI tracking
- [ ] Social media API integrations
- [ ] Advanced reporting and analytics
- [ ] Payment/Compensation management
- [ ] Multi-language support
- [ ] Real-time notifications via SignalR

## License

Proprietary - BeautySquad

## Support

For issues or feature requests, contact the development team.
