using System.Linq.Expressions;

namespace TvMaze.Domain.Persistence
{
    public class EntityFindRequest<TEntity>
        where TEntity : class, IEntity
    {

        public Expression<Func<TEntity, bool>> Where { get; set; }
        public SortingOptions<TEntity> Sorting { get; set; }
    }
}
