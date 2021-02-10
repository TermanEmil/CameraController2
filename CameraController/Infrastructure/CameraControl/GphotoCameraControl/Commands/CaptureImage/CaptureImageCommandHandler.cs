using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GphotoCameraControl.Exceptions;
using GphotoCameraControl.ScriptRunning;
using MediatR;

namespace GphotoCameraControl.Commands.CaptureImage
{
    public class CaptureImageCommandHandler : IRequestHandler<CaptureImageCommand, string>
    {
        private readonly IScriptRunner scriptRunner;

        public CaptureImageCommandHandler(IScriptRunner scriptRunner)
        {
            this.scriptRunner = scriptRunner ?? throw new ArgumentNullException(nameof(scriptRunner));
        }

        public async Task<string> Handle(CaptureImageCommand request, CancellationToken ct)
        {
            using var process = this.scriptRunner.RunCaptureImage(request.Filename, request.Port);
            
            var output = await process.StandardOutput.ReadToEndAsync();
            var errors = await process.StandardError.ReadToEndAsync();

            if (!string.IsNullOrEmpty(errors))
                throw new GphotoException(errors);


            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length != 1)
                throw new UnexpectedProcessOutputException(output);

            return lines.First();
        }
    }
}
