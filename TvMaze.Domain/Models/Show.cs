using TvMaze.Domain.Persistence;

namespace TvMaze.Domain.Models
{
    public class Show : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int ShowId { get; set; }
        public string? Url { get; set; }
        public string Name { get; set; }
        public string? OfficialSite { get; set; }
    }
}
