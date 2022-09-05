using ImplantaDEVTraining.Business.Concret;
using ImplantaDEVTraining.Business.Contract;
using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;

namespace ImplantaDEVTraining.Ioc
{
    public static class ServiceLocator
    {
        public static void Bind(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromAssembliesMatching(@"..\..\ImplantaDEVTraining.Ioc\bin\Debug\ImplantaDEVTraining.Business.Concret.dll")
                .SelectAllClasses()
                .BindDefaultInterface()
                .Configure(y => y.InRequestScope());
            });
        }
    }
}
