using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Persistence.Abstractions.Repositories;

namespace TvMaze.Application.Services;

public class CastsService : ICastsService
{
    private readonly ICastsRepository _castsRepository;

    public CastsService(ICastsRepository castsRepository)
    {
        _castsRepository = castsRepository;
    }

    public async Task<Cast> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var cast = await _castsRepository.GetAsync(id, cancellationToken);
        return cast;
    }
}