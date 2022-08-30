using System;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Data
{
    public class Profissionais
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Observacoes { get; set; }
        public Guid IdCategoria { get; set; }

        public virtual Categorias Categoria { get; set; }
        public virtual ICollection<Enderecos> Enderecos { get; set; }
    }
}
