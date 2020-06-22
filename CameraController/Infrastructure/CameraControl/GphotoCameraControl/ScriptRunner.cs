using System;
using Processes;

namespace GphotoCameraControl
{
    public interface IScriptRunner
    {
        IProcess RunAutoDetection();
    }

    public class ScriptRunner : IScriptRunner
    {
        private readonly IProcessRunner processRunner;

        public ScriptRunner(IProcessRunner processRunner)
        {
            this.processRunner = processRunner ?? throw new ArgumentNullException(nameof(processRunner));
        }

        public IProcess RunAutoDetection()
        {
            return this.processRunner.Start("auto-detect.sh");
        }
    }
}
