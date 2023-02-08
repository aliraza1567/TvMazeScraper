using AutoMapper;
using TvMazeScraper.WebApi.Contracts.Shows;

namespace TvMazeScraper.WebApi.Mappings
{
    internal class ShowMappings: Profile
    {
        public ShowMappings()
        {
            CreateMap<ShowDto, TvMaze.Domain.Models.Show>();
        }
    }
}
