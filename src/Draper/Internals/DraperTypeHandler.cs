using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Draper.DbTypes;

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
