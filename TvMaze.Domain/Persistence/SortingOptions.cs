using System.Linq.Expressions;

namespace TvMaze.Domain.Persistence
{
    public class SortingOptions<TEntity> where TEntity : class, IEntity
    {
        public SortingOptions()
        {
            SortItems = new List<SortItem<TEntity>>();
        }

        public List<SortItem<TEntity>> SortItems { get; set; }

        public bool HasSortingOptions => SortItems.Count > 0;
    }

    public class SortItem<TEntity>
    {
        public SortDirectionEnum SortDirection { get; set; }
        public Expression<Func<TEntity, object>> SortField { get; set; }
    }
}
