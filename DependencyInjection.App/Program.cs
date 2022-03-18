using System;
using DependencyInjection.Lib;
using DependencyInjection.Lib.DI;

namespace DependencyInjection.App
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Resolver resolver = new Resolver();
            // resolver.Resolve(typeof(IIntOperation));
            Calculator calculator =  resolver.Resolve<Calculator>();
            // Calculator calculator = new Calculator();
        }
    }
}