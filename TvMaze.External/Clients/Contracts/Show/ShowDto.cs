using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show
{
    public class ShowDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("genres")]
        public string[] Genres { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("runtime")]
        public long Runtime { get; set; }

        [JsonProperty("averageRuntime")]
        public long AverageRuntime { get; set; }

        [JsonProperty("premiered")]
        public DateTimeOffset Premiered { get; set; }

        [JsonProperty("ended")]
        public DateTimeOffset Ended { get; set; }

        [JsonProperty("officialSite")]
        public Uri OfficialSite { get; set; }

        [JsonProperty("schedule")]
        public ScheduleDto Schedule { get; set; }

        [JsonProperty("rating")]
        public RatingDto Rating { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("network")]
        public NetworkDto Network { get; set; }

        [JsonProperty("webChannel")]
        public object WebChannel { get; set; }

        [JsonProperty("dvdCountry")]
        public object DvdCountry { get; set; }

        [JsonProperty("externals")]
        public ExternalsDto Externals { get; set; }

        [JsonProperty("image")]
        public ImageDto Image { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("_links")]
        public LinksDto Links { get; set; }
    }
}
