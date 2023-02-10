using Microsoft.Extensions.Logging;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;
using TvMaze.Infrastructure.Abstractions.TvMaze.Gateways;

namespace TvMaze.Application.Services;

public class ScraperService : IScraperService
{
    private readonly ILogger<ScraperService> _logger;
    private readonly ITvMazeGateway _tvMazeGateway;
    private readonly IShowsService _showsService;

    public ScraperService(ITvMazeGateway tvMazeGateway, IShowsService showsService, ILogger<ScraperService> logger)
    {
        _tvMazeGateway = tvMazeGateway;
        _showsService = showsService;
        _logger = logger;
    }

    public async Task<bool> ShowAndCastScraperAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"DirectScraping Started At: {DateTime.Now}");

        var mazeClient = await _tvMazeGateway.GetTvMazeClientAsync(cancellationToken);

        var allShows = await mazeClient.GetAllShowsAsync(cancellationToken);

        //Check if show already exist
        var request = new EntityListRequest<Show>(e => e.ShowId, SortDirectionEnum.Ascending);
        var existingShows = await _showsService.GetAllShowAsync(request, cancellationToken);

        var saveableShows = allShows.Where(show => existingShows.Results.All(existingShow => existingShow.ShowId != show.ShowId)).ToList();

        if (!saveableShows.Any()) 
            return true;
        
        foreach (var show in saveableShows)
        {
            var castList = await mazeClient.GetCastByShowIdAsync(show.ShowId, cancellationToken);
            show.Casts = castList;
        }

        try
        {
            await _showsService.SaveShowsAsync(allShows, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Error while saving data, Exception: {e}");
            return false;
        }

        _logger.LogInformation($"DirectScraping Started At: {DateTime.Now}");

        return true;
    }
}