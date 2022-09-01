using ImplantaDEVTraining.Common;
using System;

namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public class ProfissionaisFilterEntity: BaseFilterEntity
    {
        public string Nome { get; set; }
        public bool SomenteAtivos { get; set; } = true;
        public Guid? IdCategoria { get; set; }
        public bool CarregarEnderecos { get; set; }
    }
}
