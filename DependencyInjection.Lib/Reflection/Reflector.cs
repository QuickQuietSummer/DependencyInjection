using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection.Lib.Reflection
{
    public class Reflector
    {
        public List<Type> GetAllSubclassesOf(Type parentType)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            List<Type> subclasses = (from eachAssembly in currentDomain.GetAssemblies()
                from eachType in eachAssembly.GetTypes()
                where eachType.IsSubclassOf(parentType)
                select eachType).ToList();
            return subclasses;
        }
    }
}