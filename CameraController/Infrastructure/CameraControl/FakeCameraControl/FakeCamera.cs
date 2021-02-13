using System;
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
        private const int ImageWidth = 255;
        private const int ImageHeight = 255;
        private const int FigurePoints = 5;

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

            var command = new DrawRandomFigureCommand(width: ImageWidth, height: ImageHeight, points: FigurePoints);
            using var image = await this.mediator.Send(command);

            var fullPath = $"{path}/{filename}.png";
            image.Save(fullPath, ImageFormat.Png);

            return fullPath;
        }

        public override async Task<byte[]> CapturePreview(CancellationToken ct)
        {
            var command = new DrawRandomFigureCommand(width: ImageWidth, height: ImageHeight, points: FigurePoints);
            using var image = await this.mediator.Send(command);

            await using var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
