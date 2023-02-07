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
        //public DbSet<Country> Countries { get; set; }
        //public DbSet<External> Externals { get; set; }
        //public DbSet<Image> Images { get; set; }
        //public DbSet<Link> Links { get; set; }
        //public DbSet<Network> Networks { get; set; }
        //public DbSet<PreviousEpisode> PreviousEpisodes { get; set; }
        //public DbSet<Rating> Ratings { get; set; }
        //public DbSet<Schedule> Schedules { get; set; }
        //public DbSet<Self> Selves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }

}
