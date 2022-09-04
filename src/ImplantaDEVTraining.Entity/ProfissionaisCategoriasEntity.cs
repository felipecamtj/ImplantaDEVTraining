using System;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Entity
{
    public class ProfissionaisCategoriasEntity
    {
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

    }
    public class CategoriasProfissionaisEntity
    {
        public CategoriasProfissionaisEntity()
        {
            Profissionais = new List<ProfissionaisCategoriasEntity>();

        }
        public string NomeCategoria { get; set; }
        public ICollection<ProfissionaisCategoriasEntity> Profissionais { get; set; }


    }
}
