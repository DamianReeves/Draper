using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Draper.DbTypes;

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


namespace Draper.Internals
{
    class DraperTypeHandler: Dapper.SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            var cursor = value as RefCursor;
            if (cursor != null)
            {
                HandleRefCursor(parameter, cursor);
            }
        }

        public object Parse(Type destinationType, object value)
        {
            return value;
        }

        private void HandleRefCursor(IDbDataParameter parameter, RefCursor cursor)
        {
            parameter.Direction = cursor.Direction;
            parameter.SetProviderSpecificDbType("RefCursor");
        }
    }
}
