using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.Lib.DI;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class GraphTest
    {
        [Test]
        public void ShouldContainRegisteredTypes()
        {
            Graph graph = new Graph(typeof(Root));
            Assert.True(graph.RegisteredRootTypes.Any(type => type == typeof(Root)));
            Assert.True(graph.RegisteredRootTypes.Any(type => type == typeof(Dep1)));
            Assert.True(graph.RegisteredRootTypes.Any(type => type == typeof(Dep2)));
            Assert.True(graph.RegisteredRootTypes.Any(type => type == typeof(Dep3)));
        }

        [Test]
        public void ShouldContainNodes()
        {
            Graph graph = new Graph(typeof(Root));

            Assert.True(graph.Nodes.Count == 4);
            // Assert.True(graph.Nodes.Count == 4);
        }

        [Test]
        public void ShouldContainAllRootTypesNode()
        {
            Graph graph = new Graph(typeof(Root));

            Assert.True(graph.Nodes.Any(node => node.RootType == typeof(Root)));
            Assert.True(graph.Nodes.Any(node => node.RootType == typeof(Dep1)));
            Assert.True(graph.Nodes.Any(node => node.RootType == typeof(Dep2)));
            Assert.True(graph.Nodes.Any(node => node.RootType == typeof(Dep3)));
        }

        [Test]
        public void ShouldContainOnlyRootTypeIfNoArgs()
        {
            Graph graph = new Graph(typeof(Root));
            Assert.IsNotEmpty(graph.Nodes.First(node => node.RootType == typeof(Root)).DependencyTypes);
            Assert.IsEmpty(graph.Nodes.First(node => node.RootType    == typeof(Dep1)).DependencyTypes);
            Assert.IsEmpty(graph.Nodes.First(node => node.RootType    == typeof(Dep2)).DependencyTypes);
            Assert.IsEmpty(graph.Nodes.First(node => node.RootType    == typeof(Dep3)).DependencyTypes);
        }

        [Test]
        public void ShouldReturnCorrectNodesEvenWithComplexHierarchy()
        {
            Graph graph = new Graph(typeof(D1));
            var   nodes = graph.Nodes;

            Assert.True(nodes.Any(node => node.RootType == typeof(D1) && node.DependencyTypes.Contains(typeof(D3))));
            Assert.True(nodes.Any(node => node.RootType == typeof(D2) && node.DependencyTypes.Contains(typeof(D4))));
            Assert.True(nodes.Any(node => node.RootType == typeof(D3)
                                       && node.DependencyTypes.Contains(typeof(D1))
                                       && node.DependencyTypes.Contains(typeof(D5))
                                       && node.DependencyTypes.Contains(typeof(D6))
                                 ));
            Assert.True(nodes.Any(node => node.RootType == typeof(D4) && node.DependencyTypes.Contains(typeof(D5))));
            Assert.True(nodes.Any(node => node.RootType == typeof(D5) && node.DependencyTypes.Contains(typeof(D8))));
            Assert.True(nodes.Any(node => node.RootType == typeof(D6) && !node.DependencyTypes.Any()));
            Assert.True(nodes.Any(node => node.RootType == typeof(D7) && !node.DependencyTypes.Any()));
            Assert.True(nodes.Any(node => node.RootType == typeof(D8)
                                       && node.DependencyTypes.Contains(typeof(D1))
                                       && node.DependencyTypes.Contains(typeof(D2))
                                       && node.DependencyTypes.Contains(typeof(D3))
                                       && node.DependencyTypes.Contains(typeof(D4))
                                       && node.DependencyTypes.Contains(typeof(D5))
                                       && node.DependencyTypes.Contains(typeof(D6))
                                       && node.DependencyTypes.Contains(typeof(D7))
                                       && node.DependencyTypes.Contains(typeof(D8))
                                 ));
        }

        [Test]
        public void ShouldReturnOnlyUniqueRegisteredRootTypes()
        {
            Graph             graph               = new Graph(typeof(D1));
            IEnumerable<Type> registeredRootTypes = graph.RegisteredRootTypes;
            Assert.True(registeredRootTypes.Count() == registeredRootTypes.Distinct().Count());
        }

        [Test]
        public void ShouldReturnOnlyUniqueRootTypesInTheNodes()
        {
            Graph             graph = new Graph(typeof(D1));
            IEnumerable<Node> nodes = graph.Nodes;

            var allLookedRootTypes = new List<Type>();

            foreach (Node eachNode in graph.Nodes)
            {
                if (allLookedRootTypes.Any(lookedRootType => lookedRootType == eachNode.RootType))
                    Assert.Fail("Several root types exist.");

                allLookedRootTypes.Add(eachNode.RootType);
            }

            Assert.Pass("All root types are unique.");
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

        private class SomeClassWithVeryComplexDependencies
        {
            public SomeClassWithVeryComplexDependencies(D1 d1, D2 d2, D3 d3)
            {
            }
        }

        private class D1
        {
            public D1(D3 d3)
            {
            }
        }

        private class D2
        {
            public D2(D4 d4)
            {
            }
        }

        private class D3
        {
            public D3(D1 d1, D5 d5, D6 d6)
            {
            }
        }

        private class D4
        {
            public D4(D5 d5)
            {
            }
        }

        private class D5
        {
            public D5(D8 d8)
            {
            }
        }

        private class D6
        {
        }

        private class D7
        {
        }

        private class D8
        {
            public D8(D1 d1, D2 d2, D3 d3, D4 d4, D5 d5, D6 d6, D7 d7, D8 d8)
            {
            }
        }
    }
}