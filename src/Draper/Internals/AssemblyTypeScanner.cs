using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Draper.Internals
{
    public delegate IEnumerable<Type> AssemblyTypeScanner(Assembly assembly);
}
