using System;
using System.Data.Entity;

namespace Edis.Repositories.Utils
{
    public class LazyLoading : IDisposable
    {
        private bool lazyLoadingEnabled;
        private DbContext context;
        public LazyLoading(DbContext context, bool enabled)
        {
            lazyLoadingEnabled = context.Configuration.LazyLoadingEnabled;
            context.Configuration.LazyLoadingEnabled = enabled;
            this.context = context;
        }

        public void Dispose()
        {
            context.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
        }
    }
}
