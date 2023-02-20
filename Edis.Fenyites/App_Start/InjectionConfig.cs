using Edis.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edis.Fenyites.App_Start
{
    public static class InjectionConfig
    {
        public static void InitializeInjection()
        {
            InjectionKernel.Instance.Initialize();
            DependencyResolver.SetResolver(InjectionKernel.Instance.GetDependencyResolver());
        }
        public static void InitializeInjection(params Type[] classes)
        {
            InjectionKernel.Instance.Initialize(classes);
            DependencyResolver.SetResolver(InjectionKernel.Instance.GetDependencyResolver());
        }
    }
}