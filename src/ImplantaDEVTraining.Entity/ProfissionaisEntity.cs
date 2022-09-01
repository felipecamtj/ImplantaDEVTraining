using ImplantaDEVTraining.Common;
using System;
using System.Collections.Generic;

namespace ImplantaDEVTraining.Entity
{
    public class ProfissionaisEntity: BaseEntity
    {
        public ProfissionaisEntity()
        {
            Ativo = true;
        }

        public override Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataNascimento { get; set; } 
        public string CPF { get; set; }
        public string RG { get; set; } 
        public string Observacoes { get; set; }
        public Guid IdCategoria { get; set; }
        public ICollection<EnderecosEntity> Enderecos { get; set; }
    }
}
