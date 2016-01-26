using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fresher
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
}
