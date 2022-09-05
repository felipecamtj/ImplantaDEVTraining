using ImplantaDEVTraining.Business.Concret;
using ImplantaDEVTraining.Business.Contract;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace ImplantaDEVTraining.Ioc
{
    public static class ServiceLocator
    {
        public static void Bind(IKernel kernel)
        {
            CategoriasBusiness categorias = kernel.Get<CategoriasBusiness>();
            EnderecosBusiness enderecos = kernel.Get<EnderecosBusiness>();
            ProfissionaisBusiness profissioanl = kernel.Get<ProfissionaisBusiness>();
        }
    }
}
