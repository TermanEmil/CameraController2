using System.Threading;
using System.Threading.Tasks;

namespace CameraControl
{
    public abstract class Camera
    {
        public abstract string Model { get; }
        
        public abstract string Port { get; }

        // TODO: Where cancellation token?
        public abstract Task<string> CaptureImage(string path, string filename);

        public abstract Task<byte[]> CapturePreview(CancellationToken ct);
    }
}
