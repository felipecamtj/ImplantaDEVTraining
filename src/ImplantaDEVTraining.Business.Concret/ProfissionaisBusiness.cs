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
    public class ProfissionaisBusiness : BaseBusiness<ProfissionaisEntity, ProfissionaisFilterEntity, Profissionais>, IProfissionaisBusiness
    {
        private readonly IEnderecosBusiness _enderecosBusiness;

        public ProfissionaisBusiness(IEnderecosBusiness enderecosBusiness)
        {
            _enderecosBusiness = enderecosBusiness;
        }

        private IQueryable<Profissionais> Query(ImplantaDEVTrainingDbContext db, ProfissionaisFilterEntity filtro)
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

            return query;
        }

        public List<ProfissionaisItemListaEntity> BuscarListagem(ProfissionaisFilterEntity filtro)
        {
            var result = new List<ProfissionaisItemListaEntity>();

            using (var db = new ImplantaDEVTrainingDbContext())
            {
                var query = Query(db, filtro);

                var listagem = query.Select(q => new
                {
                    q.Id,
                    q.Nome,
                    q.Ativo,
                    q.CPF,
                    q.RG
                });

                foreach (var item in listagem)
                {
                    var entity = new ProfissionaisItemListaEntity();
                    PropertyCopy.Copy(item, entity);
                    result.Add(entity);
                }
            }

            return result;
        }

        public override List<ProfissionaisEntity> BuscarRegistros(ProfissionaisFilterEntity filtro)
        {
            var result = new List<ProfissionaisEntity>();

            using (var db = new ImplantaDEVTrainingDbContext())
            {
                var query = Query(db, filtro);

                foreach (var item in query)
                {
                    var entity = new ProfissionaisEntity();
                    PropertyCopy.Copy(item, entity);
                    result.Add(entity);
                }
            }

            if (result.Any() && filtro.CarregarEnderecos)
            {
                var idsProfissionais = result
                    .Select(r => r.Id)
                    .Distinct()
                    .ToList();

                var enderecos = BuscarEnderecos(idsProfissionais);
                result.ForEach(p => p.Enderecos = enderecos.Where(e => e.IdProfissional == p.Id).ToList());
            }

            return result;
        }

        public override ProfissionaisEntity BuscarRegistro(Guid id)
        {
            var filtro = new ProfissionaisFilterEntity
            {
                Id = id,
                CarregarEnderecos = true
            };

            return BuscarRegistros(filtro).FirstOrDefault();
        }

        private List<EnderecosEntity> BuscarEnderecos(List<Guid> idsProfissionais)
        {
            return _enderecosBusiness.BuscarRegistros(new EnderecosFilterEntity
            {
                IdsProfissionais = idsProfissionais
            });            
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

                var enderecos = operacao
                    .Entidade
                    .Where(p => p.Acao != EntityAction.Delete && p.Enderecos != null)
                    .SelectMany(p => p.Enderecos)
                    .ToList();

                var operacaoEnderecos = new Operacao<List<EnderecosEntity>>(enderecos);

                using (var transacao = new TransactionScope())
                {
                    using (var db = new ImplantaDEVTrainingDbContext())
                    {
                        operacao.AdicionarErro(SalvarListaEntidade(operacao.Entidade, db));

                        if (!operacao.Erro)
                            operacao.AdicionarErro(ValidarDuplicidade(db));
                    }

                    if (!operacao.Erro)
                    {
                        operacaoEnderecos = _enderecosBusiness.SalvarLista(operacaoEnderecos);
                        operacao.AdicionarErro(operacaoEnderecos);
                    }

                    if (!operacao.Erro)
                        transacao.Complete();
                }

                if (!operacao.Erro)
                {
                    operacao.Entidade.RemoveAll(p => p.Acao == EntityAction.Delete);                    
                    operacao.Entidade.ForEach(p =>
                    {
                        p.Acao = EntityAction.None;
                        p.Enderecos = operacaoEnderecos.Entidade.Where(e => e.IdProfissional == p.Id).ToList();
                    });
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
