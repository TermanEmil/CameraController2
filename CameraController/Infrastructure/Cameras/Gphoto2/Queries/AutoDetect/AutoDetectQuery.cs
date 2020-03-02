using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Cameras.Infrastructure;
using MediatR;

namespace Cameras.Gphoto2.Queries.AutoDetect
{
    public class AutoDetectQuery : IRequest<IAsyncEnumerable<Camera>>
    {
    }

    public class AutoDetectQueryHandler : IRequestHandler<AutoDetectQuery, IAsyncEnumerable<Camera>>
    {
        private const string ExpectedLineFormat = @"(?<model>.+)<\|>(?<port>.+\S)";
        
        private readonly IScriptProvider scriptProvider;

        public AutoDetectQueryHandler(IScriptProvider scriptProvider)
        {
            this.scriptProvider = scriptProvider;
        }

        public Task<IAsyncEnumerable<Camera>> Handle(AutoDetectQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.HandleCore());
        }

        private async IAsyncEnumerable<Camera> HandleCore()
        {
            var processStartInfo = new ProcessStartInfo(this.scriptProvider.GetScriptForAutoDetect());
            using var process = Process.Start(processStartInfo);

            if (process is null)
                throw new Exception();

            while (true)
            {
                var line = await process.StandardOutput.ReadLineAsync();
                if (line == null)
                    break;

                var match = Regex.Match(line, ExpectedLineFormat);
                if (!match.Success)
                    throw new Exception();

                var model = match.Groups["model"].Value;
                var port = match.Groups["port"].Value;
                yield return new Camera(model, port);
            }

            var errors = await process.StandardError.ReadToEndAsync();
            if (errors.Length > 0)
                throw new Exception(errors);
        }
    }
}
