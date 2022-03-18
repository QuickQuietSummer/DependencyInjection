using DependencyInjection.Lib.Reflection;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class ReflectorTest
    {
        [Test]
        public void ShouldFindAllSubclasses()
        {
            var subclasses = Reflector.GetAllSubclassesTypesOf(typeof(ParentClass));

            Assert.True(subclasses.Count == 3);
        }

        private class ParentClass
        {
        }

        private class Child1 : ParentClass
        {
        }

        private class Child2 : ParentClass
        {
        }

        private class Child3 : ParentClass
        {
        }
    }
}