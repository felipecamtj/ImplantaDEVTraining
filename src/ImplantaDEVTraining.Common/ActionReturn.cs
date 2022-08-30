using System.Collections.Generic;
using System.Linq;

namespace ImplantaDEVTraining.Common
{
    public class ActionReturn
    {
        private readonly List<string> _mensagens;

        public ActionReturn()
        {
            _mensagens = new List<string>();
        }

        public IReadOnlyCollection<string> Mensagens => _mensagens.ToList();

        public bool Erro => _mensagens.Any();

        public void AdicionarErro(string mensagem)
        {
            _mensagens.Add(mensagem);
        }

        public void AdicionarErro(ActionReturn actionReturn)
        {
            foreach (var msg in actionReturn.Mensagens)
                AdicionarErro(msg);
        }
    }
}
