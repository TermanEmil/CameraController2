using System.Diagnostics;

namespace Processes.ProcessWrappers
{
    public class ProcessRunnerWrapper : IProcessRunner
    {
        public IProcess Start(string filename)
        {
            var proc = Process.Start(filename);
            if (proc is null)
                return null;

            return new ProcessWrapper(proc);
        }
    }
}
