using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CommonMappingUtils;
using Microsoft.Extensions.DependencyInjection;

namespace WebUi.StartupConfigExtensions
{
    public static class MappingConfigurationExtensions
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
