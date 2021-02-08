using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CameraControl;
using CameraControl.Exceptions;
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

        public async Task<Camera> FindCamera(string port, CancellationToken ct)
        {
            var cameras = await this.mediator.Send(new AutoDetectGpCamerasCommand(), ct);
            var camera = cameras.FirstOrDefault(x =>
                x.Port.Equals(port, StringComparison.InvariantCultureIgnoreCase));

            if (camera is null)
                throw new CameraNotFoundException(port);

            return camera;
        }
    }
}
