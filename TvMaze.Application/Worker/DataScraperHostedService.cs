using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Application.Abstractions.Services.Shows;

namespace TvMaze.Application.Worker
{
    public class DataScraperHostedService: BackgroundService
    {
        private readonly ILogger<DataScraperHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        public DataScraperHostedService(ILogger<DataScraperHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.Run(async () =>
        {
            _logger.LogInformation("DataScraperHostedService is starting.");

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var scraperService = scope.ServiceProvider.GetRequiredService<IScraperService>();

                var isSuccess = await scraperService.ShowAndCastScraperAsync(stoppingToken);

                _logger.LogInformation(isSuccess
                    ? "DataScraperHostedService is successfully completed."
                    : "DataScraperHostedService is ended abnormally.");
            }
            catch (Exception e)
            {
                _logger.LogError($"DataScraperHostedService, Exception: {e}");
                throw;
            }


        }, stoppingToken);
    }
}
