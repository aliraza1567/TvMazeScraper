using TvMazeScraper.Infrastructure.Abstractions.TvMaze.Clients;

namespace TvMazeScraper.Infrastructure.Abstractions.TvMaze.Gateways
{
    internal interface ITvMazeGateway
    {
        Task<ITvMazeClient> GetTvMazeClientAsync(CancellationToken cancellationToken = default);
    }
}
