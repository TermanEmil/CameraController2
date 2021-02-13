using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using CameraControl.Commands.AutoDetect;
using CameraControl.Commands.CaptureImage;
using CameraControl.Commands.CaptureImageAndDownload;
using CameraControl.Commands.CreatePreviewSource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WebUi.Common;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CaptureImage(string port, string path, string filename, CancellationToken ct)
        {
            await this.Mediator.Send(new CaptureImageCommand(port, path, filename), ct);
            return this.NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CaptureImageAndDownload(string port, CancellationToken ct)
        {
            var path = await this.Mediator.Send(new CaptureImageAndDownloadCommand(port), ct);

            var contentTypeProvider = new FileExtensionContentTypeProvider();
            if (!contentTypeProvider.TryGetContentType(path, out var contentType))
                contentType = MediaTypeNames.Application.Octet;

            return new PhysicalFileResult(path, contentType)
            {
                FileDownloadName = Path.GetFileName(path)
            };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CapturePreview(string port, CancellationToken ct)
        {
            var source = await this.Mediator.Send(new CreatePreviewSourceCommand(port), ct);
            return new MjpegStreamContent(source, delay: 1000/20);
        }
    }
}
