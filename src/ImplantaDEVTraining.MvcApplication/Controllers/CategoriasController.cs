using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace ImplantaDEVTraining.MvcApplication.Controllers
{
    public class CategoriasController : Controller
    {
        [HttpPost]
        public string Mensagem()
        {
            return "Esta é uma mensagem de teste";
        }

        public DateTime DataAtual()
        {
            return DateTime.Now;
        }

        public int Idade()
        {
            return 30;
        }

        // GET: Categorias
        public ActionResult Index(string nome, int idade)
        {
            ViewBag.Nome = nome;
            ViewData["Idade"] = idade;
            return View();
        }
    }
}