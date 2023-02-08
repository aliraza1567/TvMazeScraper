using System.Net;

namespace TvMazeScraper.WebApi.Contracts.Status
{
    public sealed class InternalServerErrorDetailsDto : ErrorDetailsDto
    {
        public InternalServerErrorDetailsDto(string message)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
            Message = message;
        }
    }
}
