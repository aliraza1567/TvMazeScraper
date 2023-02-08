using TvMaze.Domain.Persistence;

namespace TvMaze.Domain.Models
{
    public class Show : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int TvMazeId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public int Runtime { get; set; }
        public int AverageRuntime { get; set; }
        public string Premiered { get; set; }
        public string Ended { get; set; }
        public string OfficialSite { get; set; }
    }
}
