using System;
using System.Data;
using Dapper;


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
    public static partial class SqlDataMapper
    {
        public static IDataReader ExecuteReader(this IDbConnection connection, string sql, object parameters=null, IDbTransaction transaction=null, int? commandTimeout = null)
        {
            return SqlMapper.ExecuteReader(connection, sql, parameters, transaction, commandTimeout,CommandType.StoredProcedure);
        }

        public static IDataReader ExecuteReaderSql(this IDbConnection connection, string sql, object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return SqlMapper.ExecuteReader(connection, sql, parameters, transaction, commandTimeout, CommandType.StoredProcedure);
        }
    }
}