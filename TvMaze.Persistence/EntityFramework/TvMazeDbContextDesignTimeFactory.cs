#if DEBUG
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TvMaze.Persistence.EntityFramework
{
    class TicketsContextDesignTimeFactory : IDesignTimeDbContextFactory<TvMazeDbContext>
    {
        public TvMazeDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "..", "TvMazeScraper.WebApi", "appsettings.json")).Build();
            return new TvMazeDbContext(new DbContextOptionsBuilder().UseSqlServer(config.GetConnectionString("TvMazeDbConnection")).Options);
        }
    }
}
#endif