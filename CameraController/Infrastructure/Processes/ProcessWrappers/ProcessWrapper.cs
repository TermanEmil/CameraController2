#pragma warning disable CA1063 // Implement IDisposable Correctly (Ignored as it calls the wrapper's dispose)

using System;
using System.Diagnostics;
using System.IO;

namespace Processes.ProcessWrappers
{
    public class ProcessWrapper : IProcess
    {
        private readonly Process process;

        public ProcessWrapper(Process process)
        {
            this.process = process ?? throw new ArgumentNullException(nameof(process));
        }

        public void Dispose()
        {
            this.process.Dispose();
        }

        public StreamReader StandardOutput => this.process.StandardOutput;
        public StreamReader StandardError => this.process.StandardError;
    }
}
