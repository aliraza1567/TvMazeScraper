using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
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
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
                Console.WriteLine(e);
                throw;
            }

            
        }
    }
}
