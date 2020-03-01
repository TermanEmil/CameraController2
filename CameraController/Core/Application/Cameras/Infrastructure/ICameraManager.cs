using System.Collections.Generic;

namespace Application.Cameras.Infrastructure
{
    public interface ICameraManager
    {
        IEnumerable<Camera> AutoDetectCameras();
    }
}
