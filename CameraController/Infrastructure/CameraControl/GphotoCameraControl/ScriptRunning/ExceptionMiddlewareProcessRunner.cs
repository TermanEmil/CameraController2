using System;
using System.ComponentModel;
using System.IO;
using CameraControl.Exceptions;
using Processes;

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
            catch (Win32Exception e)
            {
                throw new CameraControlException($"Failed to run '{Path.GetFileName(filename)}'", e);
            }
            catch (PlatformNotSupportedException e)
            {
                throw new UnsupportedActionException("This action is not supported on this platform", e);
            }
        }
    }
}
