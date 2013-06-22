using DamirzoneWeb.Configuration;
using DamirzoneWeb.Configuration.Helpers;
using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DamirzoneWeb.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        IKernel _kernel;

        public NinjectDependencyResolver() {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }

        public IBindingToSyntax<T> Bind<T>() {
            return _kernel.Bind<T>();
        }

        public IKernel Kernel {
            get {
                return _kernel;
            }
        }

        void AddBindings() {
            var helper = new DamirzoneConfigurationHelper(ConfigurationManager.GetSection("dzc") as DamirzoneConfigurationSection);
            _kernel.Bind<DamirzoneConfigurationHelper>().ToConstant(helper);
        }
    }
}