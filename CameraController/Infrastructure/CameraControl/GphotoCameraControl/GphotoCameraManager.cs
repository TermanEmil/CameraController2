using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CameraControl;
using GphotoCameraControl.Commands.AutoDetectGpCameras;
using MediatR;

namespace GphotoCameraControl
{
    public class GphotoCameraManager : ICameraManager
    {
        private readonly IMediator mediator;

        public GphotoCameraManager(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IEnumerable<Camera>> AutoDetectCameras(CancellationToken ct = default)
        {
            return await this.mediator.Send(new AutoDetectGpCamerasCommand(), ct);
        }
    }
}
