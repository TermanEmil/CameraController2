using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CameraControl;
using CameraControl.Exceptions;
using MediatR;

namespace FakeCameraControl
{
    public class FakeCameraManager : ICameraManager
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IReadOnlyCollection<FakeCameraDetails> camerasDetails;

        public FakeCameraManager(IServiceProvider serviceProvider, IEnumerable<FakeCameraDetails> cameras)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.camerasDetails = cameras?.ToList() ?? throw new ArgumentNullException(nameof(cameras));
        }

        private IEnumerable<FakeCamera> Cameras => this.camerasDetails
            .Select(camera =>
                new FakeCamera(
                    this.serviceProvider.GetService(typeof(IMediator)) as IMediator,
                    camera.Model,
                    camera.Port));


        public async Task<IEnumerable<Camera>> AutoDetectCameras(CancellationToken ct = default)
        {
            // Fake long process
            await Task.Delay(2000);

            return this.Cameras;
        }

        public Task<Camera> FindCamera(string port, CancellationToken ct = default)
        {
            var camera = this.Cameras
                .Where(x => x.Port.Equals(port, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            if (camera is null)
                throw new CameraNotFoundException(port);

            return Task.FromResult<Camera>(camera);
        }
    }
}
