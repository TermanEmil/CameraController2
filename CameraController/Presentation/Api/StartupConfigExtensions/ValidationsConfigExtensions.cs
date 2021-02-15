using FakeCameraControl;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupConfigExtensions
{
    public static class ValidationsConfigExtensions
    {
        public static void ConfigureValidations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<FakeCamera>();
        }
    }
}
