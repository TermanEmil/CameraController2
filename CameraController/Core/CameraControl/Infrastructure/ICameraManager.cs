using System.Collections.Generic;
using CameraControl.Entities;

namespace CameraControl.Infrastructure
{
    public interface ICameraManager
    {
        IEnumerable<Camera> AutoDetectCameras();
    }
}
