using System.Linq.Expressions;

namespace TvMaze.Domain.Persistence
{
    public abstract class BulkRequest<TEntity> where TEntity : class, IEntity
    {
        protected BulkRequest()
        {
            Sorting = new SortingOptions<TEntity>();
            Paging = new PagingOptions();
        }
        public Expression<Func<TEntity, bool>> Where { get; set; }
        public SortingOptions<TEntity> Sorting { get; set; }
        public PagingOptions Paging { get; set; }
    }

    public class EntityListRequest<TEntity> : BulkRequest<TEntity> where TEntity : class, IEntity
    {
        public bool OnlyCount { get; set; }

        public EntityListRequest(Expression<Func<TEntity, object>> sortField, SortDirectionEnum sortDirection) : base()
        {
            var defaultSortItem = new SortItem<TEntity>()
            {
                SortDirection = sortDirection,
                SortField = sortField
            };
            Sorting.SortItems.Add(defaultSortItem);
        }
    }

    public class EntityUpdateRequest<TEntity> : BulkRequest<TEntity> where TEntity : class, IEntity
    {
        public Action<TEntity> UpdateLogic { get; set; }

        public EntityUpdateRequest()
        {
        }
    }

    public class EntityDeleteRequest<TEntity> : BulkRequest<TEntity> where TEntity : class, IEntity
    {
        public EntityDeleteRequest()
        {

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
