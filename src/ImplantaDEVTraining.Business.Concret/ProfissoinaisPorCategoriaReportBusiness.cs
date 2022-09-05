using ImplantaDEVTraining.Data;
using ImplantaDEVTraining.Entity.FilterEntity;
using ImplantaDEVTraining.Entity.ProfissionaisPorCategoriaReport;
using System.Collections.Generic;
using System.Linq;

namespace ImplantaDEVTraining.Business.Concret
{
    public class ProfissoinaisPorCategoriaReportBusiness
    {
        public List<CategoriaEntity> BuscarProfissionaisPorCategoria(BuscarProfissionaisPorCategoriaFilterEntity filtro)
        {
            using (var db = new ImplantaDEVTrainingDbContext())
            {
                var query = from c in db.Categorias
                            join prof in db.Profissionais on c.Id equals prof.IdCategoria into pLeftJoin
                            from p in pLeftJoin.DefaultIfEmpty()
                            select new
                            {
                                IdCategoria = c.Id,
                                NomeCategoria = c.Nome,
                                CategoriaAtiva = c.Ativo,
                                IdProfissional = p.Id,
                                NomeProfissional = p.Nome,
                                ProfissionalAtivo = p.Ativo,
                                p.DataNascimento,
                                p.RG,
                                p.CPF
                            };

                if (!string.IsNullOrEmpty(filtro.NomeCategoria))
                    query = query.Where(c => c.NomeCategoria.Contains(filtro.NomeCategoria));

                if (filtro.CategoriaAtiva != Common.BooleanOption.All)
                {
                    bool categoriaAtiva = filtro.CategoriaAtiva == Common.BooleanOption.True;
                    query = query.Where(c => c.CategoriaAtiva == categoriaAtiva);
                }

                if (filtro.ProfissionalAtivo != Common.BooleanOption.All)
                {
                    bool profissionalAtivo = filtro.ProfissionalAtivo == Common.BooleanOption.True;
                    query = query.Where(c => c.ProfissionalAtivo == profissionalAtivo);
                }

                var data = query.ToList();

                var queryAgrupada = from c in data
                                    group c by new { c.IdCategoria, c.NomeCategoria } into grupo
                                    orderby grupo.Key.NomeCategoria
                                    select new CategoriaEntity
                                    {
                                        Id = grupo.Key.IdCategoria,
                                        Nome = grupo.Key.NomeCategoria,
                                        Profissionais = grupo.Select(p => new ProfissionalEntity
                                        { 
                                            Id = p.IdProfissional,
                                            Nome = p.NomeProfissional,
                                            DataNascimento = p.DataNascimento,
                                            CPF = p.CPF,
                                            RG = p.RG
                                        }).OrderBy(p => p.Nome).ToList()
                                    };

                return queryAgrupada.ToList();
            }
        }
    }
}
