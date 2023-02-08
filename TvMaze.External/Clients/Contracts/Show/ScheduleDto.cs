using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show;

public class ScheduleDto
{
    [JsonProperty("time")]
    public string Time { get; set; }

    [JsonProperty("days")]
    public string[] Days { get; set; }
}