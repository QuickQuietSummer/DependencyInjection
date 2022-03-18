using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection.Lib.Reflection
{
    public static class Reflector
    {
        public static List<Type> GetAllSubclassesTypesOf(Type parentType)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            List<Type> subclasses = (from eachAssembly in currentDomain.GetAssemblies()
                from eachType in eachAssembly.GetTypes()
                where eachType.IsSubclassOf(parentType)
                select eachType).ToList();
            return subclasses;
        }

        // public static void Get 
    }
}