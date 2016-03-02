using System;
using System.Data;
using Dapper;

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