using TvMaze.Infrastructure.Abstractions.TvMaze.Clients;

namespace TvMaze.Infrastructure.Abstractions.TvMaze.Gateways
{
    public interface ITvMazeGateway
    {
        Task<ITvMazeClient> GetTvMazeClientAsync(CancellationToken cancellationToken = default);
    }
}
