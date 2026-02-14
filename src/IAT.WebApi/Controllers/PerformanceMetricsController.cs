using System;
using System.Web.Http;
using System.Web.Http.Cors;
using IAT.Application;
using IAT.Application.Services;

namespace IAT.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/performance-metrics")]
    public class PerformanceMetricsController : ApiController
    {
        private readonly IPerformanceMetricsService _metricsService;

        public PerformanceMetricsController(IPerformanceMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        /// <summary>
        /// Get a specific performance metric record
        /// </summary>
        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var metric = _metricsService.Get(id);
                if (metric == null)
                    return NotFound();

                return Ok(metric);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all performance metrics for a submission
        /// </summary>
        [HttpGet]
        [Route("submission/{submissionId:guid}")]
        public IHttpActionResult GetBySubmission(Guid submissionId)
        {
            try
            {
                var metrics = _metricsService.GetBySubmission(submissionId);
                return Ok(metrics);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get the latest performance metrics for a submission
        /// </summary>
        [HttpGet]
        [Route("submission/{submissionId:guid}/latest")]
        public IHttpActionResult GetLatest(Guid submissionId)
        {
            try
            {
                var metric = _metricsService.GetLatest(submissionId);
                if (metric == null)
                    return NotFound();

                return Ok(metric);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get summary statistics for a submission
        /// </summary>
        [HttpGet]
        [Route("submission/{submissionId:guid}/summary")]
        public IHttpActionResult GetSummary(Guid submissionId)
        {
            try
            {
                var summary = _metricsService.GetSummaryStats(submissionId);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Record new performance metrics for a submission
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Record([FromBody] PerformanceMetricsRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var metricsRequest = new PerformanceMetricsCreateRequest
                {
                    Reach = request.Reach,
                    Engagements = request.Engagements,
                    Saves = request.Saves,
                    Shares = request.Shares,
                    Clicks = request.Clicks,
                    Conversions = request.Conversions,
                    CapturedAt = request.CapturedAt
                };

                var recorded = _metricsService.Record(request.SubmissionId, metricsRequest);
                return Created(new Uri(Request.RequestUri, recorded.Id.ToString()), recorded);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    /// <summary>
    /// Model for recording performance metrics
    /// </summary>
    public class PerformanceMetricsRequestModel
    {
        public Guid SubmissionId { get; set; }
        public int Reach { get; set; }
        public int Engagements { get; set; }
        public int Saves { get; set; }
        public int Shares { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public DateTime? CapturedAt { get; set; }
    }
}
