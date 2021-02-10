using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CameraControl.Commands.CaptureImageAndDownload
{
    public class CaptureImageAndDownloadCommandHandler : IRequestHandler<CaptureImageAndDownloadCommand, string>
    {
        private readonly ICameraManager cameraManager;

        public CaptureImageAndDownloadCommandHandler(ICameraManager cameraManager)
        {
            this.cameraManager = cameraManager ?? throw new ArgumentNullException(nameof(cameraManager));
        }

        public async Task<string> Handle(CaptureImageAndDownloadCommand request, CancellationToken ct)
        {
            var camera = await this.cameraManager.FindCamera(request.Port, ct);
            var imagePath = await camera.CaptureImage(Path.GetTempPath(), Guid.NewGuid().ToString());
            return imagePath;
        }
    }
}
