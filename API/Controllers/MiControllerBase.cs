using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiControllerBase : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator mediator => _mediator ?? ( _mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}