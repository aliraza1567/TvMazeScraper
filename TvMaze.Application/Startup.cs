using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Application.Gateways;
using TvMaze.Application.Services;
using TvMaze.Infrastructure.Abstractions.TvMaze.Gateways;

namespace TvMaze.Application
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration? configuration)
        {
            //Services
            services.AddScoped(typeof(IShowsService), typeof(ShowsService));
            services.AddScoped(typeof(IScraperService), typeof(ScraperService));
            services.AddScoped(typeof(ICastsService), typeof(CastsService));

            //Gateways
            services.AddScoped(typeof(ITvMazeGateway), typeof(TvMazeGateway));
        }
    }
}
