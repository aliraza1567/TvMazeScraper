using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TvMaze.Domain.Persistence;
using TvMaze.Persistence.Abstractions.Repositories;

namespace TvMaze.Persistence.EntityFramework
{
    public abstract class EfEntityRepositoryBase<TEntity, TKey> : EfRepositoryBase, IEntityRepository<TEntity, TKey>
         where TEntity : class, IEntity<TKey>
    {
        protected IEfQueryBuilder QueryBuilder { get; }

        protected EfEntityRepositoryBase(IEfUnitOfWork unitOfWork, IEfQueryBuilder queryBuilder) : base(unitOfWork)
        {
            QueryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        public IQueryable<TEntity> Queryable => UnitOfWork.Entities<TEntity>().AsQueryable();
        public virtual IQueryable<TEntity> GetQueryable => Queryable;
        public virtual IQueryable<TEntity> FindQueryable => Queryable;
        public virtual IQueryable<TEntity> GetTrackedQueryable => Queryable;
        public virtual IQueryable<TEntity> ListQueryable => Queryable;
        public virtual IQueryable<TEntity> BulkUpdateQueryable => Queryable;
        public virtual IQueryable<TEntity> BulkDeleteQueryable => Queryable;
        public virtual IQueryable<TEntity> BulkInsertQueryable => Queryable;

        public virtual Expression<Func<TEntity, TEntity>> ProjectionExpression => entity => entity;
        public virtual Expression<Func<TEntity, TEntity>> GetProjectionExpression => ProjectionExpression;
        public virtual Expression<Func<TEntity, TEntity>> ListProjectionExpression => ProjectionExpression;


        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default)
        {
            var query = GetQueryable.Where(wherePredicate);
            return await query.AsNoTracking().SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default)
        {
            var query = GetQueryable.Where(wherePredicate);
            return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellation = default)
        {
            var query = UnitOfWork.Entities<TEntity>().Where(wherePredicate);
            return await query.AsNoTracking().AnyAsync(cancellation);
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var removedEntity = UnitOfWork.Entities<TEntity>().Remove(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return removedEntity.Entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var updatedEntity = UnitOfWork.Entities<TEntity>().Update(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return updatedEntity.Entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var insertEntity = await UnitOfWork.Entities<TEntity>().AddAsync(entity, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return insertEntity.Entity;
        }

        public async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var query = UnitOfWork.Entities<TEntity>();
            var result = await query.AnyAsync(e => e.Id.Equals(id), cancellationToken);
            return result;
        }

        public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var result = await GetQueryable.AsNoTracking().Select(GetProjectionExpression).SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
            return result;
        }

        protected async Task<TEntity> GetTrackedAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var result = await GetTrackedQueryable.AsTracking().SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
            return result;
        }

        public async Task<EntityListResponse<TEntity>> ListAsync(EntityListRequest<TEntity> request, CancellationToken cancellation = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.Snapshot, cancellation))
            {
                var queryForResults = QueryBuilder.GetQueryForResults(request, ListQueryable);
                var queryForTotalResults = QueryBuilder.GetQueryForTotalResults(request, ListQueryable);

                if (request.OnlyCount)
                {
                    var countQuery = UnitOfWork.Entities<TEntity>().Select(e => new
                    {
                        TotalResultCount = queryForTotalResults.AsNoTracking().Count(),
                        ResultCount = queryForResults.AsNoTracking().Count()
                    });
                    var countQueryResult = await countQuery.FirstOrDefaultAsync(cancellation);

                    await transaction.CommitAsync(cancellation);
                    return new EntityListResponse<TEntity>(countQueryResult?.ResultCount ?? 0, countQueryResult?.TotalResultCount ?? 0, request.Paging?.SkipCount ?? 0);
                }

                var resultQuery = UnitOfWork.Entities<TEntity>().Select(e => new
                {
                    TotalResultCount = queryForTotalResults.AsNoTracking().Count(),
                    Results = queryForResults.AsNoTracking().AsSplitQuery().Select(ListProjectionExpression).ToList()
                });
                var resultQueryResult = await resultQuery.FirstOrDefaultAsync(cancellation);

                await transaction.CommitAsync(cancellation);
                return new EntityListResponse<TEntity>(resultQueryResult?.Results ?? new List<TEntity>(), resultQueryResult?.TotalResultCount ?? 0, request.Paging?.SkipCount ?? 0);
            };
        }

        public async Task<EntityUpdateResponse<TEntity>> BulkUpdateAsync(EntityUpdateRequest<TEntity> request, CancellationToken cancellation = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellation))
            {
                var queryForResults = QueryBuilder.GetQueryForResults(request, BulkUpdateQueryable);

                var results = queryForResults.AsTracking().ToList();

                foreach (var entityToUpdate in results)
                {
                    request.UpdateLogic.Invoke(entityToUpdate);
                    UnitOfWork.Entities<TEntity>().Update(entityToUpdate);
                }

                await UnitOfWork.SaveChangesAsync(cancellation);
                await transaction.CommitAsync(cancellation);
                return new EntityUpdateResponse<TEntity>(results);
            }
        }

        public async Task<EntityDeleteResponse<TEntity>> BulkDeleteAsync(EntityDeleteRequest<TEntity> request, CancellationToken cancellation = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellation))
            {
                var queryForResults = QueryBuilder.GetQueryForResults(request, BulkDeleteQueryable);
                var entitiesToDelete = queryForResults.AsTracking().ToList();

                UnitOfWork.Entities<TEntity>().RemoveRange(entitiesToDelete);

                await UnitOfWork.SaveChangesAsync(cancellation);
                await transaction.CommitAsync(cancellation);

                return new EntityDeleteResponse<TEntity>(entitiesToDelete);
            }
        }

        public async Task<EntityInsertResponse<TEntity>> BulkInsertAsync(EntityInsertRequest<TEntity> request, CancellationToken cancellationToken = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken))
            {
                UnitOfWork.Entities<TEntity>().AddRange(request.EntitiesToInsert);

                await UnitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return new EntityInsertResponse<TEntity>(request.EntitiesToInsert);
            }
        }

        public async Task<TEntity> FindAsync(EntityFindRequest<TEntity> request, CancellationToken cancellationToken = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.Snapshot, cancellationToken))
            {
                var queryForResults = QueryBuilder.GetQueryForResults(request, FindQueryable);
                
                var result = await queryForResults.AsNoTracking().FirstOrDefaultAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                
                return result;
            }
        }
    }

    public abstract class EfEntityRepositoryBase<TEntity> : EfRepositoryBase, IEntityRepository<TEntity>
         where TEntity : class, IEntity
    {
        protected IEfQueryBuilder QueryBuilder { get; }

        protected EfEntityRepositoryBase(IEfUnitOfWork unitOfWork, IEfQueryBuilder queryBuilder) : base(unitOfWork)
        {
            QueryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        public IQueryable<TEntity> Queryable => UnitOfWork.Entities<TEntity>().AsQueryable();
        public virtual IQueryable<TEntity> GetQueryable => Queryable;
        public virtual IQueryable<TEntity> GetTrackedQueryable => Queryable;
        public virtual IQueryable<TEntity> ListQueryable => Queryable;
        public virtual IQueryable<TEntity> BulkUpdateQueryable => Queryable;
        public virtual IQueryable<TEntity> BulkDeleteQueryable => Queryable;
        public virtual IQueryable<TEntity> BulkInsertQueryable => Queryable;

        public virtual Expression<Func<TEntity, TEntity>> ProjectionExpression => entity => entity;
        public virtual Expression<Func<TEntity, TEntity>> GetProjectionExpression => ProjectionExpression;
        public virtual Expression<Func<TEntity, TEntity>> ListProjectionExpression => ProjectionExpression;
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default)
        {
            var query = GetQueryable.Where(wherePredicate);
            return await query.AsNoTracking().SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default)
        {
            var query = GetQueryable.Where(wherePredicate);
            return await query.AsNoTracking().SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellation = default)
        {
            var query = UnitOfWork.Entities<TEntity>().Where(wherePredicate);
            return await query.AsNoTracking().AnyAsync(cancellation);
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var removedEntity = UnitOfWork.Entities<TEntity>().Remove(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return removedEntity.Entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var updatedEntity = UnitOfWork.Entities<TEntity>().Update(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return updatedEntity.Entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var insertEntity = await UnitOfWork.Entities<TEntity>().AddAsync(entity, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return insertEntity.Entity;
        }

        public async Task<EntityListResponse<TEntity>> ListAsync(EntityListRequest<TEntity> request, CancellationToken cancellation = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.Snapshot, cancellation))
            {
                var queryForResults = QueryBuilder.GetQueryForResults(request, ListQueryable);
                var queryForTotalResults = QueryBuilder.GetQueryForTotalResults(request, ListQueryable);

                if (request.OnlyCount)
                {
                    var countQuery = UnitOfWork.Entities<TEntity>().Select(e => new
                    {
                        TotalResultCount = queryForTotalResults.AsNoTracking().Count(),
                        ResultCount = queryForResults.AsNoTracking().Count()
                    });
                    var countQueryResult = await countQuery.FirstOrDefaultAsync(cancellation);

                    await transaction.CommitAsync(cancellation);

                    return new EntityListResponse<TEntity>(countQueryResult?.ResultCount ?? 0, countQueryResult?.TotalResultCount ?? 0, request.Paging?.SkipCount ?? 0);
                }

                var resultQuery = UnitOfWork.Entities<TEntity>().Select(e => new
                {
                    TotalResultCount = queryForTotalResults.AsNoTracking().Count(),
                    Results = queryForResults.AsNoTracking().AsSplitQuery().Select(ListProjectionExpression).ToList()
                });
                var resultQueryResult = await resultQuery.FirstOrDefaultAsync(cancellation);

                await transaction.CommitAsync(cancellation);

                return new EntityListResponse<TEntity>(resultQueryResult?.Results ?? new List<TEntity>(), resultQueryResult?.TotalResultCount ?? 0, request.Paging?.SkipCount ?? 0);
            };
        }

        public async Task<EntityUpdateResponse<TEntity>> BulkUpdateAsync(EntityUpdateRequest<TEntity> request, CancellationToken cancellation = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellation))
            {
                var queryForResults = QueryBuilder.GetQueryForResults(request, BulkUpdateQueryable);

                var results = queryForResults.AsTracking().ToList();

                foreach (var entityToUpdate in results)
                {
                    request.UpdateLogic.Invoke(entityToUpdate);
                    UnitOfWork.Entities<TEntity>().Update(entityToUpdate);
                }

                await UnitOfWork.SaveChangesAsync(cancellation);
                await transaction.CommitAsync(cancellation);
                return new EntityUpdateResponse<TEntity>(results);
            }
        }

        public async Task<EntityDeleteResponse<TEntity>> BulkDeleteAsync(EntityDeleteRequest<TEntity> request, CancellationToken cancellation = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellation))
            {
                var queryForResults = QueryBuilder.GetQueryForResults(request, BulkDeleteQueryable);

                var entitiesToDelete = queryForResults.AsTracking().ToList();

                UnitOfWork.Entities<TEntity>().RemoveRange(entitiesToDelete);

                await UnitOfWork.SaveChangesAsync(cancellation);
                await transaction.CommitAsync(cancellation);

                return new EntityDeleteResponse<TEntity>(entitiesToDelete);
            }
        }

        public async Task<EntityInsertResponse<TEntity>> BulkInsertAsync(EntityInsertRequest<TEntity> request, CancellationToken cancellationToken = default)
        {
            await using (var transaction = await UnitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken))
            {
                UnitOfWork.Entities<TEntity>().AddRange(request.EntitiesToInsert);

                await UnitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return new EntityInsertResponse<TEntity>(request.EntitiesToInsert);
            }
        }
    }

}
