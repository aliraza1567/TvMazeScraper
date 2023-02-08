using Microsoft.AspNetCore.Mvc;
using System.Net;
using TvMaze.Application.Abstractions.Services.Shows;

namespace TvMazeScraper.WebApi.Controllers
{
    public class ScraperController : ControllerBase
    {
        private readonly ILogger<ScraperController> _logger;
        private readonly IScraperService _scraperService;

        public ScraperController(ILogger<ScraperController> logger, IScraperService scraperService)
        {
            _logger = logger;
            _scraperService = scraperService;
        }

        [HttpGet("ScrapeAllShows")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ScrapeAllShows(CancellationToken cancellationToken = default)
        {
            var isSuccess = await _scraperService.ShowAndCastScraperAsync(cancellationToken);

            if (isSuccess)
            {
                return Ok();
            }

            return Problem();
        }
    }
}
