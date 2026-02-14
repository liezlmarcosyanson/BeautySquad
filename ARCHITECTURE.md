# BeautySquad API - Architecture & Design Document

## System Overview

### What is BeautySquad?

BeautySquad is a RESTful API backend for managing beauty brand influencer campaigns. It handles:

1. **Campaign Management** - Create and manage marketing campaigns
2. **Influencer Management** - Maintain influencer profiles and advocacy status
3. **Content Submission Workflow** - Handle content from proposal → approval → posting
4. **Approval System** - Review and approve/reject content submissions
5. **Performance Analytics** - Track engagement and conversion metrics

### Architecture Pattern: Layered Architecture

```
┌─────────────────────────────────────────────────────────┐
│                  Presentation Layer                      │
│            (REST Controllers & HTTP Endpoints)           │
├─────────────────────────────────────────────────────────┤
│                  Business Logic Layer                    │
│             (Services & Business Rules)                  │
├─────────────────────────────────────────────────────────┤
│              Data Access Layer                           │
│        (Unit of Work, Repositories, DbContext)          │
├─────────────────────────────────────────────────────────┤
│                  Domain Layer                            │
│               (Entities & Value Objects)                │
├─────────────────────────────────────────────────────────┤
│                   Data Layer                             │
│                  (SQL Server)                            │
└─────────────────────────────────────────────────────────┘
```

## Project Structure

### 1. Domain Layer (IAT.Domain)
**Responsibility:** Define business entities and rules

**Key Files:**
- `DomainModels.cs` - All entity models

**Entity Categories:**

**Users & Security:**
- `User` - System users
- `Role` - User roles (Admin, Reviewer, etc.)
- `UserRole` - User-to-role mapping

**Campaigns:**
- `Campaign` - Marketing campaigns
- `CampaignDeliverable` - Campaign deliverables/requirements

**Influencers:**
- `Influencer` - Influencer profiles
- `Tag` - Content tags/categories
- `InfluencerTag` - Influencer-to-tag mapping
- `SocialProfile` - Social media account details

**Content Management:**
- `ContentSubmission` - Influencer content submissions
- `ContentVersion` - Version history for submissions
- `Approval` - Approval decisions
- `PerformanceMetric` - Engagement metrics

**Legacy:**
- `Collaboration` - Historical collaboration records

**Enums:**
```csharp
CampaignStatus: Draft, Active, Completed, Archived
DeliverableStatus: Planned, InProgress, Submitted, Approved, Rejected
SubmissionState: Draft, Submitted, Approved, Rejected
AdvocacyStatus: Prospect, Active, Ambassador
ApprovalDecision: Approved, Rejected
SocialPlatform: Instagram, TikTok, YouTube, X, Other
DeliverableType: Post, Story, UGC, Review, Event
```

### 2. Infrastructure Layer (IAT.Infrastructure)
**Responsibility:** Database access and data persistence

**Key Files:**
- `AppDbContext.cs` - Entity Framework DbContext
- `UnitOfWork.cs` - Unit of Work pattern implementation
- `Repositories/` - Repository implementations

**Database Configuration:**
- Connection string: `IATConnection` (Web.config)
- Database: `BeautySquadDb`
- ORM: Entity Framework 6
- Conventions: No pluralizing table names

**Unit of Work Pattern:**
```csharp
interface IUnitOfWork
{
    IRepository<Campaign> Campaigns { get; }
    IRepository<Influencer> Influencers { get; }
    // ... all repositories
    int SaveChanges();
    int Commit();
}
```

### 3. Application Layer (IAT.Application)
**Responsibility:** Business logic and use cases

**Key Files:**
- `DTOs.cs` - Data transfer objects
- `MappingProfile.cs` - AutoMapper configuration
- `Validators.cs` - FluentValidation rules
- `Services/` - Business logic services

**Data Transfer Objects (DTOs):**

Request DTOs (Input):
- `CampaignCreateRequest`
- `InfluencerCreateRequest`
- `ContentSubmissionCreateRequest`
- `PerformanceMetricsCreateRequest`

Response DTOs (Output):
- `CampaignDto`
- `InfluencerDto`
- `ContentSubmissionDto`
- `ApprovalDto`
- `PerformanceMetricsDto`
- `AuthResponse`

**Service Implementations:**

**CampaignService**
```
- Query(q): Search campaigns
- Get(id): Get specific campaign
- Create(request): Create new campaign
- Update(id, request): Update campaign
- SoftDelete(id): Mark as deleted
```

**InfluencerService**
```
- Create(req): Create influencer with tags
- Get(id): Get influencer
- Query(): Get all active
- Update(id, req): Update influencer
- SoftDelete(id): Soft delete
```

**ContentSubmissionService**
```
- GetByCampaign(campaignId): Get campaign submissions
- GetByInfluencer(influencerId): Get influencer submissions
- Get(id): Get specific submission
- Create(request): Create draft submission
- UpdateCaption(id, caption): Edit draft
- Submit(id): Move to submitted state
- Delete(id): Hard delete
```

