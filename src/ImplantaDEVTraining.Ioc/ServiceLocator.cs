using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace ImplantaDEVTraining.Ioc
{
    public static class ServiceLocator
    {
        public static void Bind(IKernel kernel)
        {

            /*https//stackoverflow.com/questions/28739091/ninject-binding-all-interfaces-to-the-same-class-in-singleton-scope
             
            Código criado com base na URL acima
             */
            kernel.Bind(x => x.FromAssembliesMatching("*.dll")
            .SelectAllClasses()
            .InNamespaces("ImplantaDEVTraining.Business.Concret")
            .BindAllInterfaces()
            .Configure(z => z.InRequestScope()));

            /*kernel.Bind<ICategoriasBusiness>().To<CategoriasBusiness>().InRequestScope();
            kernel.Bind<IEnderecosBusiness>().To<EnderecosBusiness>().InRequestScope();
            kernel.Bind<IProfissionaisBusiness>().To<ProfissionaisBusiness>().InRequestScope();*/
        }
    }
}
