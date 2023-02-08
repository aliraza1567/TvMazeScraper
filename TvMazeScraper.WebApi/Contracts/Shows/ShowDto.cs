namespace TvMazeScraper.WebApi.Contracts.Shows
{
    public class ShowDto
    {
        public Guid Id { get; set; }
        public int TvMazeId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string OfficialSite { get; set; }
    }
}