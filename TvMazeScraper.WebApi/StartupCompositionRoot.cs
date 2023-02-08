using System.Reflection;

namespace TvMazeScraper.WebApi
{
    internal static class StartupCompositionRoot
    {
        public static void AddCompositionRootServices(this IServiceCollection services, IConfiguration? configuration, params Assembly[] callingAssemblies)
        {
            services.AddOptions();
            //Presentation
            //services.AddScoped(typeof(IMessageSender), typeof(MessageSender));
            //Application
            TvMaze.Application.Startup.ConfigureServices(services, configuration);
            //Infrastructure.Startup.ConfigureServices(services, configuration);
            TvMaze.Persistence.Startup.ConfigureServices(services, configuration);
            //MessageBroker.Startup.ConfigureServices(services, configuration);
        }
    }
}
