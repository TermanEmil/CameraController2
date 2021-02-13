using System;
using System.Threading;
using System.Threading.Tasks;
using CameraControl;
using GphotoCameraControl.Commands.CaptureImage;
using GphotoCameraControl.Commands.CapturePreview;
using MediatR;

namespace GphotoCameraControl
{
    public class GpCamera : Camera
    {
        private readonly IMediator mediator;

        public GpCamera(IMediator mediator, string model, string port)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.Port = port ?? throw new ArgumentNullException(nameof(port));
        }

        public override string Model { get; }
        public override string Port { get; }

        public override async Task<string> CaptureImage(string path, string filename)
        {
            return await this.mediator.Send(new CaptureImageCommand($"{path}/{filename}", this.Port));
        }

        public override async Task<byte[]> CapturePreview(CancellationToken ct)
        {
            return await this.mediator.Send(new CapturePreviewCommand(this.Port), ct);
        }
    }
}
