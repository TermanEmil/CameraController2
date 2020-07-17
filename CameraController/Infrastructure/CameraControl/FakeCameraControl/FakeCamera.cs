using System;
using CameraControl;

namespace FakeCameraControl
{
    public class FakeCamera : Camera
    {
        public FakeCamera(string model, string port)
        {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.Port = port ?? throw new ArgumentNullException(nameof(port));
        }

        public override string Model { get; }
        public override string Port { get; }
    }
}
