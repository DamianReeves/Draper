using System;

namespace Draper.Internals
{
    public static class TypeFilters
    {
        public static readonly Func<Type, bool> AllTypes  = _ => true;
    }
}