using ImplantaDEVTraining.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ImplantaDEVTraining.Business.Concret
{
    public abstract class BaseBusiness<TEntity, TFilterEntity, TData>: IBusiness<TEntity, TFilterEntity>
        where TEntity : BaseEntity, new()
        where TFilterEntity : BaseFilterEntity, new()
        where TData: class, new()
    {
        public virtual TEntity BuscarRegistro(Guid id)
        {
            var filtro = new TFilterEntity
            {
                Id = id,
            };

            return BuscarRegistros(filtro).FirstOrDefault();
        }

        public abstract List<TEntity> BuscarRegistros(TFilterEntity filtro);

        public virtual Operacao<TEntity> Salvar(Operacao<TEntity> operacao)
        {
            var operacaoLista = new Operacao<List<TEntity>>(new List<TEntity> { operacao.Entidade });
            operacaoLista = SalvarLista(operacaoLista);
            operacao.AdicionarErro(operacaoLista);

            return operacao;
        }

        public abstract Operacao<List<TEntity>> SalvarLista(Operacao<List<TEntity>> operacao);

        protected virtual ActionReturn SalvarListaEntidade<TDb>(ICollection<TEntity> entities, TDb context)
            where TDb: DbContext
        {
            var result = new ActionReturn();

            try
            {
                entities.Where(e => e.Acao == EntityAction.Delete).ToList().ForEach(entity => 
                {
                    TData data = new TData();
                    PropertyCopy.Copy(entity, data);
                    context.Entry(data).State = EntityState.Deleted;
                });

                entities.Where(e => e.Acao == EntityAction.New).ToList().ForEach(entity =>
                {
                    TData data = new TData();
                    PropertyCopy.Copy(entity, data);
                    context.Entry(data).State = EntityState.Added;
                });

                entities.Where(e => e.Acao == EntityAction.Update).ToList().ForEach(entity =>
                {
                    TData data = new TData();
                    PropertyCopy.Copy(entity, data);
                    context.Entry(data).State = EntityState.Modified;
                });

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.AdicionarErro(ex.Message);
            }

            return result;
        }

        protected Operacao<List<TEntity>> ValidarEntidade(Operacao<List<TEntity>> operacao)
        {
            if (operacao.Entidade == null)            
                operacao.AdicionarErro("Operação inválida!");                            

            return operacao;
        }
    }
}
