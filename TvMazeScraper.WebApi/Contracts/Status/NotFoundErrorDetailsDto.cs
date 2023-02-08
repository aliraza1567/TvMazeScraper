using System.Net;

namespace TvMazeScraper.WebApi.Contracts.Status
{
    public sealed class NotFoundErrorDetailsDto : ErrorDetailsDto
    {
        public NotFoundErrorDetailsDto(string message)
        {
            StatusCode = (int)HttpStatusCode.NotFound;
            Message = message;
        }
    }
}
