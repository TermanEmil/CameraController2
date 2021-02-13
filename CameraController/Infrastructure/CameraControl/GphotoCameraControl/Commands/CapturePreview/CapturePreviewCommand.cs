using MediatR;

namespace GphotoCameraControl.Commands.CapturePreview
{
    public class CapturePreviewCommand : IRequest<byte[]>
    {
        public CapturePreviewCommand(string port)
        {
            this.Port = port ?? throw new System.ArgumentNullException(nameof(port));
        }

        public string Port { get; }
    }
}
