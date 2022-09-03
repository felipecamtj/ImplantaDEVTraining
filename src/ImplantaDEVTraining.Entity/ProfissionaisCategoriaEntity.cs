using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplantaDEVTraining.Entity
{
    public class ProfissionalCategoriaEntity
    {
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

    }

    public class ProfissionaisCategoriaEntity
    {
        public ProfissionaisCategoriaEntity()
        {
            Profissionais = new List<ProfissionalCategoriaEntity>();
        }
        public string NomeCategoria { get; set; }
        public ICollection<ProfissionalCategoriaEntity> Profissionais { get; set; }

    }
}
