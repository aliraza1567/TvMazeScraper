using Newtonsoft.Json;

namespace TvMaze.External.Clients.Contracts.Show
{
    public class ShowDto
    {
        [JsonProperty("id")] 
        public long Id { get; set; }

        [JsonProperty("url")] 
        public string Url { get; set; }

        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("officialSite")] 
        public string OfficialSite { get; set; }
    }
}

