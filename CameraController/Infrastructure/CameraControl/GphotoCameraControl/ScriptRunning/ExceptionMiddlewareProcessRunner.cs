using CameraControl.Exceptions;
using Processes;
using System;
using System.ComponentModel;

namespace GphotoCameraControl.ScriptRunning
{
    public class ExceptionMiddlewareProcessRunner : IProcessRunner
    {
        private readonly IProcessRunner wrappee;

        public ExceptionMiddlewareProcessRunner(IProcessRunner wrappee)
        {
            this.wrappee = wrappee ?? throw new ArgumentNullException(nameof(wrappee));
        }

        public IProcess Start(string filename, params string[] args)
        {
            try
            {
                return this.wrappee.Start(filename, args);
            }
            catch (Exception e) when (e is Win32Exception || e is PlatformNotSupportedException)
            {
                throw new UnsupportedActionException("This action is not supported on this platform", e);
            }
        }
    }
}
