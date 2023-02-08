namespace TvMaze.Infrastructure.Abstractions.TvMaze.Exceptions
{
    public class TvMazeExceptions: Exception
    {
        public TvMazeExceptions()
        {
        }

        public TvMazeExceptions(string message) : base(message)
        {
        }

        public TvMazeExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TvMazeExceptions(Exception innerException) : base(null, innerException)
        {
        }
    }
}
