namespace GphotoCameraControl.Exceptions
{
    public class UnexpectedProcessOutputException : GphotoException
    {
        public UnexpectedProcessOutputException() : base("Unexpected process output")
        {
        }

        public UnexpectedProcessOutputException(string message) : base($"Unexpected process output: {message}")
        {
        }

        public UnexpectedProcessOutputException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
