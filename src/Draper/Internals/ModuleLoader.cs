using System;

namespace Draper.Internals
{
    internal static class ModuleLoader
    {
        public static void LoadModule(Type moduleType)
        {
            var module = CreateModule(moduleType);
        }

        public static IDataAccessModule CreateModule(Type moduleType)
        {
            return (IDataAccessModule) Activator.CreateInstance(moduleType);
        } 
    }
}