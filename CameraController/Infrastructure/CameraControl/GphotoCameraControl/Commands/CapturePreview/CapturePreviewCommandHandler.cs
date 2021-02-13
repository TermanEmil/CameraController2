using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GphotoCameraControl.Exceptions;
using GphotoCameraControl.ScriptRunning;
using MediatR;
using StreamUtils;

namespace GphotoCameraControl.Commands.CapturePreview
{
    public class CapturePreviewCommandHandler : IRequestHandler<CapturePreviewCommand, byte[]>
    {
        private readonly IScriptRunner scriptRunner;

        public CapturePreviewCommandHandler(IScriptRunner scriptRunner)
        {
            this.scriptRunner = scriptRunner ?? throw new System.ArgumentNullException(nameof(scriptRunner));
        }

        public async Task<byte[]> Handle(CapturePreviewCommand request, CancellationToken ct)
        {
            using var process = this.scriptRunner.RunCapturePreview(request.Port);
            var bytes = await process.StandardOutput.BaseStream.ReadToEndAsync(ct);

            var errors = await process.StandardError.ReadToEndAsync();
            if (!string.IsNullOrEmpty(errors))
                throw new GphotoException(errors);

            return bytes.ToArray();
        }
    }
}
