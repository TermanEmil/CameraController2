using System.Collections.Generic;
using MediatR;

namespace GphotoCameraControl.Commands.AutoDetectGpCameras
{
    public class AutoDetectGpCamerasCommand : IRequest<IEnumerable<GpCamera>>
    {
    }
}
