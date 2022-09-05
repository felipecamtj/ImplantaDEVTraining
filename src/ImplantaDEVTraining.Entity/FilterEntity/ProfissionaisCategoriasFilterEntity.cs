using ImplantaDEVTraining.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public class ProfissionaisCategoriasFilterEntity:BaseFilterEntity
    {
        public string NomeCategoria { get; set; }
        public bool? SomenteCategoriasAtivas { get; set; } = true;
        public bool? SomenteProfissionaisAtivos { get; set; } = true;

    }
}
