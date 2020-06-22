using System;

namespace CameraControl.Entities
{
    public class Camera
    {
        public Camera(string model, string port)
        {
            if (string.IsNullOrEmpty(model))
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrEmpty(port))
                throw new ArgumentNullException(nameof(port));

            this.Model = model;
            this.Port = port;
        }

        public string Model { get; }
        public string Port { get; }
    }
}
