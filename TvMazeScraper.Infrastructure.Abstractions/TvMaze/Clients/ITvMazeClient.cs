using TvMaze.Domain.Models;

namespace TvMaze.Infrastructure.Abstractions.TvMaze.Clients
{
    public interface ITvMazeClient
    {
        Task<List<Show>> GetAllShowsAsync(CancellationToken cancellationToken = default);
    }
}
