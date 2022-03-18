using System;
using System.Collections.Generic;
using DependencyInjection.Lib.DI;

namespace DependencyInjection.App
{
    public class MathOperationProvider : ServiceProvider
    {
        public override IEnumerable<Binding<Type, Type>> Bind()
        {
            return new[]
            {
                new Binding<Type, Type>(typeof(IIntOperation), typeof(IntSum))
            };
        }
    }
}