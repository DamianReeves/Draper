namespace Draper.Internals
{
    public static class AssemblyTypeScanners
    {
        public static readonly AssemblyTypeScanner AllTypesScanner = new DefaultAssemblyTypeScanner(TypeFilters.AllTypes).DiscoverTypes;
    }
}