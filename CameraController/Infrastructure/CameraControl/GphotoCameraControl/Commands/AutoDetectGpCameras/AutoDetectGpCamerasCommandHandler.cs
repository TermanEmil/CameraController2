using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using GphotoCameraControl.Exceptions;
using GphotoCameraControl.ScriptRunning;
using MediatR;

namespace GphotoCameraControl.Commands.AutoDetectGpCameras
{
    public class AutoDetectGpCamerasCommandHandler : IRequestHandler<AutoDetectGpCamerasCommand, IEnumerable<GpCamera>>
    {
        private const string ExpectedLineFormat = @"(?<model>.+)===(?<port>.+\S)";
        private readonly IMediator mediator;
        private readonly IScriptRunner scriptRunner;

        public AutoDetectGpCamerasCommandHandler(IMediator mediator, IScriptRunner scriptRunner)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.scriptRunner = scriptRunner ?? throw new ArgumentNullException(nameof(scriptRunner));
        }

        public async Task<IEnumerable<GpCamera>> Handle(AutoDetectGpCamerasCommand request, CancellationToken ct = default)
        {
            return await this.HandleCore().ToListAsync(ct);
        }

        private async IAsyncEnumerable<GpCamera> HandleCore()
        {
            string line;

            using var process = this.scriptRunner.RunAutoDetection();

            while ((line = await process.StandardOutput.ReadLineAsync()) != null)
            {
                var match = Regex.Match(line, ExpectedLineFormat);
                if (!match.Success)
                    throw new UnexpectedProcessOutputException(line);

                var model = match.Groups["model"].Value.Trim();
                var port = match.Groups["port"].Value.Trim();
                yield return new GpCamera(mediator, model, port);
            }

            var errors = await process.StandardError.ReadToEndAsync();
            if (!string.IsNullOrEmpty(errors))
                throw new GphotoException(errors);
        }
    }
}