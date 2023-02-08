namespace TvMaze.Infrastructure.Abstractions.TvMaze.Clients
{
    public interface ITvMazeClientFactory
    {
        ITvMazeClient Create(string url);
    }
}
