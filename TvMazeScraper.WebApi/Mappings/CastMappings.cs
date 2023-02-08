using AutoMapper;
using TvMazeScraper.WebApi.Contracts.Shows;

namespace TvMazeScraper.WebApi.Mappings
{
    internal class CastMappings : Profile
    {
        public CastMappings()
        {
            CreateMap<CastDto, TvMaze.Domain.Models.Cast>();
        }
    }
}
