namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public class ProfissionaisCategoriasFilterEntity
    {
        public string NomeCategoria {get; set;}
        public bool? CategoriaAtivo {get; set;} = true;
        public bool? ProfissionalAtivo { get; set;} = true;

    }
}
