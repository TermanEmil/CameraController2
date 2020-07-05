using System;

namespace CameraControl.Exceptions
{
    public class UnsupportedActionException : CameraControlException
    {
        public UnsupportedActionException() : base()
        {
        }

        public UnsupportedActionException(string message) : base(message)
        {
        }

        public UnsupportedActionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
