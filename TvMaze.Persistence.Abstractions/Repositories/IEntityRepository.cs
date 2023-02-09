using System.Linq.Expressions;
using TvMaze.Domain.Persistence;

namespace TvMaze.Persistence.Abstractions.Repositories
{
    public interface IEntityRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>
    {
        Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default);

        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<EntityListResponse<TEntity>> ListAsync(EntityListRequest<TEntity> request, CancellationToken cancellation = default);

        Task<EntityUpdateResponse<TEntity>> BulkUpdateAsync(EntityUpdateRequest<TEntity> request, CancellationToken cancellation = default);

        Task<EntityDeleteResponse<TEntity>> BulkDeleteAsync(EntityDeleteRequest<TEntity> request, CancellationToken cancellation = default);

        Task<EntityInsertResponse<TEntity>> BulkInsertAsync(EntityInsertRequest<TEntity> request, CancellationToken cancellation = default);
    }
}
