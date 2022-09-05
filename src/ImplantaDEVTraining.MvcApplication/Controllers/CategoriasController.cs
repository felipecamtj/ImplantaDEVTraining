using ImplantaDEVTraining.Business.Contract;
using ImplantaDEVTraining.Entity;
using ImplantaDEVTraining.Entity.FilterEntity;

namespace ImplantaDEVTraining.MvcApplication.Controllers
{
    public class CategoriasController : BaseController<ICategoriasBusiness, CategoriasEntity, CategoriasFilterEntity>
    {
        public CategoriasController(ICategoriasBusiness business) 
            : base(business)
        {

        }
    }
}