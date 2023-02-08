using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Domain.Models;

namespace TvMaze.Persistence.Abstractions.Repositories
{
    public interface IShowsRepository : IEntityRepository<Show, Guid>
    {

    }
}
