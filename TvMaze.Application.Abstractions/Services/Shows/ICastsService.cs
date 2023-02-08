using TvMaze.Domain.Models;

namespace TvMaze.Application.Abstractions.Services.Shows;

public interface ICastsService
{
    Task<Cast> GetAsync(Guid id, CancellationToken cancellationToken);
}