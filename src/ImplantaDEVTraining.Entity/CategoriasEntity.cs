using ImplantaDEVTraining.Common;
using System;

namespace ImplantaDEVTraining.Entity
{
    public class CategoriasEntity: BaseEntity
    {
        public CategoriasEntity()
        {
            Ativo = true;
        }

        public override Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
