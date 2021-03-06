﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CameraControl
{
    public interface ICameraManager
    {
        Task<IEnumerable<Camera>> AutoDetectCameras(CancellationToken ct = default);
        Task<Camera> FindCamera(string port, CancellationToken ct = default);
    }
}
