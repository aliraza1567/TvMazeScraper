using System.Linq.Expressions;

namespace TvMaze.Domain.Persistence
{
    public abstract class BulkRequest<TEntity>
        where TEntity : class, IEntity
    {
        protected BulkRequest()
        {
            Sorting = new SortingOptions<TEntity>();
            Paging = new PagingOptions();
        }
        public SortItem<TEntity> DefaultSortItem { get; protected set; }
        public Expression<Func<TEntity, bool>> Where { get; set; }
        public SortingOptions<TEntity> Sorting { get; set; }
        public PagingOptions Paging { get; set; }
    }

    public class EntityListRequest<TEntity> : BulkRequest<TEntity> where TEntity : class, IEntity
    {

        public bool OnlyCount { get; set; }

        public EntityListRequest()
        {
            
        }

    }

    public class EntityUpdateRequest<TEntity> : BulkRequest<TEntity> where TEntity : class, IEntity
    {
        public Action<TEntity> UpdateLogic { get; set; }

        public EntityUpdateRequest(Expression<Func<TEntity, object>> sortField, SortDirectionEnum sortDirection)
        {
            DefaultSortItem = new SortItem<TEntity>()
            {
                SortDirection = sortDirection,
                SortField = sortField
            };
        }
    }

    public class EntityDeleteRequest<TEntity> : BulkRequest<TEntity> where TEntity : class, IEntity
    {

        public EntityDeleteRequest(Expression<Func<TEntity, object>> sortField, SortDirectionEnum sortDirection)
        {
            DefaultSortItem = new SortItem<TEntity>()
            {
                SortDirection = sortDirection,
                SortField = sortField
            };
        }
    }

    public class EntityInsertRequest<TEntity> where TEntity : class, IEntity
    {
        public List<TEntity> EntitiesToInsert { get; set; }

        public EntityInsertRequest()
        {

        }
    }
}
