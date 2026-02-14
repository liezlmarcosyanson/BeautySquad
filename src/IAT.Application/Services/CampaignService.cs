using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IAT.Application.DTOs;
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
                query = query.Where(c => c.Name.Contains(q) || c.Description.Contains(q));
            }
            return query.Where(c => !c.IsDeleted).ToList().Select(_mapper.Map<CampaignDto>);
        }

        public CampaignDto Get(Guid id)
        {
            var c = _uow.Campaigns.Get(id);
            if (c == null || c.IsDeleted) return null;
            return _mapper.Map<CampaignDto>(c);
        }

        public CampaignDto Create(CampaignCreateRequest request)
        {
            var campaign = new Campaign
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Start = request.Start,
                End = request.End,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            if (request.Deliverables != null)
            {
                campaign.Deliverables = request.Deliverables.Select(d => new CampaignDeliverable
                {
                    Id = Guid.NewGuid(),
                    CampaignId = campaign.Id,
                    Title = d.Title,
                    Description = d.Description,
                    Required = d.Required
                }).ToList();
            }

            _uow.Campaigns.Add(campaign);
            _uow.Commit();
            return _mapper.Map<CampaignDto>(campaign);
        }

        public CampaignDto Update(Guid id, CampaignCreateRequest request)
        {
            var campaign = _uow.Campaigns.Get(id);
            if (campaign == null || campaign.IsDeleted) return null;
            campaign.Name = request.Name;
            campaign.Description = request.Description;
            campaign.Start = request.Start;
            campaign.End = request.End;

            // basic deliverable sync: replace
            if (request.Deliverables != null)
            {
                campaign.Deliverables.Clear();
                foreach (var d in request.Deliverables)
                {
                    campaign.Deliverables.Add(new CampaignDeliverable
                    {
                        Id = Guid.NewGuid(),
                        CampaignId = campaign.Id,
                        Title = d.Title,
                        Description = d.Description,
                        Required = d.Required
                    });
                }
            }

            _uow.Campaigns.Update(campaign);
            _uow.Commit();
            return _mapper.Map<CampaignDto>(campaign);
        }

        public bool SoftDelete(Guid id)
        {
            var campaign = _uow.Campaigns.Get(id);
            if (campaign == null || campaign.IsDeleted) return false;
            campaign.IsDeleted = true;
            _uow.Campaigns.Update(campaign);
            _uow.Commit();
            return true;
        }
    }
}
