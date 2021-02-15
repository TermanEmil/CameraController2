using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common
{
    public class MjpegStreamContent : IActionResult
    {
        /// <summary>
        /// In web, for some reason, this is the standard for newlines
        /// </summary>
        private static string NewLine => "\r\n";
        private static string Boundary => "frame";
        private static string ContentType => $"multipart/x-mixed-replace;boundary={Boundary}";

        private readonly IAsyncEnumerable<byte[]> imageSource;
        private readonly int delay;

        public MjpegStreamContent(IAsyncEnumerable<byte[]> imageSource, int delay = 0)
        {
            this.imageSource = imageSource ?? throw new ArgumentNullException(nameof(imageSource));
            this.delay = delay;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var newLineBytes = Encoding.UTF8.GetBytes(NewLine);
            var outputStream = context.HttpContext.Response.Body;
            var ct = context.HttpContext.RequestAborted;
            context.HttpContext.Response.ContentType = ContentType;

            try
            {
                await foreach (var imageBytes in this.imageSource)
                {
                    var header =
                        $"--{Boundary}{NewLine}" +
                        $"Content-Type: image/jpeg{NewLine}" +
                        $"Content-Length: {imageBytes.Length}{NewLine}{NewLine}";

                    var headerData = Encoding.UTF8.GetBytes(header);
                    await outputStream.WriteAsync(headerData, 0, headerData.Length, ct);
                    await outputStream.WriteAsync(imageBytes, 0, imageBytes.Length, ct);
                    await outputStream.WriteAsync(newLineBytes, 0, newLineBytes.Length, ct);

                    await Task.Delay(this.delay, ct);

                    if (ct.IsCancellationRequested)
                        break;
                }
            }
            catch (TaskCanceledException)
            {
                // connection closed, no need to report this
            }
        }
    }
}
