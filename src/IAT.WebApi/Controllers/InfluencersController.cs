using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using FluentValidation.WebApi;
using IAT.Application;
using IAT.Application.Services;

namespace IAT.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/influencers")]
    public class InfluencersController : ApiController
    {
        private readonly IInfluencerService _influencerService;

        public InfluencersController(IInfluencerService influencerService)
        {
            _influencerService = influencerService;
        }

        /// <summary>
        /// Get all influencers with optional search
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var influencers = _influencerService.Query()
                .Select(i => new
                {
                    i.Id,
                    i.FullName,
                    i.Email,
                    i.Bio,
                    i.Geography,
                    i.AdvocacyStatus,
                    TagCount = i.Tags.Count,
                    SocialProfileCount = i.SocialProfiles.Count
                })
                .ToList();

            return Ok(influencers);
        }

        /// <summary>
        /// Get a specific influencer by ID
        /// </summary>
        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            var influencer = _influencerService.Get(id);
            if (influencer == null)
            {
                return NotFound();
            }
            return Ok(influencer);
        }

        /// <summary>
        /// Create a new influencer
        /// </summary>
        [HttpPost]
        [Route("")]
        [CustomizeValidator(RulesetName = "default")]
        public IHttpActionResult Create([FromBody] InfluencerCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = _influencerService.Create(request);
                return Created(new Uri(Request.RequestUri, created.Id.ToString()), created);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update an existing influencer
        /// </summary>
        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult Update(Guid id, [FromBody] InfluencerCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _influencerService.Update(id, request);
                var updated = _influencerService.Get(id);
                return Ok(updated);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Soft delete an influencer
        /// </summary>
        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                _influencerService.SoftDelete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
