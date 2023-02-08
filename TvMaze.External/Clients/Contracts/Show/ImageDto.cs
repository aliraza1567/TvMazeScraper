using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show;

public class ImageDto
{
    [JsonProperty("medium")]
    public Uri Medium { get; set; }

    [JsonProperty("original")]
    public Uri Original { get; set; }
}