using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IAT.Application.Services;
using IAT.Application;

namespace IAT.WebApi.Controllers
{
    [RoutePrefix("api/campaigns")]
    public class CampaignsController : ApiController
    {
        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri] string q = null)
        {
            var items = _campaignService.Query(q);
            return Ok(items);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            var item = _campaignService.Get(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] CampaignCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = _campaignService.Create(request);
            return Created(new Uri(Request.RequestUri, $"{created.Id}"), created);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult Update(Guid id, [FromBody] CampaignCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = _campaignService.Update(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            var ok = _campaignService.SoftDelete(id);
            if (!ok) return NotFound();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