**ApprovalService**
```
- Get(id): Get approval record
- GetBySubmission(id): Get approval history
- GetPendingForReviewer(id): Get pending for reviewer
- Approve(submissionId, reviewerId, comments): Approve
- Reject(submissionId, reviewerId, comments): Reject
```

**PerformanceMetricsService**
```
- Get(id): Get metric record
- GetBySubmission(id): Get all metrics
- Record(submissionId, request): Record new metrics
- GetLatest(id): Get most recent
- GetSummaryStats(id): Get aggregates
```

### 4. Presentation Layer (IAT.WebApi)
**Responsibility:** REST API endpoints and HTTP handling

**Key Files:**
- `Startup.cs` - OWIN configuration and DI
- `Program.cs` - Application entry point
- `Web.config` - Configuration and connection strings
- `GlobalExceptionFilter.cs` - Centralized error handling
- `Controllers/` - REST endpoints

**Controllers:**

```
CampaignsController (5 endpoints)
├── GET /api/campaigns
├── GET /api/campaigns/{id}
├── POST /api/campaigns
├── PUT /api/campaigns/{id}
└── DELETE /api/campaigns/{id}

InfluencersController (5 endpoints)
├── GET /api/influencers
├── GET /api/influencers/{id}
├── POST /api/influencers
├── PUT /api/influencers/{id}
└── DELETE /api/influencers/{id}

ContentSubmissionsController (7 endpoints)
├── GET /api/content-submissions/campaign/{campaignId}
├── GET /api/content-submissions/influencer/{influencerId}
├── GET /api/content-submissions/{id}
├── POST /api/content-submissions
├── PUT /api/content-submissions/{id}/caption
├── POST /api/content-submissions/{id}/submit
└── DELETE /api/content-submissions/{id}

ApprovalsController (5 endpoints)
├── GET /api/approvals/{id}
├── GET /api/approvals/submission/{submissionId}
├── GET /api/approvals/pending/{reviewerId}
├── POST /api/approvals/{submissionId}/approve
└── POST /api/approvals/{submissionId}/reject

PerformanceMetricsController (5 endpoints)
├── GET /api/performance-metrics/{id}
├── GET /api/performance-metrics/submission/{submissionId}
├── GET /api/performance-metrics/submission/{submissionId}/latest
├── GET /api/performance-metrics/submission/{submissionId}/summary
└── POST /api/performance-metrics
```

## Data Flows

### Campaign Creation Flow
```
1. Client POST /api/campaigns
2. CampaignsController validates request
3. AutoMapper maps DTO → Entity
4. CampaignService.Create() executes
5. UnitOfWork.Campaigns.Add(campaign)
6. UnitOfWork.Commit() → SaveChanges()
7. Controller returns 201 Created with CampaignDto
```

### Content Submission Workflow
```
1. Influencer POST /api/content-submissions (Draft)
   ├─ ContentSubmissionService.Create()
   └─ State = Draft

2. Influencer reviews and submits
   ├─ Influencer POST /api/content-submissions/{id}/submit
   ├─ ContentSubmissionService.Submit()
   ├─ State = Submitted
   └─ Creates ContentVersion record

3. Reviewer reviews submission
   ├─ Reviewer POST /api/approvals/{submissionId}/approve
   ├─ ApprovalService.Approve()
   ├─ State = Approved
   └─ Creates Approval record

OR Reviewer rejects
   ├─ Reviewer POST /api/approvals/{submissionId}/reject
   ├─ ApprovalService.Reject()
   ├─ State = Rejected
   └─ Creates Approval record

4. Track performance
   ├─ POST /api/performance-metrics
   ├─ PerformanceMetricsService.Record()
   └─ Creates PerformanceMetric record
```

## Design Patterns Used

### 1. Repository Pattern
```csharp
// Generic repository for all entities
public interface IRepository<T> where T : class
{
    IQueryable<T> Query();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
```

### 2. Unit of Work Pattern
```csharp
// Coordinates multiple repositories and transactions
public interface IUnitOfWork : IDisposable
{
    IRepository<Campaign> Campaigns { get; }
    IRepository<Influencer> Influencers { get; }
    // ... all repositories
    int Commit();
}
```

### 3. Service Layer Pattern
```csharp
// Business logic encapsulation
public class CampaignService : ICampaignService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    // ... business methods
}
```

### 4. DTO (Data Transfer Object) Pattern
```csharp
// Transfer objects for API communication
public class CampaignDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Start { get; set; }
    // ... mapped from entity
}
```

### 5. Dependency Injection Pattern
```csharp
// Constructor-based DI
public CampaignsController(ICampaignService campaignService)
{
    _campaignService = campaignService;
}
```

## Security Architecture

