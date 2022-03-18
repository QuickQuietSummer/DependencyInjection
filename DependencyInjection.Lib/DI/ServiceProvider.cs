using System;
using System.Collections.Generic;

namespace DependencyInjection.Lib.DI
{
    public abstract class ServiceProvider
    {
        public abstract IEnumerable<Binding<Type, Type>> Bind();
    }
}