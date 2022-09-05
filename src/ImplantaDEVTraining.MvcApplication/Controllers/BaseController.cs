using ImplantaDEVTraining.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ImplantaDEVTraining.MvcApplication.Controllers
{
    public abstract class BaseController<TBusiness, TEntity, TFilter>: Controller
        where TBusiness: IBusiness<TEntity, TFilter>
        where TEntity: BaseEntity, new()
        where TFilter: BaseFilterEntity, new()
    {
        protected readonly TBusiness _business;

        protected BaseController(TBusiness business)
        {
            _business = business;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Editar(Guid? id)
        {
            ViewData["id"] = id;
            return View();
        }

        [HttpGet]
        public JsonResult BuscarFiltro()
        {
            var filtro = new TFilter
            {
                Ids = new List<Guid>()
            };

            return Json( new { data = filtro }, JsonRequestBehavior.AllowGet);            
        }

        [HttpPost]
        public JsonResult Salvar(TEntity entity)
        {
            if (entity.Acao == EntityAction.None)
                entity.Acao = EntityAction.Update;

            var operacao = new Operacao<TEntity>(entity);
            operacao = _business.Salvar(operacao);

            return Json( new { data = operacao }, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public JsonResult BuscarRegistros(TFilter filtro)
        {
            var registros = _business.BuscarRegistros(filtro);
            return Json( new { data = registros }, JsonRequestBehavior.AllowGet);
        }

        protected virtual TEntity NewEntity()
        {
            return new TEntity
            {
                Acao = EntityAction.New,
                Id = Guid.NewGuid()
            };
        }

        [HttpGet]
        public JsonResult BuscarRegistro(Guid? id)
        {
            var registro = id.HasValue
                ? _business.BuscarRegistro(id.Value)
                : NewEntity();

            return Json(new { data = registro }, JsonRequestBehavior.AllowGet);
        }
    }
}