using FakeCameraControl.Configuration;
using GphotoCameraControl.ScriptRunning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Processes;
using Processes.ProcessWrappers;
using Api.Middlewares;

namespace Api.StartupConfigExtensions
{
    public static class AppDependenciesConfigExtensions
    {
        public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddTransient<IProcessRunner, ProcessRunnerWrapper>();
            services.ConfigureCameras(configuration);
        }

        private static void ConfigureCameras(this IServiceCollection services, IConfiguration configuration)
        {
            var fakeCameraControlConfig = configuration.GetSection("FakeCameraControl").Get<FakeCameraControlConfig>();
            if (fakeCameraControlConfig?.Enabled is true)
                services.ConfigureFakeCameraControl(fakeCameraControlConfig);
            else
                services.ConfigureGphoto();

            services.AddTransient<IScriptRunner, ScriptRunner>();
        }
    }
}
