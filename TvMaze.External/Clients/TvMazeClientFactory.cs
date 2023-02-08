using AutoMapper;
using Microsoft.Extensions.Logging;
using TvMaze.Infrastructure.Abstractions.TvMaze.Clients;

namespace TvMaze.External.Clients
{
    public class TvMazeClientFactory: ITvMazeClientFactory
    {
        private readonly ILogger<TvMazeClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        public TvMazeClientFactory(IMapper mapper, ILogger<TvMazeClient> logger, IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public ITvMazeClient Create(string url)
        {
            return new TvMazeClient(_logger, _httpClientFactory, url, _mapper);
        }
    }
}
