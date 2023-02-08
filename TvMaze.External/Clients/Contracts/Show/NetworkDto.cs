using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show;

public class NetworkDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("country")]
    public CountryDto Country { get; set; }

    [JsonProperty("officialSite")]
    public Uri OfficialSite { get; set; }
}