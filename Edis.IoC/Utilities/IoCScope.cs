using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector.Extensions.LifetimeScoping;

namespace Edis.IoC.Utilities
{
    public class IoCScope : IDisposable
    {
        private readonly LifetimeScope _scope = null;

        public IoCScope(LifetimeScope scope)
        {
            this._scope = scope;
        }

        public void Dispose()
        {
            this._scope.Dispose();
        }
    }
}
