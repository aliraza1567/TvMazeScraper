using TvMazeScraper.WebApi.Queries;

namespace TvMazeScraper.WebApi.Contracts.Shows
{
    public class ShowDto: IResource<int>
    {
        public int ShowId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string OfficialSite { get; set; }
        public List<CastDto> Casts { get; set; }
    }
}