using CameraControl;
using GphotoCameraControl;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace WebUi.StartupConfigExtensions
{
    public static class MediatorConfigExtensions
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(Startup),
                typeof(Camera),
                typeof(GpCamera));
        }
    }
}
