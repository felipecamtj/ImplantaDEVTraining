using ImplantaDEVTraining.Common;
using System;

namespace ImplantaDEVTraining.Entity
{
    public class ProfissionaisCategoriaEntity : BaseEntity
    {
        public override Guid Id { get; set ; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

    }
}
