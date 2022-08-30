using System;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Data
{
    public class Categorias
    {
        public Guid Id { get; set; } 
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Profissionais> Profissionais { get; set; }
    }
}
