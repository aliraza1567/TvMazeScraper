using Microsoft.AspNetCore.Mvc;
using System.Net;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMazeScraper.WebApi.Contracts.Shows;

namespace TvMazeScraper.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly ILogger<ShowsController> _logger;
        private readonly IShowsService _showsService;

        public ShowsController(ILogger<ShowsController> logger, IShowsService showsService)
        {
            _logger = logger;
            _showsService = showsService;
        }

        [HttpGet("GetAllShows")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ShowDto>))]
        public virtual Task<ActionResult> GetAll(CancellationToken cancellationToken = default)
        {

            return Task.FromResult<ActionResult>(Ok());
        }

        [HttpGet("GetShowById")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ShowDto))]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var show = await _showsService.GetAsync(id, cancellationToken);
            return Ok(show);
        }
    }
}