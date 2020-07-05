using System;
using CameraControl.Infrastructure;
using GphotoCameraControl;
using GphotoCameraControl.ScriptRunning;
using Microsoft.Extensions.DependencyInjection;
using Processes;
using Processes.ProcessWrappers;
using WebUi.Middlewares;

namespace WebUi.StartupConfigExtensions
{
    public static class AppDependenciesConfigExtensions
    {
        public static void ConfigureAppDependencies(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddTransient<IProcessRunner, ProcessRunnerWrapper>();
            services.Decorate<IProcessRunner, ExceptionMiddlewareProcessRunner>();

            services.ConfigureGphoto();
        }

        private static void ConfigureGphoto(this IServiceCollection services)
        {
            services.AddTransient<IScriptRunner, ScriptRunner>();
            services.AddTransient<ICameraManager, GphotoCameraManager>();
        }
    }
}
