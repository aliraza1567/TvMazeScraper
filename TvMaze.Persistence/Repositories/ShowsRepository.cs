using TvMaze.Domain.Models;
using TvMaze.Persistence.Abstractions.Repositories;
using TvMaze.Persistence.EntityFramework;

namespace TvMaze.Persistence.Repositories
{
    internal class ShowsRepository: EfEntityRepositoryBase<Show, Guid>, IShowsRepository
    {
        public ShowsRepository(IEfUnitOfWork unitOfWork, IEfQueryBuilder queryBuilder) : base(unitOfWork, queryBuilder)
        {
        }
    }
}
