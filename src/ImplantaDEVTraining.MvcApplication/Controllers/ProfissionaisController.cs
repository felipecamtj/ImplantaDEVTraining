using ImplantaDEVTraining.Business.Contract;
using ImplantaDEVTraining.Entity;
using ImplantaDEVTraining.Entity.FilterEntity;
using System.Web.Mvc;

namespace ImplantaDEVTraining.MvcApplication.Controllers
{
    public class ProfissionaisController : BaseController<IProfissionaisBusiness, ProfissionaisEntity, ProfissionaisFilterEntity>
    {
        public ProfissionaisController(IProfissionaisBusiness business) 
            : base(business)
        {

        }

        [HttpGet]
        public JsonResult BuscarListagem(ProfissionaisFilterEntity filtro)
        {
            var registros = _business.BuscarListagem(filtro);
            return Json(new { data = registros }, JsonRequestBehavior.AllowGet);
        }
    }
}