### Authentication (JWT)
- Token-based authentication configured
- OWIN middleware for JWT validation
- Bearer token in Authorization header
- Configurable expiration (default 60 minutes)

### Authorization
- Role-based access control (RBAC) ready
- User and Role entities exist
- Attributes can be added to controllers

### Input Validation
- FluentValidation for Business logic validation
- Model validation attributes for DTOs
- Controller ModelState checking

### CORS
- Currently allows all origins (should restrict in production)
- Supports all HTTP methods
- Includes Authorization header

## Scalability Considerations

### Database
- Uses SQL Server (scalable RDBMS)
- Connection pooling via connection string
- Indexes on frequently queried fields (Email, Tags)
- Soft delete support for data retention

### API
- Stateless design (horizontal scalable)
- No session storage
- No in-memory caching (but can be added)

### Performance Optimizations Available
1. Add query pagination
2. Implement caching (Redis/MemoryCache)
3. Add database query optimization
4. Implement background jobs (Hangfire)
5. Add compression middleware

## Error Handling Strategy

### Exception Hierarchy
```
Exception
├── ArgumentException → 400 Bad Request
├── InvalidOperationException → 400 Bad Request
├── UnauthorizedAccessException → 401 Unauthorized
└── General Exception → 500 Internal Server Error
```

### Error Response Format
```json
{
  "message": "User-friendly error message",
  "exceptionType": "ArgumentException",
  "stackTrace": null,  // null in production
  "timestamp": "2024-02-14T12:00:00Z",
  "requestUri": "POST /api/campaigns"
}
```

## Testing Strategy

### Unit Tests (Recommended)
- Service business logic
- Validator rules
- Mapper configurations

### Integration Tests (Recommended)
- Repository operations
- Database interactions
- End-to-end workflows

### Manual Testing
- Swagger UI exploration
- Postman collection execution
- Load testing

## Deployment Architecture

### Development
```
Developer Machine
├── Visual Studio
├── SQL Server LocalDB
├── Localhost:9000
└── Swagger UI
```

### Staging
```
Staging Server
├── IIS Application
├── SQL Server
├── HTTPS + SSL
└── Monitoring
```

### Production
```
Production Environment
├── Azure App Service (or similar)
├── Azure SQL Database
├── Application Insights (monitoring)
├── Key Vault (secrets management)
└── CDN (static content)
```

## Configuration Management

### Development (Web.config)
```xml
<connectionStrings>
  <add name="IATConnection" value="(local, dev DB)" />
</connectionStrings>
<appSettings>
  <add key="JwtSecret" value="dev-secret" />
</appSettings>
```

### Staging/Production
- Use environment-specific Web.config
- Store secrets in Key Vault
- Connection strings from secure storage
- Feature flags for gradual rollout

## Monitoring & Logging

### Serilog Integration
```csharp
Log.Error(exception, "Error message", params);
Log.Information("Info message");
Log.Warning("Warning message");
```

### Health Checks
- Database connectivity
- External service health
- API response times

### Metrics to Track
- Request volume and latency
- Error rates and types
- Database query performance
- Approval workflow metrics

## Future Enhancements

### Phase 2
- [ ] Email notifications
- [ ] File upload/asset management
- [ ] Search and filtering
- [ ] Pagination on list endpoints
- [ ] Rate limiting

### Phase 3
- [ ] Analytics dashboard API
- [ ] Influencer discovery algorithm
- [ ] Multi-brand support
- [ ] Advanced reporting
- [ ] Social media integrations

### Phase 4
- [ ] Mobile app API enhancements
- [ ] Real-time notifications (SignalR)
- [ ] Machine learning recommendations
- [ ] Payment integration
- [ ] Multi-language support

## Documentation Map

- **README.md** - Project overview
- **QUICKSTART.md** - Getting started guide
- **TESTING.md** - How to test the API
- **BUILD_SUMMARY.md** - What was built
- **ARCHITECTURE.md** - This document

## Code Quality Standards

### Naming Conventions
- PascalCase for classes, methods, properties
- camelCase for local variables and parameters
- UPPER_SNAKE_CASE for constants

### Code Organization
- One class per file (unless very small)
- Logical grouping of related files
- Clear separation of concerns

### Comments & Documentation
- XML documentation for public members
- Inline comments for complex logic
- Method summaries explaining purpose and parameters

### SOLID Principles
- **S**ingle Responsibility - One reason to change
- **O**pen/Closed - Open for extension, closed for modification
- **L**iskov Substitution - Subtypes substitutable
- **I**nterface Segregation - Many specific interfaces
- **D**ependency Inversion - Depend on abstractions

## Conclusion

The BeautySquad API follows industry best practices for .NET development with a clean layered architecture, proper separation of concerns, and modern design patterns. The codebase is structured for maintainability, testability, and scalability.

---

**Document Version:** 1.0  
**Last Updated:** February 14, 2026  
**Status:** Complete
