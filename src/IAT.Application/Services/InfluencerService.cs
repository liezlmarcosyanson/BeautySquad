using AutoMapper;
using IAT.Domain;
using IAT.Infrastructure;
using System;
using System.Linq;

namespace IAT.Application.Services
{
    public interface IInfluencerService
    {
        InfluencerDto Create(InfluencerCreateRequest req);
        InfluencerDto Get(Guid id);
        IQueryable<Influencer> Query();
        void Update(Guid id, InfluencerCreateRequest req);
        void SoftDelete(Guid id);
    }

    public class InfluencerService : IInfluencerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public InfluencerService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public InfluencerDto Create(InfluencerCreateRequest req)
        {
            var entity = _mapper.Map<Influencer>(req);
            _uow.Influencers.Add(entity);
            _uow.SaveChanges();

            if (req.Tags != null && req.Tags.Length > 0)
            {
                foreach (var tagName in req.Tags.Distinct())
                {
                    var tag = _uow.Tags.Query().FirstOrDefault(t => t.Name == tagName) ?? new Tag { Name = tagName };
                    if (tag.Id == 0) _uow.Tags.Add(tag);
                    _uow.SaveChanges();
                    _uow.InfluencerTags.Add(new InfluencerTag { InfluencerId = entity.Id, TagId = tag.Id });
                }
                _uow.SaveChanges();
            }

            return _mapper.Map<InfluencerDto>(entity);
        }

        public InfluencerDto Get(Guid id)
        {
            var entity = _uow.Influencers.Query().FirstOrDefault(i => i.Id == id && !i.IsDeleted);
            if (entity == null) return null;
            return _mapper.Map<InfluencerDto>(entity);
        }

        public IQueryable<Influencer> Query()
        {
            return _uow.Influencers.Query().Where(i => !i.IsDeleted);
        }

        public void Update(Guid id, InfluencerCreateRequest req)
        {
            var entity = _uow.Influencers.Query().FirstOrDefault(i => i.Id == id && !i.IsDeleted);
            if (entity == null) throw new ArgumentException("Not found");
            entity.FullName = req.FullName ?? entity.FullName;
            entity.Email = req.Email ?? entity.Email;
            entity.Bio = req.Bio ?? entity.Bio;
            entity.Phone = req.Phone ?? entity.Phone;
            entity.Geography = req.Geography ?? entity.Geography;
            entity.ModifiedAt = DateTime.UtcNow;
            _uow.Influencers.Update(entity);
            _uow.SaveChanges();
        }

        public void SoftDelete(Guid id)
        {
            var entity = _uow.Influencers.Query().FirstOrDefault(i => i.Id == id && !i.IsDeleted);
            if (entity == null) throw new ArgumentException("Not found");
            entity.IsDeleted = true;
            _uow.Influencers.Update(entity);
            _uow.SaveChanges();
        }
    }
}
