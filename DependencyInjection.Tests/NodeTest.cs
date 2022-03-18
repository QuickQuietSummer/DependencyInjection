using System.Linq;
using DependencyInjection.Lib.DI;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class NodeTest
    {
        [Test]
        public void ShouldReturnCorrectNode()
        {
            Node node = Node.GetNodeFromType(typeof(Root));
            Assert.True(node.RootType == typeof(Root));
            Assert.True(node.DependencyTypes.Any(type => type == typeof(Dep1)));
            Assert.True(node.DependencyTypes.Any(type => type == typeof(Dep2)));
            Assert.True(node.DependencyTypes.Any(type => type == typeof(Dep3)));
        }

        private class Root
        {
            public Root(Dep1 dep1, Dep2 dep2, Dep3 dep3)
            {
            }
        }

        private class Dep1
        {
        }

        private class Dep2
        {
        }

        private class Dep3
        {
        }
    }
}