using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvMazeScraper.Infrastructure.Abstractions.TvMaze.Clients
{
    public interface ITvMazeClientFactory
    {
        ITvMazeClient Create(string url);
    }
}
