using ImplantaDEVTraining.Entity;
using System;
using System.Web.Mvc;

namespace ImplantaDEVTraining.MvcApplication.Controllers
{
    public class EnderecosController : Controller
    {
        private EnderecosEntity NewEntity(Guid idProfissional)
        {
            return new EnderecosEntity
            {
                Acao = Common.EntityAction.New,
                Id = Guid.NewGuid(),
                IdProfissional = idProfissional
            };
        }

        [HttpGet]
        public JsonResult BuscarNovoEndereco(Guid idProfissional)
        {
            var endereco = NewEntity(idProfissional);
            return Json(new { data = endereco }, JsonRequestBehavior.AllowGet);
        }
    }
}