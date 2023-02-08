using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TvMaze.Domain.Models;

namespace TvMaze.Persistence.EntityFramework
{
    public class TvMazeDbContext : DbContext
    {
        public TvMazeDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Cast> Casts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }

}
