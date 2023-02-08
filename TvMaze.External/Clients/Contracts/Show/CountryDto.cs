using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show;

public class CountryDto
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("timezone")]
    public string Timezone { get; set; }
}