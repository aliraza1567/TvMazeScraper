using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show;

public class ExternalsDto
{
    [JsonProperty("tvrage")]
    public long TvRage { get; set; }

    [JsonProperty("thetvdb")]
    public long TheTvdb { get; set; }

    [JsonProperty("imdb")]
    public string Imdb { get; set; }
}