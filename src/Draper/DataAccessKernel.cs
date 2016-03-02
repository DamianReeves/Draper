using System;

namespace Draper
{
    public class DataAccessKernel
    {
        private static Lazy<DataAccessKernel> KernelAccessor = new Lazy<DataAccessKernel>();
        private object _syncRoot = new object();
        public static void Start()
        {
            KernelAccessor.Value.Load();
        }

        public bool Load()
        {
            return false;
            lock (_syncRoot)
            {
                
            }
        }

        private class DataAccessKernelHandle:IDataAccessKernelHandle
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        
    }

    public interface IDataAccessKernelHandle : IDisposable { }
}