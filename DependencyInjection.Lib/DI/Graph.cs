using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace DependencyInjection.Lib.DI
{
    public class Graph
    {
        public Type RootType { get; }

        private List<Type>               _registeredRootTypes = new List<Type>();
        public  ReadOnlyCollection<Type> RegisteredRootTypes => new ReadOnlyCollection<Type>(_registeredRootTypes);

        private List<Node>               _nodes = new List<Node>();
        public  ReadOnlyCollection<Node> Nodes => new ReadOnlyCollection<Node>(_nodes);

        public Graph(Type type)
        {
            RootType = type;
            Resolve();
        }

        private void Resolve()
        {
            var typePool = new List<Type> {RootType};

            while (typePool.Count != 0)
            {
                Type currentType = typePool[0];
                Node node        = Node.GetNodeFromType(currentType);

                var nodeDependenciesTypes = RegisterAndGetDependenciesTypes(node);

                typePool.Remove(currentType);
                typePool.AddRange(nodeDependenciesTypes);
            }
        }

        private List<Type> RegisterAndGetDependenciesTypes(Node node)
        {
            var defaultValue = new List<Type>();

            if (_nodes.Any(nodeItem => nodeItem.RootType == node.RootType)) return defaultValue;

            _nodes.Add(node);


            if (_registeredRootTypes.Contains(node.RootType)) return defaultValue;

            _registeredRootTypes.Add(node.RootType);

            return node.DependencyTypes;
        }
    }
}