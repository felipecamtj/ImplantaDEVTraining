using ImplantaDEVTraining.Common;

namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public class BuscarProfissionaisPorCategoriaFilterEntity
    {
        public string NomeCategoria { get; set; }
        public BooleanOption CategoriaAtiva { get; set; } = BooleanOption.All;
        public BooleanOption ProfissionalAtivo { get; set; } = BooleanOption.All;
    }
}
