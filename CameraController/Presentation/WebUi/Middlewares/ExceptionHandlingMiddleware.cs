using System.Net.Mime;
using System.Threading.Tasks;
using CameraControl.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebUi.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (UnsupportedActionException e)
            {
                await HandleUnsupportedActionException(context, e);
            }
            catch (CameraControlException e)
            {
                await HandleCameraControlException(context, e);
            }
        }

        private static async Task HandleUnsupportedActionException(HttpContext context, UnsupportedActionException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Error = e.Message }));
        }

        private static async Task HandleCameraControlException(HttpContext context, CameraControlException e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Error = e.Message }));
        }
    }
}
