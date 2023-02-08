using System.Net.Sockets;
using TvMaze.Domain.Persistence;

namespace TvMaze.Domain.Models
{
    public class Cast : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public long CastId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public string CharacterName { get; set; }
        public Show Show { get; set; }
    }
}
