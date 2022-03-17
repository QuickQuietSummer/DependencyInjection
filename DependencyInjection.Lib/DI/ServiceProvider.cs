using System;
using System.Collections.Generic;

namespace DependencyInjection.Lib.DI
{
    public abstract class ServiceProvider
    {
        private Container _container = new Container();
        public abstract IEnumerable<Binding<Type,Type>> Bind();

        public void RefillContainer()
        {
            var bindings = Bind();
    
            foreach (var binding in bindings)
            {
                _container.Register(binding.Binding1, binding.Binding2);
            }
        }

        public IEnumerable<Binding<Type, Type>> GetAllBindings()
        {
            return _container.RegisteredTypes;
        }
    }
}