using System.Linq;
using CameraControl.Infrastructure;
using FakeCameraControl;
using FakeCameraControl.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebUi.StartupConfigExtensions
{
    public static class FakeCameraControlDependenciesConfigExtensions
    {
        public static void ConfigureFakeCameraControl(this IServiceCollection services, FakeCameraControlConfig config)
        {
            var cameras = config.Cameras.Select(x => new FakeCamera(x.Model, x.Port));
            var cameraManager = new FakeCameraManager(cameras);

            services.AddSingleton<ICameraManager>(cameraManager);
        }
    }
}
