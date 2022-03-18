using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.Lib.Reflection;

namespace DependencyInjection.Lib.DI
{
    public class ServiceCollector
    {
        public List<ServiceProvider> GetAllServiceProviders()
        {
            var serviceProviderTypes = Reflector.GetAllSubclassesTypesOf(typeof(ServiceProvider));

            return serviceProviderTypes.Select(serviceProviderType => (ServiceProvider) Activator.CreateInstance(serviceProviderType))
                .ToList();
        }
    }
}