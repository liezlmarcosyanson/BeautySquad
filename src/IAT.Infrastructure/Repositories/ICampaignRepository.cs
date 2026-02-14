using IAT.Domain;
using System.Linq;

namespace IAT.Infrastructure.Repositories
{
    public interface ICampaignRepository : IAT.Infrastructure.IRepository<Campaign>
    {
        IQueryable<Campaign> QueryActive();
    }
}
