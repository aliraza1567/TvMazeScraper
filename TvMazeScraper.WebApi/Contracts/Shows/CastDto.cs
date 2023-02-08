namespace TvMazeScraper.WebApi.Contracts.Shows
{
    public class CastDto
    {
        public Guid Id { get; set; }
        public long CastId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public string CharacterName { get; set; }
    }
}
