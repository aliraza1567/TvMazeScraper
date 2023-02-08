using AutoMapper;
using TvMaze.Domain.Models;
using TvMazeScraper.WebApi.Contracts.Shows;

namespace TvMazeScraper.WebApi.Mappings
{
    internal class CastMappings : Profile
    {
        public CastMappings()
        {
            CreateMap<Cast, CastDto>();
        }
    }
}
