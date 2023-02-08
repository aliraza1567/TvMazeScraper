using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvMaze.Domain.Models;
using TvMaze.External.Clients.Contracts.Show;

namespace TvMaze.External.Mappings
{
    internal class TvMazeExternalProfile: Profile
    {
        public TvMazeExternalProfile()
        {
            CreateMap<ShowDto, Show>();
        }
    }
}
