using AutoMapper;
using CameraControl;
using CommonMappingUtils;

namespace Api.Models
{
    public class CameraViewModel : IHaveCustomMapping
    {
        public string Model { get; set; }
        public string Port { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Camera, CameraViewModel>();
        }
    }
}
