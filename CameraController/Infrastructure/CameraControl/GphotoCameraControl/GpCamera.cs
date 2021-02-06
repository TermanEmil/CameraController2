using CameraControl;
using System;
using System.Threading.Tasks;

namespace GphotoCameraControl
{
    public class GpCamera : Camera
    {
        public GpCamera(string model, string port)
        {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.Port = port ?? throw new ArgumentNullException(nameof(port));
        }

        public override string Model { get; }
        public override string Port { get; }

        public override Task<string> CaptureImage(string path, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
