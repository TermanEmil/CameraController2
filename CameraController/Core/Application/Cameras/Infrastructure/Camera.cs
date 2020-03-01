namespace Application.Cameras.Infrastructure
{
    public class Camera
    {
        public Camera(string model, string port)
        {
            this.Model = model;
            this.Port = port;
        }

        public string Model { get; }
        public string Port { get; }
    }
}
