using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Cast;

public class CharacterDto
{
    [JsonProperty("name")]
    public string Name { get; set; }
}