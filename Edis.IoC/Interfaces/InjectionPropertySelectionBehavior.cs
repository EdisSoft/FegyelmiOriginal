using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Edis.IoC.Attributes;
using SimpleInjector.Advanced;

namespace Edis.IoC.Interfaces
{
    /// <summary>
    /// Behavior for selecting injected properties. We select everything that has the inject attribute on it.
    /// </summary>
    public class InjectionPropertySelectionBehavior : IPropertySelectionBehavior
    {
        public bool SelectProperty(Type serviceType, PropertyInfo info)
        {
            return info.GetCustomAttributes<InjectAttribute>().Any();
        }
    }
}
