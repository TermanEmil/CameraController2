using System;
using System.ComponentModel;
using CameraControl.Exceptions;
using GphotoCameraControl.Exceptions;
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

        public IProcess Start(string filename)
        {
            try
            {
                return this.wrappee.Start(filename);
            }
            catch (Win32Exception e)
            {
                throw new UnsupportedActionException("This action is not supported on Windows", e);
            }
        }
    }
}
