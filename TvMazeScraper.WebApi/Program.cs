using NLog;
using NLog.Web;
using TvMaze.Persistence;

namespace TvMazeScraper.WebApi
{
    public class Program
    {
        public static IConfiguration? Configuration { get; set; }
        public static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Info("TvMazeScraper Web Api is booting up");

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                Configuration = builder.Configuration;

                // Add services to the container.

                builder.Services.AddControllers();

                //Configure NLog
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();


                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddAutoMapper(typeof(Program).Assembly);
                builder.Services.AddCompositionRootServices(Configuration, typeof(Startup).Assembly);

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                logger.Fatal($"Exception starting up TvMazeScraper Web Api, Exception: {ex}");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}