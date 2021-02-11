using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CameraControl;
using FakeCameraControl.Commands.DrawRandomFigure;
using MediatR;

namespace FakeCameraControl
{
    public class FakeCamera : Camera
    {
        private readonly IMediator mediator;

        public FakeCamera(IMediator mediator, string model, string port)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.Port = port ?? throw new ArgumentNullException(nameof(port));
        }

        public override string Model { get; }
        public override string Port { get; }

        public override async Task<string> CaptureImage(string path, string filename)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"{path} not found");

            using var image = await this.mediator.Send(
                new DrawRandomFigureCommand(width: 255, height: 255, points: 5));

            var fullPath = $"{path}/{filename}.png";
            image.Save(fullPath, ImageFormat.Png);

            return fullPath;
        }

        public override Task<IEnumerable<byte>> CapturePreview(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
