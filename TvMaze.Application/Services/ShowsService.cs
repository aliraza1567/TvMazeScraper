using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Infrastructure.Abstractions.TvMaze.Gateways;
using TvMaze.Persistence.Abstractions.Repositories;

namespace TvMaze.Application.Services
{
    public class ShowsService: IShowsService
    {
        private readonly IShowsRepository _showsRepository;
        private readonly ITvMazeGateway _tvMazeGateway;

        public ShowsService(IShowsRepository showsRepository, ITvMazeGateway tvMazeGateway)
        {
            _showsRepository = showsRepository;
            _tvMazeGateway = tvMazeGateway;
        }

        public async Task<Show> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var mazeClient = await _tvMazeGateway.GetTvMazeClientAsync(cancellationToken);

            var shows = await mazeClient.GetAllShowsAsync(cancellationToken);

            var show = await _showsRepository.GetAsync(id, cancellationToken);
            return show;
        }
    }
}
