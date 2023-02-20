using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.IoC.Interfaces
{
    public interface IServiceLocator
    {
        void Register(InjectionKernel kernel);
    }
}
