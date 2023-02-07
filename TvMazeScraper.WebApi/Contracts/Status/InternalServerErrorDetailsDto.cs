using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
