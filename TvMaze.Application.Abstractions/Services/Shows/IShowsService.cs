using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;

namespace TvMaze.Application.Abstractions.Services.Shows
{
    public interface IShowsService
    {
        Task<Show> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<Show> GetByShowIdAsync(int showId, CancellationToken cancellationToken);
        Task<IList<Show>> GetAllWithCastSortedAsync(EntityListRequest<Show> listRequest, CancellationToken cancellationToken);
        Task<IList<Show>> GetAllShowAsync(CancellationToken cancellationToken);
        Task<EntityInsertResponse<Show>> SaveShowsAsync(List<Show> allShows, CancellationToken cancellationToken);
    }
}
