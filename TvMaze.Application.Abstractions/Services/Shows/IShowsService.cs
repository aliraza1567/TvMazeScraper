using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;

namespace TvMaze.Application.Abstractions.Services.Shows
{
    public interface IShowsService
    {
        Task<Show> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<Show> GetByShowIdAsync(int showId, CancellationToken cancellationToken);
        Task<IList<Show>> GetAllWithCastSorted(CancellationToken cancellationToken);
        Task<IList<Show>> GetAllShow(CancellationToken cancellationToken);
        Task SaveShows(List<Show> allShows, CancellationToken cancellationToken);
    }
}
