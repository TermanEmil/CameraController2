using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CameraControl;
using CameraControl.Infrastructure;

namespace FakeCameraControl
{
    public class FakeCameraManager : ICameraManager
    {
        private readonly IReadOnlyCollection<FakeCamera> cameras;

        public FakeCameraManager(IEnumerable<FakeCamera> cameras)
        {
            this.cameras = cameras?.ToList() ?? throw new ArgumentNullException(nameof(cameras));
        }

        public Task<IEnumerable<Camera>> AutoDetectCameras(CancellationToken ct = default)
        {
            return Task.FromResult(this.cameras.Select(x => x as Camera));
        }
    }
}
