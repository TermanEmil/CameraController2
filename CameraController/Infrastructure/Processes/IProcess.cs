using System;
using System.IO;

namespace Processes
{
    public interface IProcess : IDisposable
    {
        StreamReader StandardOutput { get; }
        StreamReader StandardError { get; }
    }
}
