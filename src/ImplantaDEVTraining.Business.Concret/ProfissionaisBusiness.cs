using ImplantaDEVTraining.Business.Contract;
using ImplantaDEVTraining.Common;
using ImplantaDEVTraining.Data;
using ImplantaDEVTraining.Entity;
using ImplantaDEVTraining.Entity.FilterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ImplantaDEVTraining.Business.Concret
{
    public class ProfissionaisBusiness : BaseBusiness<ProfissionaisEntity, ProfissionaisFilterEntity, Profissionais>, IProfissionaisBusiness
    {
        public override List<ProfissionaisEntity> BuscarRegistros(ProfissionaisFilterEntity filtro)
        {
            var result = new List<ProfissionaisEntity>();

            using (var db = new ImplantaDEVTrainingDbContext())
            {
                var query = from p in db.Profissionais
                            select p;

                bool filtrouPorId = false;

                if (filtro.Id.HasValue)
                {
                    query = query.Where(p => p.Id == filtro.Id);
                    filtrouPorId = true;
                }

                if (filtro.Ids != null && filtro.Ids.Any())
                {
                    query = query.Where(p => filtro.Ids.Contains(p.Id));
                    filtrouPorId = true;
                }

                if (filtro.IdCategoria.HasValue)
                    query = query.Where(p => p.Id == filtro.Id);

                if (!string.IsNullOrEmpty(filtro.Nome))
                    query = query.Where(p => p.Nome.StartsWith(filtro.Nome));

                if (filtro.SomenteAtivos && !filtrouPorId)
                    query = query.Where(p => p.Ativo);

                query = query.OrderBy(p => p.Nome);
                query = query.Skip(filtro.Skip).Take(filtro.Take);

                foreach (var item in query)
                {
                    var entity = new ProfissionaisEntity();
                    PropertyCopy.Copy(item, entity);
                    result.Add(entity);
                }
            }

            return result;
        }

        public override Operacao<List<ProfissionaisEntity>> SalvarLista(Operacao<List<ProfissionaisEntity>> operacao)
        {
            try
            {
                operacao = ValidarEntidade(operacao);

                if (operacao.Erro)
                    return operacao;

                operacao
                    .Entidade
                    .Where(e => e.Acao != EntityAction.Delete)
                    .ToList()
                    .ForEach(e => operacao.AdicionarErro(ValidarEntidade(e)));

                if (operacao.Erro)
                    return operacao;

                using (var transacao = new TransactionScope())
                {
                    using (var db = new ImplantaDEVTrainingDbContext())
                    {
                        operacao.AdicionarErro(SalvarListaEntidade(operacao.Entidade, db));

                        if (!operacao.Erro)
                            operacao.AdicionarErro(ValidarDuplicidade(db));
                    }

                    if (!operacao.Erro)
                        transacao.Complete();
                }

                if (!operacao.Erro)
                {
                    operacao.Entidade.RemoveAll(e => e.Acao == EntityAction.Delete);
                    operacao.Entidade.ForEach(e => e.Acao = EntityAction.None);
                }
            }
            catch (Exception ex)
            {
                operacao.AdicionarErro(ex.Message);
            }

            return operacao;
        }

        private ActionReturn ValidarEntidade(ProfissionaisEntity profissional)
        {
            var result = new ActionReturn();

            if (profissional.Id == Guid.Empty)
                result.AdicionarErro("O campo Id é obrigatório.");

            if (profissional.IdCategoria == Guid.Empty)
                result.AdicionarErro("O campo IdCategoria é obrigatório.");

            if (string.IsNullOrEmpty(profissional.Nome))
                result.AdicionarErro("O campo Nome é obrigatório.");

            return result;
        }

        private ActionReturn ValidarDuplicidade(ImplantaDEVTrainingDbContext db)
        {
            var result = new ActionReturn();

            var duplicados = from p in db.Profissionais
                             group p by p.Nome into grupo
                             where grupo.Count() > 1
                             select grupo.Key;

            foreach (var duplicado in duplicados.OrderBy(d => d))
                result.AdicionarErro($"Nome duplicado: {duplicado}");

            return result;
        }
    }
}
