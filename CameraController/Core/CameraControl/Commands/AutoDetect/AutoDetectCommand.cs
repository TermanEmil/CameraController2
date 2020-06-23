using System.Collections.Generic;
using MediatR;

namespace CameraControl.Commands.AutoDetect
{
    public class AutoDetectCommand : IRequest<IEnumerable<Camera>>
    {
    }
}
