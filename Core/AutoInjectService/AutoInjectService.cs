using Core.Attribute;
using Microsoft.Extensions.DependencyInjection;

namespace Core.AutoInjectService;

public static class AutoInjectService
{
    public static IServiceCollection AutoRegistryService(this IServiceCollection serviceCollection)
    {
        var types = AssemblyHelper.GetTypesByAssembly("Application").ToArray();
        foreach (var serviceType in types)
            if (System.Attribute.IsDefined(serviceType, typeof(DynamicApiAttribute)))
            {
                var serviceRegistryAttribute =
                    (DynamicApiAttribute)serviceType.GetCustomAttributes(typeof(DynamicApiAttribute), false)
                        .FirstOrDefault()!;
                if (serviceRegistryAttribute == null) continue;
                var interfaces = serviceType.GetInterfaces();
                //获取首个接口
                var serviceInterfaceType = interfaces.FirstOrDefault();
                if (serviceInterfaceType == null) continue;
                switch (serviceRegistryAttribute.ServiceLifeCycle)
                {
                    case "Singleton":
                        serviceCollection.AddSingleton(serviceInterfaceType, serviceType);
                        break;
                    case "Scoped":
                        serviceCollection.AddScoped(serviceInterfaceType, serviceType);
                        break;
                    case "Transient":
                        serviceCollection.AddTransient(serviceInterfaceType, serviceType);
                        break;
                }
            }

        return serviceCollection;
    }
}