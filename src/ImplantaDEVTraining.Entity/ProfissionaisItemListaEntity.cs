using System;

namespace ImplantaDEVTraining.Entity
{
    public class ProfissionaisItemListaEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
    }
}
