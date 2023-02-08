using TvMaze.Domain.Models;

namespace TvMaze.Application.Abstractions.Services.Shows
{
    public interface IShowsService
    {
        Task<Show> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<Show> GetByShowIdAsync(int showId, CancellationToken cancellationToken);
        Task<IList<Show>> GetAll(CancellationToken cancellationToken);
    }
}
