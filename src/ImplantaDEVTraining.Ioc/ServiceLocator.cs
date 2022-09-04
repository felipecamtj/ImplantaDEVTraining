﻿using ImplantaDEVTraining.Business.Concret;
using ImplantaDEVTraining.Business.Contract;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace ImplantaDEVTraining.Ioc
{
    public static class ServiceLocator
    {
        public static void Bind(IKernel kernel)
        {
            kernel.Bind(x => x.FromAssembliesMatching("~/bin/DebugImplantaDEVTraining.Business.Concret.dll")
                  .SelectAllClasses()
                  .BindAllInterfaces()
                  .Configure(o => o.InRequestScope()));

            //kernel.Bind<ICategoriasBusiness>().To<CategoriasBusiness>().InRequestScope();
            //kernel.Bind<IEnderecosBusiness>().To<EnderecosBusiness>().InRequestScope();
            //kernel.Bind<IProfissionaisBusiness>().To<ProfissionaisBusiness>().InRequestScope();
        }
    }
}
