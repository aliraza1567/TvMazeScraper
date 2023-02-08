using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show;

public class PreviousEpisodeDto
{
    [JsonProperty("href")]
    public Uri Href { get; set; }
}