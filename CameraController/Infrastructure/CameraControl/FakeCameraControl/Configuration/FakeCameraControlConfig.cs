using System.Collections.Generic;

namespace FakeCameraControl.Configuration
{
    public class FakeCameraControlConfig
    {
        public bool Enabled { get; set; }

        public List<FakeCameraConfiguration> Cameras { get; set; } = new List<FakeCameraConfiguration>();
    }
}
