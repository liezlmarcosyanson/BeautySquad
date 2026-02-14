using System;
using System.Collections.Generic;

namespace IAT.Application.Services
{
    public interface ICampaignService
    {
        IEnumerable<CampaignDto> Query(string q = null);
        CampaignDto Get(Guid id);
        CampaignDto Create(CampaignCreateRequest request);
        CampaignDto Update(Guid id, CampaignCreateRequest request);
        bool SoftDelete(Guid id);
    }
}
