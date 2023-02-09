using Microsoft.EntityFrameworkCore;
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

        public override IQueryable<Show> ListQueryable => Queryable.Include(x => x.Casts);

        public async Task<Show> GetByShowIdAsync(int showId, CancellationToken cancellationToken)
        {
            var query = Queryable
                .Include(x => x.Casts);

            return await query.AsNoTracking().FirstOrDefaultAsync(e => e.ShowId == showId, cancellationToken);
        }
    }
}
