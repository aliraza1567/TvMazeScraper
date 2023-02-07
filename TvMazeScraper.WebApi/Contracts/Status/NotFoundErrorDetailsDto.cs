using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
