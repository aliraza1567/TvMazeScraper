using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TvMazeScraper.WebApi.Queries.Shows;

namespace TvMazeScraper.WebApi.Queries
{
    public static class QueryBuilderExtensions
    {
        public static void AddQueryBuilders(this IServiceCollection services, IConfiguration configuration, params Assembly[] callingAssemblies)
        {
            services.AddScoped(typeof(IShowsQueryBuilder), typeof(ShowsQueryBuilder));
        }
    }
}
