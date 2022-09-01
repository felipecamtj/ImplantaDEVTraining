using ImplantaDEVTraining.Business.Contract;
using ImplantaDEVTraining.Common;
using ImplantaDEVTraining.Data;
using ImplantaDEVTraining.Entity;
using ImplantaDEVTraining.Entity.FilterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace ImplantaDEVTraining.Business.Concret
{
    public class CategoriasBusiness : BaseBusiness<CategoriasEntity, CategoriasFilterEntity, Categorias>, ICategoriasBusiness
    {
        public override List<CategoriasEntity> BuscarRegistros(CategoriasFilterEntity filtro)
        {
            var result = new List<CategoriasEntity>();

            using (var db = new ImplantaDEVTrainingDbContext())
            {
                var query = from c in db.Categorias
                            select c;

                bool filtrouPorId = false;

                if (filtro.Id.HasValue)
                {
                    query = query.Where(c => c.Id == filtro.Id);
                    filtrouPorId = true;
                }

                if (filtro.Ids != null && filtro.Ids.Any())
                {
                    query = query.Where(c => filtro.Ids.Contains(c.Id));
                    filtrouPorId = true;
                }

                if (!string.IsNullOrEmpty(filtro.Nome))
                    query = query.Where(c => c.Nome.StartsWith(filtro.Nome));

                if (filtro.SomenteAtivos && !filtrouPorId)
                    query = query.Where(c => c.Ativo);

                query = query.OrderBy(c => c.Nome);
                query = query.Skip(filtro.Skip).Take(filtro.Take);

                foreach (var item in query)
                {
                    var entity = new CategoriasEntity();
                    PropertyCopy.Copy(item, entity);

                    result.Add(entity);
                }
            }

            return result;
        }

        public override Operacao<List<CategoriasEntity>> SalvarLista(Operacao<List<CategoriasEntity>> operacao)
        {
            try
            {
                operacao = ValidarOperacao(operacao);

                if (operacao.Erro)
                    return operacao;

                operacao.Entidade.ForEach(e => operacao.AdicionarErro(ValidarEntidade(e)));

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

        private ActionReturn ValidarDuplicidade(ImplantaDEVTrainingDbContext db)
        {
            var result = new ActionReturn();

            var query = from c in db.Categorias
                        group c by c.Nome into grupo                        
                        select new
                        {
                            Nome = grupo.Key,
                            Qtd = grupo.Count()
                        };

            foreach (var categoriaDuplicada in query.Where(c => c.Qtd > 1).OrderBy(c => c.Nome))            
                result.AdicionarErro($"Categoria duplicada: {categoriaDuplicada.Nome}");

            return result;
        }

        private ActionReturn ValidarEntidade(CategoriasEntity categoria)
        {
            var result = new ActionReturn();

            if (categoria.Id == Guid.Empty)
                result.AdicionarErro("O campo Id é obrigatório");

            if (string.IsNullOrEmpty(categoria.Nome))
                result.AdicionarErro("O campo Nome é obrigatório.");

            return result;
        }
    }
}
