using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplantaDEVTraining.Entity
{
    public class ProfissionaisCategoriasEntity
    {
        public string NomeCategoria { get; set; }
        public List<ProfissionaisNaCategoria> Profissionais { get; set; }

        public class ProfissionaisNaCategoria
        {
            public string Nome { get; set; }
            public DateTime? DataNascimento { get; set; }
            public string CPF { get; set; }
            public string RG { get; set; }
        }

    }
}
