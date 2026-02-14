using IAT.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IAT.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=IATConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public AppDbContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Influencer> Influencers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<InfluencerTag> InfluencerTags { get; set; }
        public DbSet<SocialProfile> SocialProfiles { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignDeliverable> CampaignDeliverables { get; set; }

        public DbSet<ContentSubmission> ContentSubmissions { get; set; }
        public DbSet<ContentVersion> ContentVersions { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        public DbSet<PerformanceMetric> PerformanceMetrics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Tag>().HasIndex(t => t.Name).IsUnique();

            modelBuilder.Entity<SocialProfile>()
                .HasIndex(sp => new { sp.InfluencerId, sp.Platform })
                .IsUnique();

            modelBuilder.Entity<InfluencerTag>().HasKey(t => new { t.InfluencerId, t.TagId });
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<Influencer>().Property(i => i.Email).HasMaxLength(256);
        }
    }
}
