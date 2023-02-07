using System.Runtime.Serialization;
using TvMaze.Persistence.Abstractions.Constants;

namespace TvMaze.Persistence.Abstractions.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException() : base(StringConstants.ErrorRepositoryMessage)
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
