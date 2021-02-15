using CameraControl;
using FakeCameraControl;
using GphotoCameraControl;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Validations;

namespace Api.StartupConfigExtensions
{
    public static class MediatorConfigExtensions
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(Startup),
                typeof(Camera),
                typeof(GpCamera),
                typeof(FakeCamera));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }
    }
}
