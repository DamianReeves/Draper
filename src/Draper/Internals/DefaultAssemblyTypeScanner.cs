using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Draper.Internals
{
    public class DefaultAssemblyTypeScanner
    {        
        private readonly Func<Type, bool> _typeFilter;

        public DefaultAssemblyTypeScanner(Func<Type,bool> typeFilter)
        {
            if (typeFilter == null) throw new ArgumentNullException(nameof(typeFilter));
            _typeFilter = typeFilter;
        }

        public Func<Type, bool> TypeFilter
        {
            get { return _typeFilter; }
        }

        public IEnumerable<Type> DiscoverTypes(Assembly assembly)
        {
            return GetAssemblyTypes(assembly).Where(TypeFilter);
        }

        protected virtual IEnumerable<TypeInfo> GetAssemblyTypes(Assembly assembly)
        {
            return assembly.DefinedTypes.Where(t=>t.IsPublic);
        } 
    }
}