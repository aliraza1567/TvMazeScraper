using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Persistence.Abstractions.Repositories;

namespace TvMaze.Application.Services
{
    public class ShowsService: IShowsService
    {
        private readonly IShowsRepository _showsRepository;

        public ShowsService(IShowsRepository showsRepository)
        {
            _showsRepository = showsRepository;
        }

        public async Task<Show> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var show = await _showsRepository.GetAsync(id, cancellationToken);
            return show;
        }
    }
}
