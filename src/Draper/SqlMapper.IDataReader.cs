using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if COREFX
using IDbDataParameter = System.Data.Common.DbParameter;
using IDataParameter = System.Data.Common.DbParameter;
using IDbTransaction = System.Data.Common.DbTransaction;
using IDbConnection = System.Data.Common.DbConnection;
using IDbCommand = System.Data.Common.DbCommand;
using IDataReader = System.Data.Common.DbDataReader;
using IDataRecord = System.Data.Common.DbDataReader;
using IDataParameterCollection = System.Data.Common.DbParameterCollection;
using DataException = System.InvalidOperationException;
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
