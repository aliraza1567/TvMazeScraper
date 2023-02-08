using AutoMapper;
using TvMaze.Domain.Models;
using TvMazeScraper.WebApi.Contracts.Shows;

namespace TvMazeScraper.WebApi.Mappings
{
    internal class ShowMappings: Profile
    {
        public ShowMappings()
        {
            CreateMap<Show, ShowDto>();
        }
    }
}
