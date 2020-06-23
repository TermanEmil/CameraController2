using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CameraControl.Infrastructure
{
    public interface ICameraManager
    {
        Task<IEnumerable<Camera>> AutoDetectCameras(CancellationToken ct = default);
    }
}
