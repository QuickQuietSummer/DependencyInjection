using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyInjection.Lib.DI;
using DependencyInjection.Lib.Reflection;

namespace DependencyInjection.Lib
{
    public class Resolver
    {
        private Container _container;

        public Resolver()
        {
            _container = new Container();
            var serviceProviders = GetAllServiceProviders();
            foreach (ServiceProvider serviceProvider in serviceProviders)
            {
                var bindings = serviceProvider.Bind();
                foreach (var binding in bindings)
                {
                    _container.Register(binding.Binding1, binding.Binding2);
                }
            }
        }

        private List<ServiceProvider> GetAllServiceProviders()
        {
            var serviceProviderTypes = Reflector.GetAllSubclassesTypesOf(typeof(ServiceProvider));

            return serviceProviderTypes.Select(serviceProviderType => (ServiceProvider) Activator.CreateInstance(serviceProviderType))
                .ToList();
        }

        public TInstance Resolve<TInstance>()
        {
            Graph graph = new Graph(typeof(TInstance));

            var allNodes = graph.Nodes.ToList();

            List<Type>               rejectedTypesPool = new List<Type>();
            Dictionary<Type, object> insantiatedArgs   = new Dictionary<Type, object>();

            TInstance result;

            Node deepestDependency = allNodes.First(node => node.DependencyTypes.Count == 0);

            var deepestInstance = Activator.CreateInstance(deepestDependency.RootType);
            insantiatedArgs.Add(deepestDependency.RootType, deepestInstance);
            allNodes.Remove(deepestDependency);
    

            // foreach (Node eachNode in allNodes)
            // {
            //     // if ()
            // }
            return Activator.CreateInstance<TInstance>();
        }

        public List<Type> GetDependenciesTypes(Type type)
        {
            var allCtors = type.GetConstructors();

            if (allCtors.Length == 0) throw new ArgumentException($"Type {type.FullName} has no public constructors");

            ConstructorInfo ctor = allCtors[0];

            ParameterInfo[] eachCtorParams = ctor.GetParameters();

            return eachCtorParams.Select(eachCtorParam => _container.GetConcreteTypeFromAbstract(eachCtorParam.ParameterType)).ToList();
        }
    }
}