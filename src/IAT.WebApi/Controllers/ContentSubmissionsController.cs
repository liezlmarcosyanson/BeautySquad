using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using IAT.Application;
using IAT.Application.Services;

namespace IAT.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/content-submissions")]
    public class ContentSubmissionsController : ApiController
    {
        private readonly IContentSubmissionService _contentSubmissionService;

        public ContentSubmissionsController(IContentSubmissionService contentSubmissionService)
        {
            _contentSubmissionService = contentSubmissionService;
        }

        /// <summary>
        /// Get all submissions for a campaign
        /// </summary>
        [HttpGet]
        [Route("campaign/{campaignId:guid}")]
        public IHttpActionResult GetByCampaign(Guid campaignId)
        {
            try
            {
                var submissions = _contentSubmissionService.GetByCampaign(campaignId);
                return Ok(submissions);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all submissions by an influencer
        /// </summary>
        [HttpGet]
        [Route("influencer/{influencerId:guid}")]
        public IHttpActionResult GetByInfluencer(Guid influencerId)
        {
            try
            {
                var submissions = _contentSubmissionService.GetByInfluencer(influencerId);
                return Ok(submissions);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get a specific submission
        /// </summary>
        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var submission = _contentSubmissionService.Get(id);
                if (submission == null)
                    return NotFound();

                return Ok(submission);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create a new content submission (starts in Draft state)
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] ContentSubmissionCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = _contentSubmissionService.Create(request);
                return Created(new Uri(Request.RequestUri, created.Id.ToString()), created);
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

        /// <summary>
        /// Update caption of a draft submission
        /// </summary>
        [HttpPut]
        [Route("{id:guid}/caption")]
        public IHttpActionResult UpdateCaption(Guid id, [FromBody] dynamic request)
        {
            try
            {
                var caption = (string)request.caption;
                var updated = _contentSubmissionService.UpdateCaption(id, caption);
                return Ok(updated);
            }
            catch (ArgumentException)
            {
                return NotFound();
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
        /// Submit a draft submission for approval
        /// </summary>
        [HttpPost]
        [Route("{id:guid}/submit")]
        public IHttpActionResult Submit(Guid id)
        {
            try
            {
                var submitted = _contentSubmissionService.Submit(id);
                return Ok(submitted);
            }
            catch (ArgumentException)
            {
                return NotFound();
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
        /// Delete a submission
        /// </summary>
        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var deleted = _contentSubmissionService.Delete(id);
                if (!deleted)
                    return NotFound();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
