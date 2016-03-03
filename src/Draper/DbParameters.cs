using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


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
    public static class DbParameters
    {
        public static TParameter SetProviderSpecificDbType<TParameter>(
            this TParameter parameter,
            string dbType,
            bool throwOnFailure = false) where TParameter : IDbDataParameter
        {
            var parameterType = parameter?.GetType() ?? typeof(TParameter);
            var providerSpecificTypeProperty = 
                parameterType.GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes()
                .Any(attr => attr.GetType().Name == "DbProviderSpecificTypePropertyAttribute"));
            if (providerSpecificTypeProperty == null)
            {
                throw new InvalidOperationException("Cannot determine the appropriate type to set for this provider.");
            }
            else
            {
                var providerAssembly = parameterType.GetTypeInfo().Assembly;
                //TODO: Consider whether we need to force provider assembly load
                var providerSpecificType = providerSpecificTypeProperty.PropertyType;
                var parameterExpr = Expression.Parameter(parameterType, "parameter");
                var propertyExpr = Expression.Property(parameterExpr, providerSpecificTypeProperty);
                var rhsExpr = Expression.Field(null, providerSpecificType, dbType);
                var assignPropertyExpr = Expression.Assign(propertyExpr, rhsExpr);
                var lambdaExpr = Expression.Lambda(assignPropertyExpr, parameterExpr);
                //Console.WriteLine($"LambdaExpr: {lambdaExpr}");
                lambdaExpr.Compile().DynamicInvoke(parameter);
            }
            return parameter;
        }
    }

    public class Draper
    {
        
    }
}
