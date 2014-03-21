using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WePayMVC5Example.IoC
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernal;

        public NinjectDependencyResolver()
        {
            kernal = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernal.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernal.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernal.Bind<IGlobalVariables>().To<GlobalVariables>();
        }
    }
}