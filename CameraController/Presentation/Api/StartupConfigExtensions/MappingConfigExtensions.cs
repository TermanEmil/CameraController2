using System.Linq;
using System.Reflection;
using AutoMapper;
using CommonMappingUtils;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupConfigExtensions
{
    public static class MappingConfigExtensions
    {
        public static void ConfigureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(
                mapperConfig =>
                {
                    mapperConfig.AddProfile(new AutoMapperProfile(typeof(Startup)));
                },
                Enumerable.Empty<Assembly>());
        }
    }
}
