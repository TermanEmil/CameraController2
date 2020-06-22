using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CameraControl.Entities;
using CameraControl.Exceptions;
using MediatR;

namespace GphotoCameraControl.Commands.AutoDetect
{
    public class AutoDetectCommandHandler : IRequestHandler<AutoDetectCommand, IEnumerable<Camera>>
    {
        private const string ExpectedLineFormat = @"(?<model>.+)===(?<port>.+\S)";

        private readonly IScriptRunner scriptRunner;

        public AutoDetectCommandHandler(IScriptRunner scriptRunner)
        {
            this.scriptRunner = scriptRunner ?? throw new ArgumentNullException(nameof(scriptRunner));
        }

        public async Task<IEnumerable<Camera>> Handle(AutoDetectCommand request, CancellationToken ct = default)
        {
            return await this.HandleCore().ToListAsync(ct);
        }

        private async IAsyncEnumerable<Camera> HandleCore()
        {
            using var process = this.scriptRunner.RunAutoDetection();

            if (process is null)
                throw new Exception();

            while (true)
            {
                var line = await process.StandardOutput.ReadLineAsync();
                if (line is null)
                    break;

                var match = Regex.Match(line, ExpectedLineFormat);
                if (!match.Success)
                    throw new Exception("Unexpected command output");

                var model = match.Groups["model"].Value.Trim();
                var port = match.Groups["port"].Value.Trim();
                yield return new Camera(model, port);
            }

            var errors = await process.StandardError.ReadToEndAsync();
            if (!string.IsNullOrEmpty(errors))
                throw new CameraControlException(errors);
        }
    }
}