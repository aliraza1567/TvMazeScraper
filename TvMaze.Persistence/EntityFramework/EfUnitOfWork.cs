using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using TvMaze.Domain.Persistence;
using TvMaze.Persistence.Abstractions.Exceptions;

namespace TvMaze.Persistence.EntityFramework
{
    public class EfUnitOfWork : IEfUnitOfWork
    {
        protected readonly DbContext DbContext;

        public EfUnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public DbSet<TEntity> Entities<TEntity>()
            where TEntity : class, IEntity
        {
            return DbContext.Set<TEntity>();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            return DbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                DbContext.ChangeTracker.Clear();
                var message = string.Join(", ", ex.Message, ex.InnerException?.Message);
                throw new RepositoryException($"SaveChangesAsync failed. Message: {message}", ex);
            }
        }

        public TEntity GetOriginalEntity<TEntity>(TEntity updatedEntity)
            where TEntity : class, IEntity
        {
            Func<PropertyValues, Type, object> getOriginal = null;
            getOriginal = (originalValues, type) =>
            {
                object original = Activator.CreateInstance(type, true);
                foreach (var property in originalValues.Properties)
                {
                    var propertyOnDestinationType = type.GetProperty(property.Name);
                    object value = originalValues[property.Name];

                    if (value is PropertyValues) //nested complex object
                    {
                        propertyOnDestinationType.SetValue(original, getOriginal(value as PropertyValues, propertyOnDestinationType.PropertyType));
                    }
                    else
                    {
                        propertyOnDestinationType.SetValue(original, value);
                    }
                }
                return original;
            };

            return (TEntity)getOriginal(DbContext.Entry(updatedEntity).OriginalValues, typeof(TEntity));
        }

    }
}
