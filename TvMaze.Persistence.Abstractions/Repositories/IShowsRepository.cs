using TvMaze.Domain.Models;

namespace TvMaze.Persistence.Abstractions.Repositories
{
    public interface IShowsRepository : IEntityRepository<Show, Guid>
    {
        Task<Show> GetByShowIdAsync(int showId, CancellationToken cancellationToken);
    }
}
