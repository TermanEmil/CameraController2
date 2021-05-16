using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CameraControl;
using CameraControl.Exceptions;

namespace FakeCameraControl
{
    public class FakeCameraManager : ICameraManager
    {
        private readonly IReadOnlyCollection<FakeCamera> cameras;

        public FakeCameraManager(IEnumerable<FakeCamera> cameras)
        {
            this.cameras = cameras?.ToList() ?? throw new ArgumentNullException(nameof(cameras));
        }

        public async Task<IEnumerable<Camera>> AutoDetectCameras(CancellationToken ct = default)
        {
            // Fake long process
            await Task.Delay(2000);

            return this.cameras;
        }

        public Task<Camera> FindCamera(string port, CancellationToken ct = default)
        {
            var camera = this.cameras
                .Where(x => x.Port.Equals(port, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            if (camera is null)
                throw new CameraNotFoundException(port);

            return Task.FromResult<Camera>(camera);
        }
    }
}
