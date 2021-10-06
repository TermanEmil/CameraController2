using System;

namespace FakeCameraControl
{
    public class FakeCameraDetails
    {
        public FakeCameraDetails(string model, string port)
        {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.Port = port ?? throw new ArgumentNullException(nameof(port));
        }

        public string Model { get; }
        public string Port { get; }
    }
}
