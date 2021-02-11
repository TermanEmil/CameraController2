using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GphotoCameraControl.Exceptions;
using Processes;

namespace GphotoCameraControl.ScriptRunning
{
    public class ScriptRunner : IScriptRunner
    {
        private static string BasePath => $"{Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)}/Scripts";

        private readonly IProcessRunner processRunner;

        public ScriptRunner(IProcessRunner processRunner)
        {
            this.processRunner = processRunner ?? throw new ArgumentNullException(nameof(processRunner));
        }

        public IProcess RunAutoDetection()
        {
            return this.RunScript("auto-detect.sh");
        }

        public IProcess RunCaptureImage(string filename, string port)
        {
            return this.RunScript("capture-image-and-download.sh", port, filename);
        }

        public IProcess RunCapturePreview(string port)
        {
            return this.RunScript("capture-preview.sh", port);
        }

        private IProcess RunScript(string script, params string[] scriptParameters)
        {
            var parameters = new List<string>();
            parameters.Add($"{BasePath}/{script}");
            parameters.AddRange(scriptParameters);

            var process = this.processRunner.Start("sh", parameters.ToArray());
            if (process is null)
                throw new GphotoException($"Failed to start {nameof(this.RunCaptureImage)}");

            return process;
        }
    }
}
