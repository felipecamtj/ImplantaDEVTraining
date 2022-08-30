using ImplantaDEVTraining.Common;

namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public class CategoriasFilterEntity : BaseFilterEntity
    {
        public CategoriasFilterEntity()
        {
            SomenteAtivos = true;
        }
    
        public string Nome { get; set; }
        public bool SomenteAtivos { get; set; }
    }
}
