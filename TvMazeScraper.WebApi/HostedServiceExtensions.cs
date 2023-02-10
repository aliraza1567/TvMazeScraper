using System.Reflection;
using TvMaze.Application.Worker;

namespace TvMazeScraper.WebApi;

public static class HostedServiceExtensions
{
    public static void AccompanyingHostedService(this IServiceCollection services, IConfiguration configuration, params Assembly[] callingAssemblies)
    {
        //HostedService
        //services.AddHostedService<DataScraperHostedService>();
    }
}