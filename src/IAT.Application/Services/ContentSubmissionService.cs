using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IAT.Application.DTOs;
using IAT.Domain;
using IAT.Infrastructure;

namespace IAT.Application.Services
{
    public interface IContentSubmissionService
    {
        IEnumerable<ContentSubmissionDto> GetByCampaign(Guid campaignId);
        IEnumerable<ContentSubmissionDto> GetByInfluencer(Guid influencerId);
        ContentSubmissionDto Get(Guid id);
        ContentSubmissionDto Create(ContentSubmissionCreateRequest request);
        ContentSubmissionDto UpdateCaption(Guid id, string caption);
        ContentSubmissionDto Submit(Guid id);
        bool Delete(Guid id);
    }

    public class ContentSubmissionService : IContentSubmissionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ContentSubmissionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<ContentSubmissionDto> GetByCampaign(Guid campaignId)
        {
            var submissions = _uow.ContentSubmissions.Query()
                .Where(cs => cs.CampaignId == campaignId && cs.State != SubmissionState.Draft)
                .ToList();

            return submissions.Select(_mapper.Map<ContentSubmissionDto>);
        }

        public IEnumerable<ContentSubmissionDto> GetByInfluencer(Guid influencerId)
        {
            var submissions = _uow.ContentSubmissions.Query()
                .Where(cs => cs.InfluencerId == influencerId)
                .ToList();

            return submissions.Select(_mapper.Map<ContentSubmissionDto>);
        }

        public ContentSubmissionDto Get(Guid id)
        {
            var submission = _uow.ContentSubmissions.Query()
                .FirstOrDefault(cs => cs.Id == id);

            if (submission == null)
                return null;

            return _mapper.Map<ContentSubmissionDto>(submission);
        }

        public ContentSubmissionDto Create(ContentSubmissionCreateRequest request)
        {
            // Verify campaign and influencer exist
            var campaign = _uow.Campaigns.Query().FirstOrDefault(c => c.Id == request.CampaignId && !c.IsDeleted);
            if (campaign == null)
                throw new ArgumentException("Campaign not found");

            var influencer = _uow.Influencers.Query().FirstOrDefault(i => i.Id == request.InfluencerId && !i.IsDeleted);
            if (influencer == null)
                throw new ArgumentException("Influencer not found");

            var submission = new ContentSubmission
            {
                Id = Guid.NewGuid(),
                CampaignId = request.CampaignId,
                InfluencerId = request.InfluencerId,
                Title = request.Title,
                Caption = request.Caption,
                State = SubmissionState.Draft,
                CurrentVersionNumber = 0,
                SubmittedAt = null
            };

            _uow.ContentSubmissions.Add(submission);
            _uow.Commit();

            return _mapper.Map<ContentSubmissionDto>(submission);
        }

        public ContentSubmissionDto UpdateCaption(Guid id, string caption)
        {
            var submission = _uow.ContentSubmissions.Query()
                .FirstOrDefault(cs => cs.Id == id);

            if (submission == null)
                throw new ArgumentException("Submission not found");

            if (submission.State != SubmissionState.Draft)
                throw new InvalidOperationException("Can only update draft submissions");

            submission.Caption = caption;
            _uow.ContentSubmissions.Update(submission);
            _uow.Commit();

            return _mapper.Map<ContentSubmissionDto>(submission);
        }

        public ContentSubmissionDto Submit(Guid id)
        {
            var submission = _uow.ContentSubmissions.Query()
                .FirstOrDefault(cs => cs.Id == id);

            if (submission == null)
                throw new ArgumentException("Submission not found");

            if (submission.State != SubmissionState.Draft)
                throw new InvalidOperationException("Only draft submissions can be submitted");

            submission.State = SubmissionState.Submitted;
            submission.SubmittedAt = DateTime.UtcNow;
            submission.CurrentVersionNumber = 1;

            // Create first version
            var version = new ContentVersion
            {
                Id = Guid.NewGuid(),
                SubmissionId = submission.Id,
                VersionNumber = 1,
                Caption = submission.Caption,
                AssetPath = submission.AssetPath,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = Guid.Empty // Would be set from current user context
            };

            _uow.ContentSubmissions.Update(submission);
            _uow.ContentVersions.Add(version);
            _uow.Commit();

            return _mapper.Map<ContentSubmissionDto>(submission);
        }

        public bool Delete(Guid id)
        {
            var submission = _uow.ContentSubmissions.Query()
                .FirstOrDefault(cs => cs.Id == id);

            if (submission == null)
                return false;

            _uow.ContentSubmissions.Delete(submission);
            _uow.Commit();
            return true;
        }
    }
}
