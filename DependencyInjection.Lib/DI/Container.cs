using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DependencyInjection.Lib.DI
{
    public partial class Container
    {
        private readonly List<Binding<Type, Type>> _registeredTypes = new List<Binding<Type, Type>>();

        public ReadOnlyCollection<Binding<Type, Type>> RegisteredTypes => new ReadOnlyCollection<Binding<Type, Type>>(_registeredTypes);

        public void Register(Type abstractTypeForRegister, Type concreteTypeForRegister)
        {
            if (_registeredTypes.Any(registeredType => abstractTypeForRegister == registeredType.Binding1))
            {
                throw new ArgumentException("Type already was bind.");
            }

            _registeredTypes.Add(new Binding<Type, Type>(abstractTypeForRegister, concreteTypeForRegister));
        }

        public Type GetConcreteTypeFromAbstract(Type abstractType)
        {
            Type result = (from registeredType in _registeredTypes
                where registeredType.Binding1 == abstractType
                select registeredType.Binding2).FirstOrDefault();

            if (result == null)
            {
                throw new ArgumentException($"{abstractType.FullName} was not bind.");
            }

            return result;
        }
    }
}