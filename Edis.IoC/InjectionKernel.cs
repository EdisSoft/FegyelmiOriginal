using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using Edis.IoC.Interfaces;
using Edis.IoC.Utilities;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Diagnostics;
using Edis.Diagnostics;

namespace Edis.IoC
{
    public class InjectionKernel
    {
        private readonly Container _container = new Container();

        private readonly ScopedLifestyle _hybridLifestyle = Lifestyle.CreateHybrid(
            lifestyleSelector: () => HttpContext.Current != null,
            falseLifestyle: new LifetimeScopeLifestyle(),
            trueLifestyle: new WebRequestLifestyle()
        );

        private SimpleInjectorDependencyResolver _dependencyResolver = null;

        #region Get instance

        public object GetInstance(Type type)
        {
            return this._container.GetInstance(type);
        }

        public TService GetInstance<TService>()
            where TService : class
        {
            return this._container.GetInstance<TService>();
        }


        #endregion

        #region Registry

        /// <summary>
        /// Registers and object in Singleton scope (one instance in the application)
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public void RegisterInSingletonScope<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this._container.RegisterSingle<TService, TImplementation>();
        }

        /// <summary>
        /// Registers an object in Transient scope (new instance every time)
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public void RegisterInTransientScope<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this._container.Register<TService, TImplementation>();
        }

        /// <summary>
        /// Registers an object in Request scope (one instance per web request)
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public void RegisterInRequestScope<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this._container.RegisterPerWebRequest<TService, TImplementation>();
        }

        /// <summary>
        /// Registers an object in Hybrid scope (one instance per web request or a new instance every time)
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public void RegisterInHybridScope<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this._container.Register<TService, TImplementation>(this._hybridLifestyle);
        }


        /// <summary>
        /// Registers an object in Session scope (one instance per session)
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public void RegisterInSessionScope<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.RegisterPerWebRequest<TService>(() =>
            {
                TImplementation instance = null;
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session[typeof(TImplementation).FullName] == null)
                    {
                        instance = (TImplementation) _instance.GetInstance(typeof(TImplementation));
                        HttpContext.Current.Session[typeof(TImplementation).FullName] = instance;
                    }
                    else
                    {
                        instance = (TImplementation) HttpContext.Current.Session[typeof(TImplementation).FullName];
                    }
                }
                else
                {
                    instance = (TImplementation) _instance.GetInstance(typeof(TImplementation));
                }
                return (TService) instance;
            });

        }

        #region RegisterInRealTransientScope

        public void RegisterInRealTransientScope<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this._container.Register<TService, TImplementation>(Lifestyle.Transient);
        }

        public void RegisterInRealTransientScope<TService>(Func<TService> instanceCreator)
            where TService : class
        {
            this._container.Register<TService>(instanceCreator);
        }
        
        public void RegisterInRealTransientScopeFunc<TType, TImplementation>()
            where TType : Type
            where TImplementation : class
        {
            this._container.Register<Func<TType, TImplementation>>(() =>
            {
                return (type) => (TImplementation) this._container.GetInstance(type);
            }, Lifestyle.Transient);
        }

        #endregion RegisterInRealTransientScope

   
        public void RegisterInSingletonScopeWithMultipleConstructor<TService>(TService instance) where TService : class
        {
            this._container.RegisterSingle<TService>(instance);
        }



        #endregion

        #region Scoping

        public IoCScope BeginScope()
        {
            return new IoCScope(this._container.BeginLifetimeScope());
        }

        #endregion

        #region Init/misc

        public IDependencyResolver GetDependencyResolver()
        {
            return this._dependencyResolver ??
                   (this._dependencyResolver = new SimpleInjectorDependencyResolver(this._container));
        }

        public void Initialize()
        {
            this._container.Options.PropertySelectionBehavior = new InjectionPropertySelectionBehavior();
            //this._container.ResolveUnregisteredType += this.OnResolveUnregisteredType;
            System.Diagnostics.Trace.WriteLine("Initializing instance...");

            Type type = typeof(IServiceLocator);
            Assembly[] assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                .Where(x => x.FullName.StartsWith("Edis.Functions")).ToArray();
            List<Type> modules = assemblies
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(x => x.GetInterfaces()
                        .Contains(type)))
                .ToList();

            //List<Type> modules = new List<Type>();
            //modules.Add(typeof(ServiceLocator))

            System.Diagnostics.Trace.WriteLine(assemblies.Count() + " assemblies, " + modules.Count + " types");

            foreach (Type module in modules)
            {
                System.Diagnostics.Trace.WriteLine("Registering types from " + module.FullName);
                IServiceLocator instance = (IServiceLocator) Activator.CreateInstance(module);
                instance.Register(this);
            }

            System.Diagnostics.Trace.WriteLine("Enabling MVC integration...");
            this._container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            this._container.RegisterMvcIntegratedFilterProvider();
            this._container.Verify();
        }

        public void Initialize(params Type[] classes)
        {
            this._container.Options.PropertySelectionBehavior = new InjectionPropertySelectionBehavior();
            //this._container.ResolveUnregisteredType += this.OnResolveUnregisteredType;
            System.Diagnostics.Trace.WriteLine("Initializing instance...");

            foreach (Type module in classes)
            {
                System.Diagnostics.Trace.WriteLine("Registering types from " + module.FullName);
                IServiceLocator instance = (IServiceLocator) Activator.CreateInstance(module);
                instance.Register(this);
            }

            System.Diagnostics.Trace.WriteLine("Enabling MVC integration...");
            this._container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            this._container.RegisterMvcIntegratedFilterProvider();
            this._container.Verify();
        }

        #endregion

        private InjectionKernel()
        {
            //We're antisocial, baby!
        }

        #region Singleton stuff

        private static volatile InjectionKernel _instance = null;

        public static InjectionKernel Instance
        {
            get { return _instance ?? (_instance = new InjectionKernel()); }
        }

        #endregion
    }



}