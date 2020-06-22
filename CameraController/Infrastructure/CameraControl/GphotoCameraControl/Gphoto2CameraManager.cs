using System.Collections.Generic;
using System.Diagnostics;
using CameraControl.Entities;
using CameraControl.Infrastructure;

namespace GphotoCameraControl
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
