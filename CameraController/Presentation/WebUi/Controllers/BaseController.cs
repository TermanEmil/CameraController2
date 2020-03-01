using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebUi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator =>
            (this.mediator) ??
            (this.mediator = this.HttpContext.RequestServices.GetService<IMediator>());
    }
}
