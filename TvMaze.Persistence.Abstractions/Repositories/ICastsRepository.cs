using TvMaze.Domain.Models;

namespace TvMaze.Persistence.Abstractions.Repositories;

public interface ICastsRepository : IEntityRepository<Cast, Guid>
{

}