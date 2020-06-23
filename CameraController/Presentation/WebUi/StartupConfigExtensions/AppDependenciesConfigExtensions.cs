using CameraControl.Infrastructure;
using GphotoCameraControl;
using GphotoCameraControl.ScriptRunning;
using Microsoft.Extensions.DependencyInjection;
using Processes;
using Processes.ProcessWrappers;

namespace WebUi.StartupConfigExtensions
{
    public static class AppDependenciesConfigExtensions
    {
        public static void ConfigureAppDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProcessRunner, ProcessRunnerWrapper>();
            services.ConfigureGphoto();
        }

        private static void ConfigureGphoto(this IServiceCollection services)
        {
            services.AddTransient<IScriptRunner, ScriptRunner>();
            services.AddTransient<ICameraManager, GphotoCameraManager>();
        }
    }
}
