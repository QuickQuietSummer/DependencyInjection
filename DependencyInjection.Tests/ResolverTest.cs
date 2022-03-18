using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.Lib;
using DependencyInjection.Lib.DI;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class ResolverTest
    {
        [Test]
        public void ShouldReturnDependenciesTypes()
        {
            Resolver resolver = new Resolver();

            var dependencies = resolver.GetDependenciesTypes(typeof(SomeClass));
            Console.WriteLine(dependencies);
            Assert.True(dependencies.Any(type => type == typeof(SomeDependencyClassImplement)));
        }

        [Test]
        public void ShouldExceptionIfPrivateCtor()
        {
            Resolver resolver = new Resolver();
            Assert.Throws<ArgumentException>(() => resolver.GetDependenciesTypes(typeof(SomeClassWithPrivateCtor)));
        }

        [Test]
        public void ShouldReturnEmptyListIfNoCtorOrIfNoDependencies()
        {
            Resolver resolver = new Resolver();
            Assert.IsEmpty(resolver.GetDependenciesTypes(typeof(SomeClassWithoutCtor)));
            Assert.IsEmpty(resolver.GetDependenciesTypes(typeof(SomeClassWithEmptyDependencies)));
        }

        private class SomeClassWithEmptyDependencies
        {
            public SomeClassWithEmptyDependencies()
            {
            }
        }

        private class SomeClass
        {
            private readonly SomeDependencyClass _dependencyClass;

            public SomeClass(SomeDependencyClass dependencyClass)
            {
                _dependencyClass = dependencyClass;
            }
        }

        private class SomeClassWithoutCtor
        {
        }

        private class SomeClassWithPrivateCtor
        {
            private SomeClassWithPrivateCtor()
            {
            }
        }

        private class SomeDependencyClass
        {
        }

        private class SomeDependencyClassImplement : SomeDependencyClass
        {
        }

        private class SomeServiceProvider : ServiceProvider
        {
            public override IEnumerable<Binding<Type, Type>> Bind()
            {
                return new[]
                {
                    new Binding<Type, Type>(typeof(SomeDependencyClass), typeof(SomeDependencyClassImplement))
                };
            }
        }
    }
}