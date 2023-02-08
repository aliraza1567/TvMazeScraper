using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TvMazeScraper.WebApi.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMapper _mapper;
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

    }
}
