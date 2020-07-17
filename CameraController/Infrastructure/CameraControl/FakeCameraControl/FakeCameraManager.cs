using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CameraControl;
using CameraControl.Infrastructure;

namespace FakeCameraControl
{
    public class FakeCameraManager : ICameraManager
    {
        public FakeCameraManager(IEnumerable<FakeCamera> cameras)
        {
        }

        public Task<IEnumerable<Camera>> AutoDetectCameras(CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
