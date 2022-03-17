using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection.Lib.DI
{
    public partial class Container
    {
        public List<Binding<Type, Type>> RegisteredTypes { get; private set; } = new List<Binding<Type, Type>>();

        public void Register(Type abstractTypeForRegister, Type concreteTypeForRegister)
        {
            if (RegisteredTypes.Any(registeredType => abstractTypeForRegister == registeredType.Binding1))
            {
                throw new ArgumentException("Type already was bind.");
            }

            RegisteredTypes.Add(new Binding<Type, Type>(abstractTypeForRegister, concreteTypeForRegister));
        }

        public Type GetConcreteTypeFromAbstract(Type abstractType)
        {
            Type result = (from registeredType in RegisteredTypes
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