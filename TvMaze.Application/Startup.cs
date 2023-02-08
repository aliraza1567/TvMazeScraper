using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

            //Gateways
            services.AddScoped(typeof(ITvMazeGateway), typeof(TvMazeGateway));
        }
    }
}
