using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Draper.Internals
{
    public static class AssemblyScanner
    {
        public const string DefaultAssemblyExclusionRegexString = @"mscorlib|System|System\..*|Microsoft\..*";

        public static readonly Regex DefaultAssemblyExclusionRegex =
            new Regex(DefaultAssemblyExclusionRegexString, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Predicate<Assembly> ShouldScanAssembly = asm =>
        {
            var asmName = asm.GetName()?.Name ?? string.Empty;
            return !DefaultAssemblyExclusionRegex.IsMatch(asmName);
        };

        public static bool ScanOnLoad { get; set; } = true;

        public static Func<IEnumerable<Assembly>> GetAssemblySources = GetDefaultAssemblySources;
        public static Func<Assembly, IEnumerable<Type>> GetModuleTypes = GetDataAccessModuleTypes;

        public static void Start()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }  

        internal static IEnumerable<Assembly> GetDefaultAssemblySources()
        {
            yield return typeof (AssemblyScanner).Assembly;
            yield return Assembly.GetEntryAssembly();
        }

        internal static IEnumerable<Type> GetDataAccessModuleTypes(Assembly assembly)
        {
            var dataAccessModuleType = typeof (DataAccessModule);
            return assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .Where(t => dataAccessModuleType.IsAssignableFrom(t));
        } 

        private static void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            if(!ScanOnLoad) return;
            if(!ShouldScanAssembly(args.LoadedAssembly)) return;
        }
    }
}