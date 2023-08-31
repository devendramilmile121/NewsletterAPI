using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Newsletter.API.Controllers
{
    /// <summary>
    /// Base API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator = null!;
        /// <summary>
        /// Mediator Config
        /// </summary>
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
