using TvMaze.Infrastructure.Abstractions.TvMaze.Exceptions;

namespace TvMaze.External.Exceptions
{
    public class TvMazeApiException: TvMazeExceptions
    {
        public TvMazeApiException()
        {
        }

        public TvMazeApiException(HttpResponseMessage response)
        {
            Response = response;
        }

        public TvMazeApiException(string message) : base(message)
        {
        }

        public TvMazeApiException(Exception innerException) : base(innerException)
        {
        }

        public TvMazeApiException(string message, HttpResponseMessage response) : base(message)
        {
            Response = response;
        }

        public TvMazeApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public HttpResponseMessage Response { get; }
    }
}
