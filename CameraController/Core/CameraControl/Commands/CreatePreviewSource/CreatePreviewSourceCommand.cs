using System.Collections.Generic;
using MediatR;

namespace CameraControl.Commands.CreatePreviewSource
{
    public class CreatePreviewSourceCommand : IRequest<IAsyncEnumerable<byte[]>>
    {
        public CreatePreviewSourceCommand(string port)
        {
            this.Port = port ?? throw new System.ArgumentNullException(nameof(port));
        }

        public string Port { get; }
    }
}
