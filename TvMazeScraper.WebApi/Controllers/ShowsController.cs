using Microsoft.AspNetCore.Mvc;
using System.Net;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMazeScraper.WebApi.Contracts.Shows;

namespace TvMazeScraper.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : BaseController
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
        public virtual async Task<ActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var showEntity = await _showsService.GetAll(cancellationToken);

            var result = Mapper.Map<IList<ShowDto>>(showEntity);
            return Ok(result);
        }

        [HttpGet("GetByShowId")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ShowDto))]
        public async Task<IActionResult> Get(int showId, CancellationToken cancellationToken = default)
        {
            var showEntity = await _showsService.GetByShowIdAsync(showId, cancellationToken);

            var result = Mapper.Map<ShowDto>(showEntity);
            return Ok(result);
        }
    }
}