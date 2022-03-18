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
            var wasBind     = allBindings.Any(binding => binding.Binding2 == typeof(SomeConcreteClass));
            Assert.True(wasBind);
        }

        private class SomeServiceProvider : ServiceProvider
        {
            public override IEnumerable<Binding<Type, Type>> Bind()
            {
                return new[]
                {
                    new Binding<Type, Type>(typeof(SomeAbstractClass), typeof(SomeConcreteClass))
                };
            }
        }

        private class SomeConcreteClass
        {
        }

        private class SomeAbstractClass
        {
        }
    }
}