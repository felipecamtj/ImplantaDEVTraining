using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace ImplantaDEVTraining.Ioc
{
    public static class ServiceLocator
    {
        public static void Bind(IKernel kernel)
        {
            //https://stackoverflow.com/questions/9824863/convention-based-dependency-injection-with-ninject-3-0-0 

            kernel.Bind(x => x.FromAssembliesMatching("..\\..\\ImplantaDEVTraining.Ioc\\bin\\Debug\\ImplantaDEVTraining.Business.Concret.dll")
                  .SelectAllClasses()
                  .BindAllInterfaces()
                  .Configure(o => o.InRequestScope()));

        }
    }
}
