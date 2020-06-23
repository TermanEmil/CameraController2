using CameraControl.Exceptions;

namespace GphotoCameraControl.Exceptions
{
    public class GphotoException : CameraControlException
    {
        public GphotoException() : base()
        {
        }

        public GphotoException(string message) : base(message)
        {
        }

        public GphotoException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
