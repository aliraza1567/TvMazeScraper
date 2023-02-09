using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TvMaze.Domain.Persistence;

namespace TvMaze.Persistence.EntityFramework
{
    public interface IEfUnitOfWork
    {
        DbSet<TEntity> Entities<TEntity>() where TEntity : class, IEntity;

        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);

        TEntity GetOriginalEntity<TEntity>(TEntity updatedEntry) where TEntity : class, IEntity;
    }
}
