using ImplantaDEVTraining.Common;
using ImplantaDEVTraining.Entity;
using ImplantaDEVTraining.Entity.FilterEntity;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Business.Contract
{
    public interface IProfissionaisBusiness: IBusiness<ProfissionaisEntity, ProfissionaisFilterEntity>
    {
        List<ProfissionaisCategoriasEntity> ProfissionaisAgrupadosCategorias(ProfissionaisCategoriasFilterEntity filtro);
    }
}
