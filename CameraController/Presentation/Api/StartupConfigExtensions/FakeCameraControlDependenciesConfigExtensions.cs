using System.Linq;
using CameraControl;
using FakeCameraControl;
using FakeCameraControl.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupConfigExtensions
{
    public static class FakeCameraControlDependenciesConfigExtensions
    {
        public static void ConfigureFakeCameraControl(this IServiceCollection services, FakeCameraControlConfig config)
        {
            services.AddSingleton<ICameraManager>(sp =>
            {
                var mediator = sp.GetRequiredService<IMediator>();

                var cameras = config.Cameras.Select(x => new FakeCamera(mediator, x.Model, x.Port));
                return new FakeCameraManager(cameras);
            });
        }
    }
}
