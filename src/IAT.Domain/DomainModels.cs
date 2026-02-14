using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAT.Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(256)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }

    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }

    public class UserRole
    {
        [Key, Column(Order = 0)]
        public Guid UserId { get; set; }
        [Key, Column(Order = 1)]
        public int RoleId { get; set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }
        [ForeignKey("RoleId")] public virtual Role Role { get; set; }
    }

    public class Influencer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string FullName { get; set; }
        public string Bio { get; set; }
        [MaxLength(256)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Geography { get; set; }
        public AdvocacyStatus AdvocacyStatus { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public virtual ICollection<InfluencerTag> Tags { get; set; } = new List<InfluencerTag>();
        public virtual ICollection<SocialProfile> SocialProfiles { get; set; } = new List<SocialProfile>();
    }

    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }

    public class InfluencerTag
    {
        [Key, Column(Order = 0)] public Guid InfluencerId { get; set; }
        [Key, Column(Order = 1)] public int TagId { get; set; }
        [ForeignKey("InfluencerId")] public virtual Influencer Influencer { get; set; }
        [ForeignKey("TagId")] public virtual Tag Tag { get; set; }
    }

    public class SocialProfile
    {
        [Key]
        public int Id { get; set; }
        public Guid InfluencerId { get; set; }
        public SocialPlatform Platform { get; set; }
        public string Handle { get; set; }
        public string Url { get; set; }
        public long Followers { get; set; }
        public double EngagementRate { get; set; }
        public bool IsVerified { get; set; }
        [ForeignKey("InfluencerId")] public virtual Influencer Influencer { get; set; }
    }

    public class Collaboration
    {
        [Key]
        public int Id { get; set; }
        public Guid InfluencerId { get; set; }
        public Guid? CampaignId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string OutcomeNotes { get; set; }
    }

    public class Campaign
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public string Brief { get; set; }
        public string Objectives { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public CampaignStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class CampaignDeliverable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CampaignId { get; set; }
        public DeliverableType Type { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid? AssignedInfluencerId { get; set; }
        public DeliverableStatus Status { get; set; }
    }

    public class ContentSubmission
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CampaignId { get; set; }
        public Guid InfluencerId { get; set; }
        public Guid? DeliverableId { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string AssetPath { get; set; }
        public SubmissionState State { get; set; } = SubmissionState.Draft;
        public int CurrentVersionNumber { get; set; } = 0;
        public DateTime? SubmittedAt { get; set; }
    }

    public class ContentVersion
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SubmissionId { get; set; }
        public int VersionNumber { get; set; }
        public string Caption { get; set; }
        public string AssetPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserId { get; set; }
    }

    public class Approval
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SubmissionId { get; set; }
        public Guid ReviewerUserId { get; set; }
        public ApprovalDecision Decision { get; set; }
        public string Comments { get; set; }
        public DateTime DecidedAt { get; set; } = DateTime.UtcNow;
    }

    public class PerformanceMetric
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SubmissionId { get; set; }
        public int Reach { get; set; }
        public int Engagements { get; set; }
        public int Saves { get; set; }
        public int Shares { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public DateTime CapturedAt { get; set; } = DateTime.UtcNow;
    }

    // Enums
    public enum AdvocacyStatus { Prospect, Active, Ambassador }
    public enum SocialPlatform { Instagram, TikTok, YouTube, X, Other }
    public enum CampaignStatus { Draft, Active, Completed, Archived }
    public enum DeliverableType { Post, Story, UGC, Review, Event }
    public enum DeliverableStatus { Planned, InProgress, Submitted, Approved, Rejected }
    public enum SubmissionState { Draft, Submitted, Approved, Rejected }
    public enum ApprovalDecision { Approved, Rejected }
}
