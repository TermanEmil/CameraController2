using System;
using GphotoCameraControl.Exceptions;
using Processes;

namespace GphotoCameraControl.ScriptRunning
{
    public class ScriptRunner : IScriptRunner
    {
        private readonly IProcessRunner processRunner;

        public ScriptRunner(IProcessRunner processRunner)
        {
            this.processRunner = processRunner ?? throw new ArgumentNullException(nameof(processRunner));
        }

        public IProcess RunAutoDetection()
        {
            var proc = this.processRunner.Start("auto-detect.sh");
            if (proc is null)
                throw new GphotoException($"Failed to start {nameof(this.RunAutoDetection)}");

            return proc;
        }
    }
}
