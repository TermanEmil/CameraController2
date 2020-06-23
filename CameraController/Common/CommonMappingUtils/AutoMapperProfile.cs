using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace CommonMappingUtils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(Type containingType)
        {
            var typesAssembly = Assembly.GetAssembly(containingType);
            if (typesAssembly is null)
                return;

            var mappings = typesAssembly
                .GetExportedTypes()
                .Where(type =>
                    typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                    !type.IsAbstract &&
                    !type.IsInterface)
                .Select(type => Activator.CreateInstance(type) as IHaveCustomMapping)
                .Where(mapping => mapping != null);

            foreach (var mapping in mappings)
                mapping.CreateMappings(this);
        }
    }
}
