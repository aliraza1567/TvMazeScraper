using System.Reflection;
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


