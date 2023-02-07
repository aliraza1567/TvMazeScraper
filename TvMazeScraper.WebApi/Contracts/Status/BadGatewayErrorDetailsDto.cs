using System.Net;

namespace TvMazeScraper.WebApi.Contracts.Status
{
    public sealed class BadGatewayErrorDetailsDto : ErrorDetailsDto
    {
        public BadGatewayErrorDetailsDto(string message)
        {
            StatusCode = (int)HttpStatusCode.BadGateway;
            Message = message;
        }
    }
}
