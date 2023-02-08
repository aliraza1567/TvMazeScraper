using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.External.Clients;
using TvMaze.Infrastructure.Abstractions.TvMaze.Clients;

namespace TvMaze.External
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration? configuration)
        {
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddHttpClient(TvMazeClient.ClientName);

            services.AddSingleton<ITvMazeClientFactory, TvMazeClientFactory>();
        }

    }
}
