using MediatR;

namespace CameraControl.Commands.CaptureImageAndDownload
{
    public class CaptureImageAndDownloadCommand : IRequest<string>
    {
        public CaptureImageAndDownloadCommand(string port)
        {
            this.Port = port ?? throw new System.ArgumentNullException(nameof(port));
        }

        public string Port { get; }
    }
}
