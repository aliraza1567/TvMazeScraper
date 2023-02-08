using TvMaze.Domain.Models;

namespace TvMaze.Application.Abstractions.Services.Shows
{
    public interface IShowsService
    {
        Task<Show> GetAsync(Guid id, CancellationToken cancellationToken);
    }
}
