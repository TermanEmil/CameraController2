using CameraControl;
using FakeCameraControl;
using FakeCameraControl.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupConfigExtensions
{
    public static class FakeCameraControlDependenciesConfigExtensions
    {
        public static void ConfigureFakeCameraControl(this IServiceCollection services, FakeCameraControlConfig config)
        {
            services.AddTransient<ICameraManager, FakeCameraManager>();
            config.Cameras
                .ForEach(config =>
                    services.AddTransient(sp => new FakeCameraDetails(config.Model, config.Port)));
        }
    }
}
