using ImplantaDEVTraining.Common;
using ImplantaDEVTraining.Data;
using ImplantaDEVTraining.Entity;
using ImplantaDEVTraining.Entity.FilterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplantaDEVTraining.Business.Concret
{
    public class CategoriasBusiness
    {
        public CategoriasEntity BuscarRegistro(Guid id)
        {
            var filtro = new CategoriasFilterEntity
            {
                Id = id
            };

            var registros = BuscarRegistros(filtro);
            return registros.FirstOrDefault();
        }

        public List<CategoriasEntity> BuscarRegistros(CategoriasFilterEntity filtro)
        {
            var result = new List<CategoriasEntity>();

            using (var db = new ImplantaDEVTrainingDbContext())
            {
                var query = from c in db.Categorias
                            select c;

                if (filtro.Id.HasValue)
                    query = query.Where(c => c.Id == filtro.Id);

                if (filtro.Ids != null && filtro.Ids.Any())
                    query = query.Where(c => filtro.Ids.Contains(c.Id));

                if (!string.IsNullOrEmpty(filtro.Nome))
                    query = query.Where(c => c.Nome.StartsWith(filtro.Nome));

                if (filtro.SomenteAtivos)
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
    }
}
