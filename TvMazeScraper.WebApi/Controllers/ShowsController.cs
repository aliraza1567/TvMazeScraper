using Microsoft.AspNetCore.Mvc;
using System.Net;
using TvMazeScraper.WebApi.Contracts.Shows;

namespace TvMazeScraper.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly ILogger<ShowsController> _logger;

        public ShowsController(ILogger<ShowsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ShowDto))]
        public virtual Task<ActionResult> GetAll(CancellationToken cancellationToken = default)
        {

            return Task.FromResult<ActionResult>(Ok());
        }
    }
}