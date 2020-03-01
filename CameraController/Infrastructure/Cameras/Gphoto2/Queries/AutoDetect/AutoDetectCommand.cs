using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Cameras.Infrastructure;
using MediatR;

namespace Cameras.Gphoto2.Queries.AutoDetect
{
    public class AutoDetectCommand : IRequest<IEnumerable<Camera>>
    {
    }

    public class AutoDetectCommandHandler : IRequestHandler<AutoDetectCommand, IEnumerable<Camera>>
    {
        private readonly string scriptPath = "auto-detect.sh";

        public Task<IEnumerable<Camera>> Handle(AutoDetectCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
