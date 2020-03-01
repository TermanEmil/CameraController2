using System.Collections.Generic;
using System.Diagnostics;
using Application.Cameras.Infrastructure;

namespace Cameras.Gphoto2
{
    public class Gphoto2CameraManager : ICameraManager
    {
        public IEnumerable<Camera> AutoDetectCameras()
        {
            var processInfo = new ProcessStartInfo();
            //using var process = Process.Start()
            

            return null;
        }
    }
}
