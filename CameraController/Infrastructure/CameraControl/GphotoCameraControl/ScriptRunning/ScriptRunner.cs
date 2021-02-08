using System;
using System.IO;
using System.Reflection;
using GphotoCameraControl.Exceptions;
using Processes;

namespace GphotoCameraControl.ScriptRunning
{
    public class ScriptRunner : IScriptRunner
    {
        private static string BasePath => Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

        private readonly IProcessRunner processRunner;

        public ScriptRunner(IProcessRunner processRunner)
        {
            this.processRunner = processRunner ?? throw new ArgumentNullException(nameof(processRunner));
        }

        public IProcess RunAutoDetection()
        {
            var proc = this.processRunner.Start("bash", $"{BasePath}/Scripts/auto-detect.sh");
            if (proc is null)
                throw new GphotoException($"Failed to start {nameof(this.RunAutoDetection)}");

            return proc;
        }

        public IProcess RunCaptureImage(string filename, string port)
        {
            var script = $"{BasePath}/Scripts/capture-image-and-download.sh";
            var proc = this.processRunner.Start("bash", script, filename, port);

            if (proc is null)
                throw new GphotoException($"Failed to start {nameof(this.RunCaptureImage)}");

            return proc;
        }
    }
}
