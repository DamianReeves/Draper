using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if COREFX
using IDataReader = System.Data.Common.DbDataReader;
#endif

namespace Draper
{
    partial class SqlDataMapper
    {
        public static IEnumerable<T> Materialize<T>(this IDataReader reader)
        {
            return Dapper.SqlMapper.Parse<T>(reader);
        }

        public static IEnumerable<object> Materialize(this IDataReader reader, Type type)
        {
            return Dapper.SqlMapper.Parse(reader);
        } 
    }
}
