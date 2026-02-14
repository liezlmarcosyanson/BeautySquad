using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IAT.Infrastructure;
using IAT.Domain;

namespace IAT.Application.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CampaignService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<CampaignDto> Query(string q = null)
        {
            var query = _uow.Campaigns.Query();
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(c => c.Name.Contains(q) || c.Brief.Contains(q));
            }
            return query.Where(c => !c.IsDeleted).ToList().Select(_mapper.Map<CampaignDto>);
        }

        public CampaignDto Get(Guid id)
        {
            var campaign = _uow.Campaigns.Query().FirstOrDefault(c => c.Id == id);
            if (campaign == null || campaign.IsDeleted) return null;
            return _mapper.Map<CampaignDto>(campaign);
        }

        public CampaignDto Create(CampaignCreateRequest request)
        {
            var campaign = new Campaign
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Brief = request.Brief,
                Objectives = request.Objectives,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Budget = request.Budget,
                Status = CampaignStatus.Draft,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _uow.Campaigns.Add(campaign);
            _uow.Commit();
            return _mapper.Map<CampaignDto>(campaign);
        }

        public CampaignDto Update(Guid id, CampaignCreateRequest request)
        {
            var campaign = _uow.Campaigns.Query().FirstOrDefault(c => c.Id == id);
            if (campaign == null || campaign.IsDeleted) return null;
            
            campaign.Name = request.Name;
            campaign.Brief = request.Brief;
            campaign.Objectives = request.Objectives;
            campaign.StartDate = request.StartDate;
            campaign.EndDate = request.EndDate;
            campaign.Budget = request.Budget;

            _uow.Campaigns.Update(campaign);
            _uow.Commit();
            return _mapper.Map<CampaignDto>(campaign);
        }

        public bool SoftDelete(Guid id)
        {
            var campaign = _uow.Campaigns.Query().FirstOrDefault(c => c.Id == id);
            if (campaign == null || campaign.IsDeleted) return false;
            campaign.IsDeleted = true;
            _uow.Campaigns.Update(campaign);
            _uow.Commit();
            return true;
        }
    }
}

