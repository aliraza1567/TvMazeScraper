using System.Linq;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;
using TvMaze.Infrastructure.Abstractions.TvMaze.Gateways;

namespace TvMaze.Application.Services;

public class ScraperService : IScraperService
{
    private readonly ITvMazeGateway _tvMazeGateway;
    private readonly IShowsService _showsService;

    public ScraperService(ITvMazeGateway tvMazeGateway, IShowsService showsService)
    {
        _tvMazeGateway = tvMazeGateway;
        _showsService = showsService;
    }

    public async Task<bool> ShowAndCastScraperAsync(CancellationToken cancellationToken)
    {
        var mazeClient = await _tvMazeGateway.GetTvMazeClientAsync(cancellationToken);

        var allShows = await mazeClient.GetAllShowsAsync(cancellationToken);

        //Check if show already exist
        var existingShows = await _showsService.GetAllShow(cancellationToken);
        var saveableShows = allShows.Where(show => existingShows.All(existingShow => existingShow.ShowId != show.ShowId)).ToList();

        if (!saveableShows.Any()) 
            return true;
        
        foreach (var show in saveableShows)
        {
            var castList = await mazeClient.GetCastByShowIdAsync(show.ShowId, cancellationToken);
            show.Casts = castList;
        }

        await _showsService.SaveShows(allShows, cancellationToken);

        return true;
    }
}