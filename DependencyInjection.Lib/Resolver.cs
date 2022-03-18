using System;
using System.Collections.Generic;
using DependencyInjection.Lib.DI;
using SimpleInjector;

namespace DependencyInjection.Lib
{
    public class Resolver
    {
        private readonly Container _container;

        public Resolver(params Type[] rootTypes)
        {
            _container = new Container();
            ServiceCollector serviceCollector = new ServiceCollector();
            var serviceProviders = serviceCollector.GetAllServiceProviders();

            foreach (ServiceProvider serviceProvider in serviceProviders)
            {
                var bindings = serviceProvider.Bind();
                foreach (var binding in bindings)
                {
                    _container.Register(binding.Binding1, binding.Binding2);
                }
            }


            foreach (Type rootType in rootTypes)
            {
                _container.Register(rootType);
            }


            _container.Verify();
        }


        public TInstance Resolve<TInstance>()
        {
            return (TInstance) _container.GetInstance(typeof(TInstance));
        }
    }
}