using CameraControl;
using GphotoCameraControl;
using GphotoCameraControl.ScriptRunning;
using Microsoft.Extensions.DependencyInjection;
using Processes;

namespace Api.StartupConfigExtensions
{
    public static class GphotoDependenciesConfigExtensions
    {
        public static void ConfigureGphoto(this IServiceCollection services)
        {
            services.Decorate<IProcessRunner, ExceptionMiddlewareProcessRunner>();
            services.AddTransient<ICameraManager, GphotoCameraManager>();
        }
    }
}
