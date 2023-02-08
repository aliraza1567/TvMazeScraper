using TvMaze.Domain.Models;
using TvMaze.Persistence.Abstractions.Repositories;
using TvMaze.Persistence.EntityFramework;

namespace TvMaze.Persistence.Repositories;

internal class CastRepository : EfEntityRepositoryBase<Cast, Guid>, ICastsRepository
{
    public CastRepository(IEfUnitOfWork unitOfWork, IEfQueryBuilder queryBuilder) : base(unitOfWork, queryBuilder)
    {
    }
}