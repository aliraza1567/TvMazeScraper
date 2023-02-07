using TvMaze.Domain.Models;

namespace TvMaze.Infrastructure.Abstractions.TvMaze.Clients
{
    public interface ITvMazeClient
    {
        Task<Show> GetAllShowsAsync(CancellationToken cancellationToken = default);
    }
}
