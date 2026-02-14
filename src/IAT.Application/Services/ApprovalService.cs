using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IAT.Domain;
using IAT.Infrastructure;

namespace IAT.Application.Services
{
    public interface IApprovalService
    {
        ApprovalDto Get(Guid id);
        IEnumerable<ApprovalDto> GetBySubmission(Guid submissionId);
        IEnumerable<ApprovalDto> GetPendingForReviewer(Guid reviewerId);
        ApprovalDto Approve(Guid submissionId, Guid reviewerId, string comments = null);
        ApprovalDto Reject(Guid submissionId, Guid reviewerId, string comments);
    }

    public class ApprovalService : IApprovalService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ApprovalService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ApprovalDto Get(Guid id)
        {
            var approval = _uow.Approvals.Query()
                .FirstOrDefault(a => a.Id == id);

            if (approval == null)
                return null;

            return _mapper.Map<ApprovalDto>(approval);
        }

        public IEnumerable<ApprovalDto> GetBySubmission(Guid submissionId)
        {
            var approvals = _uow.Approvals.Query()
                .Where(a => a.SubmissionId == submissionId)
                .OrderByDescending(a => a.DecidedAt)
                .ToList();

            return approvals.Select(_mapper.Map<ApprovalDto>);
        }

        public IEnumerable<ApprovalDto> GetPendingForReviewer(Guid reviewerId)
        {
            var submissions = _uow.ContentSubmissions.Query()
                .Where(cs => cs.State == SubmissionState.Submitted)
                .Select(cs => cs.Id)
                .ToList();

            var approvals = _uow.Approvals.Query()
                .Where(a => submissions.Contains(a.SubmissionId) && a.ReviewerUserId == reviewerId)
                .ToList();

            return approvals.Select(_mapper.Map<ApprovalDto>);
        }

        public ApprovalDto Approve(Guid submissionId, Guid reviewerId, string comments = null)
        {
            var submission = _uow.ContentSubmissions.Query()
                .FirstOrDefault(cs => cs.Id == submissionId);

            if (submission == null)
                throw new ArgumentException("Submission not found");

            if (submission.State != SubmissionState.Submitted)
                throw new InvalidOperationException("Only submitted content can be approved");

            submission.State = SubmissionState.Approved;
            _uow.ContentSubmissions.Update(submission);

            var approval = new Approval
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                ReviewerUserId = reviewerId,
                Decision = ApprovalDecision.Approved,
                Comments = comments,
                DecidedAt = DateTime.UtcNow
            };

            _uow.Approvals.Add(approval);
            _uow.Commit();

            return _mapper.Map<ApprovalDto>(approval);
        }

        public ApprovalDto Reject(Guid submissionId, Guid reviewerId, string comments)
        {
            if (string.IsNullOrWhiteSpace(comments))
                throw new ArgumentException("Rejection must include comments");

            var submission = _uow.ContentSubmissions.Query()
                .FirstOrDefault(cs => cs.Id == submissionId);

            if (submission == null)
                throw new ArgumentException("Submission not found");

            if (submission.State != SubmissionState.Submitted)
                throw new InvalidOperationException("Only submitted content can be rejected");

            submission.State = SubmissionState.Rejected;
            _uow.ContentSubmissions.Update(submission);

            var approval = new Approval
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                ReviewerUserId = reviewerId,
                Decision = ApprovalDecision.Rejected,
                Comments = comments,
                DecidedAt = DateTime.UtcNow
            };

            _uow.Approvals.Add(approval);
            _uow.Commit();

            return _mapper.Map<ApprovalDto>(approval);
        }
    }
}
