using CameraControl.Commands.AutoDetect;
using CameraControl.Commands.CaptureImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebUi.Models;

namespace WebUi.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CamerasController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(CameraViewModel[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> AutoDetectCameras(CancellationToken ct)
        {
            var cameras = await this.Mediator.Send(new AutoDetectCommand(), ct);
            var viewModels = this.Mapper.Map<IEnumerable<CameraViewModel>>(cameras);
            return this.Ok(viewModels);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task CaptureImage(string port, string path, string filename)
        {
            await this.Mediator.Send(new CaptureImageCommand(port, path, filename));
        }
    }
}
