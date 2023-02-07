using TvMaze.Domain.Models;

namespace TvMazeScraper.Infrastructure.Abstractions.TvMaze.Clients
{
    public interface ITvMazeClient
    {
        Task<Show> GetAllShowsAsync(CancellationToken cancellationToken = default);
    }
}
