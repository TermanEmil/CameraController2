using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebUi.Controllers
{
    public abstract class BaseController : Controller
    {
        private IMediator mediator;
        private IMapper mapper;

        protected IMediator Mediator => this.mediator ??= this.HttpContext.RequestServices.GetRequiredService<IMediator>();
        protected IMapper Mapper => this.mapper ??= this.HttpContext.RequestServices.GetRequiredService<IMapper>();
    }
}
