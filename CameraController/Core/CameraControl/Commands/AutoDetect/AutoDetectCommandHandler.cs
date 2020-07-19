using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CameraControl.Commands.AutoDetect
{
    public class AutoDetectCommandHandler : IRequestHandler<AutoDetectCommand, IEnumerable<Camera>>
    {
        private readonly ICameraManager cameraManager;

        public AutoDetectCommandHandler(ICameraManager cameraManager)
        {
            this.cameraManager = cameraManager ?? throw new ArgumentNullException(nameof(cameraManager));
        }

        public Task<IEnumerable<Camera>> Handle(AutoDetectCommand request, CancellationToken ct)
        {
            return this.cameraManager.AutoDetectCameras(ct);
        }
    }
}