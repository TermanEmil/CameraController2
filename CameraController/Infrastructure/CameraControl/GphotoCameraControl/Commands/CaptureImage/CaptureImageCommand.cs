using MediatR;

namespace GphotoCameraControl.Commands.CaptureImage
{
    public class CaptureImageCommand : IRequest<string>
    {
        public CaptureImageCommand(string filename, string port)
        {
            this.Filename = filename;
            this.Port = port;
        }

        /// <summary>
        /// Full path including file's name.
        /// The extension is be added after the image is captured.
        /// </summary>
        public string Filename { get; }

        public string Port { get; }
    }
}
