using ImplantaDEVTraining.Common;
using System;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public class EnderecosFilterEntity: BaseFilterEntity
    {
        public Guid? IdProfissional { get; set; }
        public List<Guid> IdsProfissionais { get; set; }
    }
}
