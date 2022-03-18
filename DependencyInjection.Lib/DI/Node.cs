using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjection.Lib.DI
{
    public struct Node
    {
        public Type RootType { get; }

        public List<Type> DependencyTypes { get; }

        public Node(Type rootType, List<Type> dependencyTypes)
        {
            RootType        = rootType;
            DependencyTypes = dependencyTypes;
        }

        public static Node GetNodeFromType(Type type)
        {
            var dependingTypes = GetDependingTypes(type);
            return new Node(type, dependingTypes);
        }

        public static List<Type> GetDependingTypes(Type type)
        {
            var ctors = type.GetConstructors();
            if (ctors.Length == 0) throw new ArgumentException($"Type {type.FullName} has no public constructors");
            var args = ctors[0].GetParameters();

            return args.Select(parameterInfo => parameterInfo.ParameterType).ToList();
        }
    }
}