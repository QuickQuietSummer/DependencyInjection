using System;
using DependencyInjection.Lib.DI;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class ContainerTest
    {
        private Container _container;

        [SetUp]
        public void Setup()
        {
            _container = new Container();
        }

        [Test]
        public void ShouldSaveAndReturnBackValidConcreteBinding()
        {
            _container.Register(typeof(SomeAbstractClass), typeof(SomeConcreteClass));

            Type testingConcreteType = _container.GetConcreteTypeFromAbstract(typeof(SomeAbstractClass));

            Type actuallyConcreteType = typeof(SomeConcreteClass);

            Assert.True(testingConcreteType.FullName == actuallyConcreteType.FullName);
        }

        [Test]
        public void ShouldThrowExceptionIfWasNotBind()
        {
            Assert.Throws<ArgumentException>(() => _container.GetConcreteTypeFromAbstract(typeof(SomeAbstractClass)));
        }

        [Test]
        public void ShouldThrowExceptionIfBindSameAbstractClassTwice()
        {
            _container.Register(typeof(SomeAbstractClass), typeof(SomeConcreteClass));
            Assert.Throws<ArgumentException>(() => _container.Register(typeof(SomeAbstractClass), typeof(SomeConcreteClass)));
        }

        private class SomeConcreteClass
        {
        }

        private class SomeAbstractClass
        {
        }
    }
}