using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Cast;

public class PersonDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("birthday")]
    public DateTimeOffset? Birthday { get; set; }

}