using ImplantaDEVTraining.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplantaDEVTraining.Entity.FilterEntity
{
    public enum OpcaoBoolean
    {
        Sim = 0,
        Nao = 1,    
        Todos = 2
    }
    public class ProfissionaisCategoriaFilterEntity
    {
        public string NomeCategoria { get; set; }
        public OpcaoBoolean SomenteCategoriaAtiva { get; set; } = OpcaoBoolean.Todos;
        public OpcaoBoolean SomenteProfissionaisAtivo { get; set; } = OpcaoBoolean.Todos;
    }
}
