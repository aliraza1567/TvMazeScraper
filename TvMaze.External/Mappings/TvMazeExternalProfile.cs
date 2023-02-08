using AutoMapper;
using TvMaze.Domain.Models;
using TvMaze.External.Clients.Contracts.Cast;
using TvMaze.External.Clients.Contracts.Show;

namespace TvMaze.External.Mappings
{
    internal class TvMazeExternalProfile: Profile
    {
        public TvMazeExternalProfile()
        {
            CreateMap<ShowDto, Show>()
                .ForMember(show => show.ShowId, expression => expression.MapFrom(dto => dto.Id))
                .ForMember(show => show.Id, expression => expression.Ignore());

            CreateMap<CastDto, Cast>()
                .ForMember(cast => cast.CastId, expression => expression.MapFrom(dto => dto.Person.Id))
                .ForMember(cast => cast.Name, expression => expression.MapFrom(dto => dto.Person.Name))
                .ForMember(cast => cast.Birthday, expression => expression.MapFrom(dto => dto.Person.Birthday))
                .ForMember(cast => cast.CharacterName, expression => expression.MapFrom(dto => dto.Character.Name))
                .ForMember(cast => cast.Id, expression => expression.Ignore());
        }
    }
}
