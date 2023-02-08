using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Persistence.Abstractions.Repositories;
using TvMaze.Persistence.EntityFramework;
using TvMaze.Persistence.Repositories;

namespace TvMaze.Persistence
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration? configuration)
        {
            services.AddScoped<IShowsRepository, ShowsRepository>();
            //services.AddScoped<IMessageRepository, MessageEfRepository>();
            //services.AddScoped<ITicketRepository, TicketEfRepository>();
            //services.AddScoped<IPincodeRepository, PincodeEfRepository>();
            //services.AddScoped<ICounterRepository, CounterEfRepository>();
            //services.AddScoped<IPharmacyRepository, PharmacyEfRepository>();

            services.AddDbContext<TvMazeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TvMazeDbConnection"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

            services.AddScoped(typeof(DbContext), typeof(TvMazeDbContext));
            services.AddScoped(typeof(IEfUnitOfWork), typeof(EfUnitOfWork));
            services.AddScoped(typeof(IEfQueryBuilder), typeof(EfQueryBuilder));

            //services.AddScoped(typeof(IMessageRepository), typeof(MessageEfRepository));
            //services.AddScoped(typeof(IVariableRepository), typeof(VariableEfRepository));
            //services.AddScoped(typeof(IJobRunRepository), typeof(JobRunEfRepository));
            //services.AddScoped(typeof(IJobRepository), typeof(JobEfRepository));
        }
    }
}
