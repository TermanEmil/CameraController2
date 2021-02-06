using System;

namespace CameraControl.Exceptions
{
    public class CameraNotFoundException : CameraControlException
    {
        public CameraNotFoundException()
        {
        }

        public CameraNotFoundException(string port)
            : base($"Camera not found on port: {port}")
        {
        }

        public CameraNotFoundException(string port, Exception innerException)
            : base($"Camera not found on port: {port}", innerException)
        {
        }
    }
}
