using AutoMapper;
using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;
using TvMazeScraper.WebApi.Contracts.Shows;
using TvMazeScraper.WebApi.Queries;

namespace TvMazeScraper.WebApi.Mappings
{
    internal class ShowMappings: Profile
    {
        public ShowMappings()
        {
            CreateMap<Show, ShowDto>();
            CreateMap<EntityListResponse<Show>, ListResponseDto<ShowDto>>();
        }
    }
}
