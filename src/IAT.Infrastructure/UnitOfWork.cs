using IAT.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace IAT.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _ctx;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<T>();
        }
        public IQueryable<T> Query() => _dbSet;
        public void Add(T entity) => _dbSet.Add(entity);
        public void Update(T entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Influencer> Influencers { get; }
        IRepository<Tag> Tags { get; }
        IRepository<SocialProfile> SocialProfiles { get; }
        IRepository<InfluencerTag> InfluencerTags { get; }
        IRepository<Campaign> Campaigns { get; }
        IRepository<CampaignDeliverable> CampaignDeliverables { get; }
        IRepository<ContentSubmission> ContentSubmissions { get; }
        IRepository<ContentVersion> ContentVersions { get; }
        IRepository<Approval> Approvals { get; }
        IRepository<PerformanceMetric> PerformanceMetrics { get; }
        int SaveChanges();
        int Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;
        public IRepository<User> Users { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<Influencer> Influencers { get; }
        public IRepository<Tag> Tags { get; }
        public IRepository<SocialProfile> SocialProfiles { get; }
        public IRepository<InfluencerTag> InfluencerTags { get; }
        public IRepository<Campaign> Campaigns { get; }
        public IRepository<CampaignDeliverable> CampaignDeliverables { get; }
        public IRepository<ContentSubmission> ContentSubmissions { get; }
        public IRepository<ContentVersion> ContentVersions { get; }
        public IRepository<Approval> Approvals { get; }
        public IRepository<PerformanceMetric> PerformanceMetrics { get; }

        public UnitOfWork(AppDbContext ctx)
        {
            _ctx = ctx;
            Users = new GenericRepository<User>(ctx);
            Roles = new GenericRepository<Role>(ctx);
            Influencers = new GenericRepository<Influencer>(ctx);
            Tags = new GenericRepository<Tag>(ctx);
            SocialProfiles = new GenericRepository<SocialProfile>(ctx);
            InfluencerTags = new GenericRepository<InfluencerTag>(ctx);
            Campaigns = new GenericRepository<Campaign>(ctx);
            CampaignDeliverables = new GenericRepository<CampaignDeliverable>(ctx);
            ContentSubmissions = new GenericRepository<ContentSubmission>(ctx);
            ContentVersions = new GenericRepository<ContentVersion>(ctx);
            Approvals = new GenericRepository<Approval>(ctx);
            PerformanceMetrics = new GenericRepository<PerformanceMetric>(ctx);
        }

        public int SaveChanges() => _ctx.SaveChanges();
        public int Commit() => SaveChanges();
        public void Dispose() => _ctx.Dispose();
    }
}
