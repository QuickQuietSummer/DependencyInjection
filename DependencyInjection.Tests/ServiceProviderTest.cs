using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.Lib.DI;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class ServiceProviderTest
    {
        [Test]
        public void ShouldFillContainer()
        {
            SomeServiceProvider someServiceProvider = new SomeServiceProvider();

            var allBindings = someServiceProvider.Bind();

            Assert.True(allBindings.Any(binding => binding.Binding2 == typeof(SomeConcreteClass)));
            Assert.True(allBindings.Any(binding => binding.Binding2 == typeof(SomeImplementation)));
        }

        private class SomeServiceProvider : ServiceProvider
        {
            public override IEnumerable<Binding<Type, Type>> Bind()
            {
                return new[]
                {
                    new Binding<Type, Type>(typeof(SomeAbstractClass), typeof(SomeConcreteClass)),
                    new Binding<Type, Type>(typeof(ISomeInterface), typeof(SomeImplementation))
                };
            }
        }

        private class SomeConcreteClass : SomeAbstractClass
        {
        }

        private class SomeAbstractClass
        {
        }

        private interface ISomeInterface
        {
        }

        private class SomeImplementation : ISomeInterface
        {
        }
    }
}