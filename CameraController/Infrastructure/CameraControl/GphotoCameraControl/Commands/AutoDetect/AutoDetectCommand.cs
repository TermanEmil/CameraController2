using System.Collections.Generic;
using CameraControl.Entities;
using MediatR;

namespace GphotoCameraControl.Commands.AutoDetect
{
    public class AutoDetectCommand : IRequest<IEnumerable<Camera>>
    {
    }
}
