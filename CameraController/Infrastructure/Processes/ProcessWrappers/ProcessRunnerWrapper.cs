using System.Diagnostics;

namespace Processes.ProcessWrappers
{
    public class ProcessRunnerWrapper : IProcessRunner
    {
        public IProcess Start(string filename, params string[] args)
        {
            var startInfo = new ProcessStartInfo(filename, string.Join(' ', args))
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            var proc = Process.Start(startInfo);
            if (proc is null)
                return null;
            
            return new ProcessWrapper(proc);
        }
    }
}
