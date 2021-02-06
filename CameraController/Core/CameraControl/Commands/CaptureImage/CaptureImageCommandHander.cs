using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CameraControl.Commands.CaptureImage
{
    public class CaptureImageCommandHander : IRequestHandler<CaptureImageCommand>
    {
        private readonly ICameraManager cameraManager;

        public CaptureImageCommandHander(ICameraManager cameraManager)
        {
            this.cameraManager = cameraManager;
        }

        public async Task<Unit> Handle(CaptureImageCommand request, CancellationToken ct)
        {
            var camera = await this.cameraManager.FindCamera(request.Port);
            await camera.CaptureImage(request.Path, request.Filename);

            return Unit.Value;
        }
    }
}
