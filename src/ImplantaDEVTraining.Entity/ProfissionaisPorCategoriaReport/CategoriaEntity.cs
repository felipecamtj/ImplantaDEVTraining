using System;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Entity.ProfissionaisPorCategoriaReport
{
    public class CategoriaEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<ProfissionalEntity> Profissionais { get; set; }
    }
}
