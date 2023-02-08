﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Application.Worker;

namespace TvMazeScraper.WebApi.Controllers
{
    public class ScraperController : ControllerBase
    {
        private readonly ILogger<ScraperController> _logger;
        private readonly IScraperService _scraperService;
        private readonly IServiceProvider _serviceProvider;

        public ScraperController(ILogger<ScraperController> logger, IScraperService scraperService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _scraperService = scraperService;
            _serviceProvider = serviceProvider;
        }

        [HttpGet("DirectScraping")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DirectScraping(CancellationToken cancellationToken = default)
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
