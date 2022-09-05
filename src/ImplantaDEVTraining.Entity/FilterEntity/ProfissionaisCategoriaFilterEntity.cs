using ImplantaDEVTraining.Common;

namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public class ProfissionaisCategoriaFilterEntity : BaseFilterEntity
    {
        public string NomeCategoria { get; set; }
        public bool? ProfissionaisAtivos { get; set; } = true;
        public bool? CategoriasAtivas { get; set; } = true;

    }
}
