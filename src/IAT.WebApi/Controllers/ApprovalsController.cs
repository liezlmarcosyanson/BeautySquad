using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using IAT.Application.Services;

namespace IAT.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/approvals")]
    public class ApprovalsController : ApiController
    {
        private readonly IApprovalService _approvalService;

        public ApprovalsController(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        /// <summary>
        /// Get a specific approval record
        /// </summary>
        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var approval = _approvalService.Get(id);
                if (approval == null)
                    return NotFound();

                return Ok(approval);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all approvals for a submission
        /// </summary>
        [HttpGet]
        [Route("submission/{submissionId:guid}")]
        public IHttpActionResult GetBySubmission(Guid submissionId)
        {
            try
            {
                var approvals = _approvalService.GetBySubmission(submissionId);
                return Ok(approvals);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get pending approvals for a reviewer
        /// </summary>
        [HttpGet]
        [Route("pending/{reviewerId:guid}")]
        public IHttpActionResult GetPending(Guid reviewerId)
        {
            try
            {
                var pending = _approvalService.GetPendingForReviewer(reviewerId);
                return Ok(pending);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Approve a content submission
        /// </summary>
        [HttpPost]
        [Route("{submissionId:guid}/approve")]
        public IHttpActionResult Approve(Guid submissionId, [FromBody] dynamic request)
        {
            try
            {
                var reviewerId = Guid.Parse((string)request.reviewerId);
                var comments = (string)request.comments ?? null;

                var approval = _approvalService.Approve(submissionId, reviewerId, comments);
                return Created(new Uri(Request.RequestUri, approval.Id.ToString()), approval);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Reject a content submission
        /// </summary>
        [HttpPost]
        [Route("{submissionId:guid}/reject")]
        public IHttpActionResult Reject(Guid submissionId, [FromBody] dynamic request)
        {
            try
            {
                var reviewerId = Guid.Parse((string)request.reviewerId);
                var comments = (string)request.comments;

                var approval = _approvalService.Reject(submissionId, reviewerId, comments);
                return Created(new Uri(Request.RequestUri, approval.Id.ToString()), approval);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
