using Microsoft.AspNetCore.Mvc;
using System.Net;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;
using TvMazeScraper.WebApi.Contracts.Shows;
using TvMazeScraper.WebApi.Queries;
using TvMazeScraper.WebApi.Queries.Shows;

namespace TvMazeScraper.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : BaseController
    {
        private readonly ILogger<ShowsController> _logger;
        private readonly IShowsService _showsService;
        private readonly IShowsQueryBuilder _showsQueryBuilder;

        public ShowsController(ILogger<ShowsController> logger, IShowsService showsService, IShowsQueryBuilder showsQueryBuilder)
        {
            _logger = logger;
            _showsService = showsService;
            _showsQueryBuilder = showsQueryBuilder;
        }

        [HttpGet("GetAllShowsCastSorted")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ShowDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public virtual async Task<ActionResult> GetAllCastSorted(CancellationToken cancellationToken = default)
        {
            var listRequest = new EntityListRequest<Show>(e => e.ShowId, SortDirectionEnum.Ascending);

            var showEntity = await _showsService.GetAllWithCastSortedAsync(listRequest, cancellationToken);

            var result = Mapper.Map<IList<ShowDto>>(showEntity);

            if (!result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetAllShows")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ShowDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public virtual async Task<ActionResult> GetAll([FromQuery] ListShowsQueryDto? listRequestResource, CancellationToken cancellationToken = default)
        {
            listRequestResource ??= new ListShowsQueryDto();

            var listRequest = _showsQueryBuilder.Build(listRequestResource);

            var listResult = await _showsService.GetAllShowAsync(listRequest, cancellationToken);

            var listResponseResource = Mapper.Map<ListResponseDto<ShowDto>>(listResult);

            if (listResponseResource.ResultCount < 0)
                return NotFound();

            return Ok(listResponseResource);
        }


        [HttpGet("GetByShowId")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ShowDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int showId, CancellationToken cancellationToken = default)
        {
            var showEntity = await _showsService.GetByShowIdAsync(showId, cancellationToken);

            var result = Mapper.Map<ShowDto>(showEntity);
            
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}