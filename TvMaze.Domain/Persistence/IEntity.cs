namespace TvMaze.Domain.Persistence
{
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }

    public interface IEntity
    {

    }
}
