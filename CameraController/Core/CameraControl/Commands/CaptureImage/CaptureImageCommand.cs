using MediatR;
using System;

namespace CameraControl.Commands.CaptureImage
{
    public class CaptureImageCommand : IRequest
    {
        public CaptureImageCommand(string port, string path, string filename)
        {
            this.Port = port ?? throw new ArgumentNullException(nameof(port));
            this.Path = path ?? throw new ArgumentNullException(nameof(path));
            this.Filename = filename ?? throw new ArgumentNullException(nameof(filename));
        }

        public string Port { get; }
        public string Path { get; }
        public string Filename { get; }
    }
}
