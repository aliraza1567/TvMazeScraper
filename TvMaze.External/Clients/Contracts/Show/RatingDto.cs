using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show;

public class RatingDto
{
    [JsonProperty("average")]
    public double Average { get; set; }
}