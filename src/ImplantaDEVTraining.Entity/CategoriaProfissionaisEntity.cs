using ImplantaDEVTraining.Common;
using System;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Entity
{
    public class CategoriaProfissionaisEntity : BaseEntity
    {
        public override Guid Id { get; set ; }
        public string NomeCategoria { get; set; }
        public List<ProfissionaisCategoriaEntity> Profissionais { get; set; } = new List<ProfissionaisCategoriaEntity>();
    }
}
