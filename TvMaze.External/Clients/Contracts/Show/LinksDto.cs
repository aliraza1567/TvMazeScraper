using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show;

public class LinksDto
{
    [JsonProperty("self")]
    public PreviousEpisodeDto Self { get; set; }

    [JsonProperty("previousepisode")]
    public PreviousEpisodeDto PreviousEpisode { get; set; }
}