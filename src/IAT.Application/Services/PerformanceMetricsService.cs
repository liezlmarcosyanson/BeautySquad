using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IAT.Domain;
using IAT.Infrastructure;

namespace IAT.Application.Services
{
    public interface IPerformanceMetricsService
    {
        PerformanceMetricsDto Get(Guid id);
        IEnumerable<PerformanceMetricsDto> GetBySubmission(Guid submissionId);
        PerformanceMetricsDto Record(Guid submissionId, PerformanceMetricsCreateRequest request);
        PerformanceMetricsDto GetLatest(Guid submissionId);
        Dictionary<string, object> GetSummaryStats(Guid submissionId);
    }

    public class PerformanceMetricsService : IPerformanceMetricsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PerformanceMetricsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public PerformanceMetricsDto Get(Guid id)
        {
            var metric = _uow.PerformanceMetrics.Query()
                .FirstOrDefault(m => m.Id == id);

            if (metric == null)
                return null;

            return _mapper.Map<PerformanceMetricsDto>(metric);
        }

        public IEnumerable<PerformanceMetricsDto> GetBySubmission(Guid submissionId)
        {
            var metrics = _uow.PerformanceMetrics.Query()
                .Where(m => m.SubmissionId == submissionId)
                .OrderBy(m => m.CapturedAt)
                .ToList();

            return metrics.Select(_mapper.Map<PerformanceMetricsDto>);
        }

        public PerformanceMetricsDto Record(Guid submissionId, PerformanceMetricsCreateRequest request)
        {
            var submission = _uow.ContentSubmissions.Query()
                .FirstOrDefault(cs => cs.Id == submissionId);

            if (submission == null)
                throw new ArgumentException("Submission not found");

            var metric = new PerformanceMetric
            {
                Id = Guid.NewGuid(),
                SubmissionId = submissionId,
                Reach = request.Reach,
                Engagements = request.Engagements,
                Saves = request.Saves,
                Shares = request.Shares,
                Clicks = request.Clicks,
                Conversions = request.Conversions,
                CapturedAt = request.CapturedAt ?? DateTime.UtcNow
            };

            _uow.PerformanceMetrics.Add(metric);
            _uow.Commit();

            return _mapper.Map<PerformanceMetricsDto>(metric);
        }

        public PerformanceMetricsDto GetLatest(Guid submissionId)
        {
            var metric = _uow.PerformanceMetrics.Query()
                .Where(m => m.SubmissionId == submissionId)
                .OrderByDescending(m => m.CapturedAt)
                .FirstOrDefault();

            if (metric == null)
                return null;

            return _mapper.Map<PerformanceMetricsDto>(metric);
        }

        public Dictionary<string, object> GetSummaryStats(Guid submissionId)
        {
            var metrics = _uow.PerformanceMetrics.Query()
                .Where(m => m.SubmissionId == submissionId)
                .ToList();

            if (metrics.Count == 0)
                return new Dictionary<string, object>();

            return new Dictionary<string, object>
            {
                { "count", metrics.Count },
                { "totalReach", metrics.Sum(m => m.Reach) },
                { "totalEngagements", metrics.Sum(m => m.Engagements) },
                { "totalSaves", metrics.Sum(m => m.Saves) },
                { "totalShares", metrics.Sum(m => m.Shares) },
                { "totalClicks", metrics.Sum(m => m.Clicks) },
                { "totalConversions", metrics.Sum(m => m.Conversions) },
                { "averageEngagementRate", metrics.Count > 0 
                    ? Math.Round((double)metrics.Sum(m => m.Engagements) / metrics.Sum(m => m.Reach), 4) 
                    : 0 },
                { "firstCaptured", metrics.Min(m => m.CapturedAt) },
                { "lastCaptured", metrics.Max(m => m.CapturedAt) }
            };
        }
    }
}
