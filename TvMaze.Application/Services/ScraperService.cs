using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;
using TvMaze.Infrastructure.Abstractions.TvMaze.Gateways;
using TvMaze.Persistence.Abstractions.Repositories;

namespace TvMaze.Application.Services;

public class ScraperService : IScraperService
{
    private readonly ITvMazeGateway _tvMazeGateway;
    private readonly IShowsRepository _showsRepository;

    public ScraperService(ITvMazeGateway tvMazeGateway, IShowsRepository showsRepository)
    {
        _tvMazeGateway = tvMazeGateway;
        _showsRepository = showsRepository;
    }

    public async Task<bool> ShowAndCastScraperAsync(CancellationToken cancellationToken)
    {
        var mazeClient = await _tvMazeGateway.GetTvMazeClientAsync(cancellationToken);
        var allShows = await mazeClient.GetAllShowsAsync(cancellationToken);

        var entityInsertRequest = new EntityInsertRequest<Show>
        {
            EntitiesToInsert = allShows
        };

        foreach (var show in allShows)
        {
            var castList = await mazeClient.GetCastByShowIdAsync(show.ShowId, cancellationToken);

            Thread.Sleep(1000);

            show.Casts = castList;
        }

        await _showsRepository.BulkInsertAsync(entityInsertRequest, cancellationToken);

        return true;
    }
}