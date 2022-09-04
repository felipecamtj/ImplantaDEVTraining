using ImplantaDEVTraining.Common;

namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public class ProfissionalByCategoriaFilterEntity : BaseFilterEntity
    {
        public string NomeCategoria { get; set; }
        public OpcaoAction ProfissionalAtivo { get; set; } = OpcaoAction.Sim;
        public OpcaoAction CategoriaAtivo { get; set; } = OpcaoAction.Sim;


    }
}
