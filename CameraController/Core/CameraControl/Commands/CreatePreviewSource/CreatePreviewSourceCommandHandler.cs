using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CameraControl.Commands.CreatePreviewSource
{
    public class CreatePreviewSourceCommandHandler
        : IRequestHandler<CreatePreviewSourceCommand, IAsyncEnumerable<byte[]>>
    {
        private readonly ICameraManager cameraManager;

        public CreatePreviewSourceCommandHandler(ICameraManager cameraManager)
        {
            this.cameraManager = cameraManager ?? throw new System.ArgumentNullException(nameof(cameraManager));
        }

        public async Task<IAsyncEnumerable<byte[]>> Handle(CreatePreviewSourceCommand request, CancellationToken ct)
        {
            var camera = await this.cameraManager.FindCamera(request.Port, ct);
            return this.Source(camera, ct);
        }

        private async IAsyncEnumerable<byte[]> Source(
            Camera camera,
            CancellationToken ct,
            [EnumeratorCancellation] CancellationToken enumeratorCt = default)
        {
            while (!ct.IsCancellationRequested && !enumeratorCt.IsCancellationRequested)
                yield return await camera.CapturePreview(ct);
        }
    }
}
