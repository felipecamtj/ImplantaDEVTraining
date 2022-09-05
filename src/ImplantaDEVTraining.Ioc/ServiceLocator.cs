using ImplantaDEVTraining.Business.Concret;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace ImplantaDEVTraining.Ioc
{
    public static class ServiceLocator
    {
        public static void Bind(IKernel kernel)
        {
            kernel.Bind(x => 
            {
                x.FromAssembliesMatching(
                    "*ImplantaDEVTraining.Business.Contract*",
                    "*ImplantaDEVTraining.Business.Concret*")
                .SelectAllClasses()
                .BindDefaultInterfaces()
                .Configure(y => y.InRequestScope());
            });
        }

        private static CategoriasBusiness _foo;
    }
}
