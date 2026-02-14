using System;
using System.Linq;
using IAT.Domain;

namespace IAT.Infrastructure.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext ctx) : base(ctx) { }

        public IQueryable<Campaign> QueryActive()
        {
            return Query().Where(c => !c.IsDeleted);
        }
    }
}
