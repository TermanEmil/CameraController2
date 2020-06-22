using System;

namespace CameraControl.Exceptions
{
    public class CameraControlException : Exception
    {
        public CameraControlException() : base()
        {
        }

        public CameraControlException(string message) : base(message)
        {
        }

        public CameraControlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
