using TvMaze.Domain.Persistence;

namespace TvMaze.Persistence.EntityFramework
{
    public class EfQueryBuilder : IEfQueryBuilder
    {
        public IEfUnitOfWork UnitOfWork { get; }

        public EfQueryBuilder(IEfUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IQueryable<TEntity> GetQueryForResults<TEntity>(BulkRequest<TEntity> request, IQueryable<TEntity> query) where TEntity : class, IEntity
        {
            if (request.Where != null)
            {
                query = query.Where(request.Where);
            }

            if (request.Sorting.HasSortingOptions)
            {
                for (var sortItemCount = 0; sortItemCount < request.Sorting.SortItems.Count; sortItemCount++)
                {
                    var sortItem = request.Sorting.SortItems[sortItemCount];
                    if (sortItem.SortDirection == SortDirectionEnum.Ascending)
                    {
                        query = sortItemCount == 0
                            ? query.OrderBy(sortItem.SortField)
                            : ((IOrderedQueryable<TEntity>)query).ThenBy(sortItem.SortField);
                    }
                    else
                    {
                        query = sortItemCount == 0
                            ? query.OrderByDescending(sortItem.SortField)
                            : ((IOrderedQueryable<TEntity>)query).ThenByDescending(
                                sortItem.SortField);
                    }
                }
            }

            if (request.Paging.HasPagingOptions)
            {
                if (!request.Sorting.HasSortingOptions)
                {
                    if (request.DefaultSortItem.SortDirection == SortDirectionEnum.Ascending)
                    {
                        query = query.OrderBy(request.DefaultSortItem.SortField);
                    }
                    else
                    {
                        query = query.OrderByDescending(request.DefaultSortItem.SortField);
                    }
                }

                if (request.Paging.SkipCount > 0)
                {
                    query = query.Skip(request.Paging.SkipCount);
                }

                if (request.Paging.TakeCount > 0)
                {
                    query = query.Take(request.Paging.TakeCount);
                }
            }

            return query;
        }

        public IQueryable<TEntity> GetQueryForTotalResults<TEntity>(BulkRequest<TEntity> request, IQueryable<TEntity> query) where TEntity : class, IEntity
        {
            if (request.Where != null)
            {
                query = query.Where(request.Where);
            }

            return query;
        }

        public IQueryable<TEntity> GetQueryForResults<TEntity>(EntityFindRequest<TEntity> request, IQueryable<TEntity> query) where TEntity : class, IEntity
        {
            if (request.Where != null)
            {
                query = query.Where(request.Where);
            }
            if (request.Sorting.HasSortingOptions)
            {
                for (var sortItemCount = 0; sortItemCount < request.Sorting.SortItems.Count; sortItemCount++)
                {
                    var sortItem = request.Sorting.SortItems[sortItemCount];
                    if (sortItem.SortDirection == SortDirectionEnum.Ascending)
                    {
                        query = sortItemCount == 0
                            ? query.OrderBy(sortItem.SortField)
                            : ((IOrderedQueryable<TEntity>)query).ThenBy(sortItem.SortField);
                    }
                    else
                    {
                        query = sortItemCount == 0
                            ? query.OrderByDescending(sortItem.SortField)
                            : ((IOrderedQueryable<TEntity>)query).ThenByDescending(
                                sortItem.SortField);
                    }
                }
            }
            return query;
        }
    }

}
