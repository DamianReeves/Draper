using System;
using System.Data;
using Dapper;

namespace Draper.DbTypes
{
    public sealed class RefCursor
    {
        public static readonly RefCursor Out = new RefCursor(ParameterDirection.Output);
        public static readonly RefCursor InOut = new RefCursor(ParameterDirection.InputOutput);
        public static readonly RefCursor In = new RefCursor(ParameterDirection.Input);
        public static readonly RefCursor Return = new RefCursor(ParameterDirection.ReturnValue);

        private RefCursor(ParameterDirection direction)
        {
            Direction = direction;
        }

        public ParameterDirection Direction { get; }
    }
}
