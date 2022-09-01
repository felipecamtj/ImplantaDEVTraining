using ImplantaDEVTraining.Business.Contract;
using ImplantaDEVTraining.Common;
using ImplantaDEVTraining.Data;
using ImplantaDEVTraining.Entity;
using ImplantaDEVTraining.Entity.FilterEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImplantaDEVTraining.Business.Concret
{
    public class EnderecosBusiness : BaseBusiness<EnderecosEntity, EnderecosFilterEntity, Enderecos>, IEnderecosBusiness
    {
        public override List<EnderecosEntity> BuscarRegistros(EnderecosFilterEntity filtro)
        {
            var result = new List<EnderecosEntity>();

            using (var db = new ImplantaDEVTrainingDbContext())
            {
                var query = from e in db.Enderecos
                            select e;

                if (filtro.Id.HasValue)
                    query = query.Where(e => e.Id == filtro.Id);

                if (filtro.Ids != null && filtro.Ids.Any())
                    query = query.Where(e => filtro.Ids.Contains(e.Id));

                if (filtro.IdProfissional.HasValue)
                    query = query.Where(e => e.IdProfissional == filtro.IdProfissional);

                if (filtro.IdsProfissionais != null && filtro.IdsProfissionais.Any())
                    query = query.Where(e => filtro.IdsProfissionais.Contains(e.IdProfissional));

                query = query.OrderBy(e => e.CEP);
                query = query.Skip(filtro.Skip).Take(filtro.Take);

                foreach (var enderecoDB in query)
                {
                    var entity = new EnderecosEntity();
                    PropertyCopy.Copy(enderecoDB, entity);
                    result.Add(entity);
                }
            }

            return result;
        }

        public override Operacao<List<EnderecosEntity>> SalvarLista(Operacao<List<EnderecosEntity>> operacao)
        {
            try
            {
                operacao.AdicionarErro(ValidarEntidade(operacao));

                if (operacao.Erro)
                    return operacao;

                operacao
                    .Entidade
                    .Where(e => e.Acao != EntityAction.Delete)
                    .ToList()
                    .ForEach(e => operacao.AdicionarErro(ValidarEntidade(e)));

                if (operacao.Erro)
                    return operacao;

                using (var db = new ImplantaDEVTrainingDbContext())
                {
                    operacao.AdicionarErro(SalvarListaEntidade(operacao.Entidade, db));
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

        private ActionReturn ValidarEntidade(EnderecosEntity endereco)
        {
            var result = new ActionReturn();

            if (endereco.Id == Guid.Empty)
                result.AdicionarErro("O campo Id é obrigatório.");

            if (endereco.IdProfissional == Guid.Empty)
                result.AdicionarErro("O campo IdProfissional é obrigatório.");

            if (string.IsNullOrEmpty(endereco.Logradouro))
                result.AdicionarErro("O campo Logradouro é obrigatório.");

            if (string.IsNullOrEmpty(endereco.CEP))
                result.AdicionarErro("O campo CEP é obrigatório.");

            if (string.IsNullOrEmpty(endereco.Cidade))
                result.AdicionarErro("O campo Cidade é obrigatório.");

            if (string.IsNullOrEmpty(endereco.Bairro))
                result.AdicionarErro("O campo Bairro é obrigatório.");

            return result;
        }
    }
}
