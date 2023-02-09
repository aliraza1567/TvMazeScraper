using TvMaze.Domain.Persistence;

namespace TvMaze.Persistence.EntityFramework
{
    public interface IEfQueryBuilder
    {
        IQueryable<TEntity> GetQueryForResults<TEntity>(BulkRequest<TEntity> request, IQueryable<TEntity> queryable) where TEntity : class, IEntity;
        IQueryable<TEntity> GetQueryForTotalResults<TEntity>(BulkRequest<TEntity> request, IQueryable<TEntity> queryable) where TEntity : class, IEntity;
    }

}
