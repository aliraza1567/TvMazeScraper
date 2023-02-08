using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Cast;

public class CastDto
{
    [JsonProperty("person")]
    public PersonDto Person { get; set; }

    [JsonProperty("character")]
    public CharacterDto Character { get; set; }
}