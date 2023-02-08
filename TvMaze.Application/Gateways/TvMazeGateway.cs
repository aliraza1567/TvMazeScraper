using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TvMaze.Infrastructure.Abstractions.TvMaze.Clients;
using TvMaze.Infrastructure.Abstractions.TvMaze.Gateways;

namespace TvMaze.Application.Gateways
{
    public class TvMazeGateway : ITvMazeGateway
    {
        private readonly IConfiguration _configuration;
        private readonly ITvMazeClientFactory _mazeClientFactory;
        private readonly ILogger<TvMazeGateway> _logger;
        public TvMazeGateway(IConfiguration configuration, ITvMazeClientFactory mazeClientFactory, ILogger<TvMazeGateway> logger)
        {
            _configuration = configuration;
            _mazeClientFactory = mazeClientFactory;
            _logger = logger;
        }
        public Task<ITvMazeClient> GetTvMazeClientAsync(CancellationToken cancellationToken = default)
        {
            var url = _configuration["TvMazeApiUrl"];

            if (string.IsNullOrEmpty(url))
            {
                _logger.LogInformation($"{nameof(GetTvMazeClientAsync)}|Configuration is missing.");
                return Task.FromResult<ITvMazeClient>(null!);
            }
            var mazeClient = _mazeClientFactory.Create(url);
            return Task.FromResult(mazeClient);
        }
    }
}
