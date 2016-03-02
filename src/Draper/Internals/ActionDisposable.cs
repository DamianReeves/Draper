using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Draper.Internals
{
    internal class ActionDisposable:IDisposable
    {
        private Action _onDisposed;

        public ActionDisposable(Action onDisposed)
        {
            if (onDisposed == null)
            {
                throw new ArgumentNullException(nameof(onDisposed));
            }

            _onDisposed = onDisposed;
        }

        ~ActionDisposable()
        {
            Dispose(false);            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);            
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                var action = Interlocked.Exchange(ref _onDisposed, null);
                if (action != null)
                {
                    action();
                }
            }
        }
    }
}
