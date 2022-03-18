using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.Lib;
using DependencyInjection.Lib.DI;
using NUnit.Framework;
using SimpleInjector;

namespace DependencyInjection.Tests
{
    public class ResolverTest
    {
        [Test]
        public void ShouldReturnValidImplementation()
        {
            Resolver resolver = new Resolver();
            SomeClass implementation = resolver.Resolve<SomeClass>();
            Assert.True(typeof(SomeClassInheritor) == implementation.GetType());
        }

        [Test]
        public void ShouldRegisterRootType()
        {
            Resolver resolver = new Resolver(typeof(Calculator));
            Calculator calculator = resolver.Resolve<Calculator>();
            Assert.True(typeof(Calculator) == calculator.GetType());
        }

        [Test]
        public void ShouldResolveValidDependencies()
        {
            Resolver resolver = new Resolver(typeof(Calculator));
            Calculator calculator = resolver.Resolve<Calculator>();
            Assert.True(calculator.Calculate(5, 5) == 10);
        }


        private class SomeClass
        {
        }

        private class SomeClassInheritor : SomeClass
        {
        }

        private class Calculator
        {
            private readonly SomeClass  _someClass;
            private readonly IOperation _operation;

            public Calculator(SomeClass someClass, IOperation operation)
            {
                _someClass = someClass;
                _operation = operation;
            }

            public int Calculate(int a, int b)
            {
                return _operation.Execute(a, b);
            }
        }

        private interface IOperation
        {
            int Execute(int a, int b);
        }

        private class SumOperation : IOperation
        {
            public int Execute(int a, int b)
            {
                return a + b;
            }
        }

        private class SomeServiceProvider : ServiceProvider
        {
            public override IEnumerable<Binding<Type, Type>> Bind()
            {
                return new[]
                {
                    new Binding<Type, Type>(typeof(SomeClass), typeof(SomeClassInheritor)),
                    new Binding<Type, Type>(typeof(IOperation), typeof(SumOperation))
                };
            }
        }
    }
}