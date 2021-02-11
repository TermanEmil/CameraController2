using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CameraControl
{
    public abstract class Camera
    {
        public abstract string Model { get; }
        
        public abstract string Port { get; }

        public abstract Task<string> CaptureImage(string path, string filename);

        public abstract Task<IEnumerable<byte>> CapturePreview(CancellationToken ct);
    }
}
