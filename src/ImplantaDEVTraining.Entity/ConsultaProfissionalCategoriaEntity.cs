using ImplantaDEVTraining.Common;
using System;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Entity
{
    public class ConsultaProfissionalCategoriaEntity
    {
        public ConsultaProfissionalCategoriaEntity()
        {
            Ativo = true;
        }
        public string Nome { get; set; }
        public bool Ativo { get; set; } 
        public DateTime? DataNascimento { get; set; } 
        public string CPF { get; set; }
        public string RG { get; set; }
        public ICollection<ConsultaProfissionalCategoriaEntity> Categorias{ get; set; }

    }
}
