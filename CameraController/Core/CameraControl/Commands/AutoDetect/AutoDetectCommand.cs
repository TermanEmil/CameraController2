using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CameraControl.Commands.AutoDetect
{
    public class AutoDetectCommand : IRequest<Unit>
    {
    }

    public class AutoDetectCommandHandler : IRequestHandler<AutoDetectCommand, Unit>
    {
        public Task<Unit> Handle(AutoDetectCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
