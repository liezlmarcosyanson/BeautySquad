using System;
using System.Collections.Generic;

namespace IAT.Application
{
    public class LoginRequest { public string Email { get; set; } public string Password { get; set; } }
    public class AuthResponse { public string Token { get; set; } public DateTime ExpiresAt { get; set; } }

    public class InfluencerCreateRequest { public string FullName { get; set; } public string Email { get; set; } public string Bio { get; set; } public string Phone { get; set; } public string Geography { get; set; } public string[] Tags { get; set; } public string AdvocacyStatus { get; set; } }
    public class InfluencerDto { public Guid Id { get; set; } public string FullName { get; set; } public string Email { get; set; } public string Bio { get; set; } public string[] Tags { get; set; } }

        public class CampaignCreateRequest { public string Name { get; set; } public string Description { get; set; } public DateTime Start { get; set; } public DateTime End { get; set; } public List<CampaignDeliverableDto> Deliverables { get; set; } }
        
        public class CampaignDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public List<CampaignDeliverableDto> Deliverables { get; set; }
        }
        
        public class CampaignDeliverableDto
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public bool Required { get; set; }
        }

    public class ContentSubmissionCreateRequest { public Guid CampaignId { get; set; } public Guid InfluencerId { get; set; } public string Title { get; set; } public string Caption { get; set; } }
    
    public class ContentSubmissionDto
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public Guid InfluencerId { get; set; }
        public Guid? DeliverableId { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string AssetPath { get; set; }
        public string State { get; set; }
        public int CurrentVersionNumber { get; set; }
        public DateTime? SubmittedAt { get; set; }
    }

    public class ApprovalDto
    {
        public Guid Id { get; set; }
        public Guid SubmissionId { get; set; }
        public Guid ReviewerUserId { get; set; }
        public string Decision { get; set; }
        public string Comments { get; set; }
        public DateTime DecidedAt { get; set; }
    }

    public class PerformanceMetricsCreateRequest
    {
        public int Reach { get; set; }
        public int Engagements { get; set; }
        public int Saves { get; set; }
        public int Shares { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public DateTime? CapturedAt { get; set; }
    }

    public class PerformanceMetricsDto
    {
        public Guid Id { get; set; }
        public Guid SubmissionId { get; set; }
        public int Reach { get; set; }
        public int Engagements { get; set; }
        public int Saves { get; set; }
        public int Shares { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public DateTime CapturedAt { get; set; }
    }
}
