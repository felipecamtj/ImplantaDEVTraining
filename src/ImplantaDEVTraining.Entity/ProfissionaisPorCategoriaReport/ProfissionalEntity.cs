using System;

namespace ImplantaDEVTraining.Entity.ProfissionaisPorCategoriaReport
{
    public class ProfissionalEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
    }
}
