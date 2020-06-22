using System.Diagnostics;

namespace Processes.ProcessWrappers
{
    public class ProcessRunnerWrapper : IProcessRunner
    {
        public IProcess Start(string filename)
        {
            return new ProcessWrapper(Process.Start(filename));
        }
    }
}
