using System.Reflection;

namespace TvMazeScraper.WebApi
{
    internal static class StartupCompositionRoot
    {
        public static void AddCompositionRootServices(this IServiceCollection services, IConfiguration? configuration, params Assembly[] callingAssemblies)
        {
            services.AddOptions();
            TvMaze.Application.Startup.ConfigureServices(services, configuration);
            TvMaze.External.Startup.ConfigureServices(services, configuration);
            TvMaze.Persistence.Startup.ConfigureServices(services, configuration);
        }
    }
}
