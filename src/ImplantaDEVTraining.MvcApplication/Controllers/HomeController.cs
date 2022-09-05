﻿using ImplantaDEVTraining.Business.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImplantaDEVTraining.MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoriasBusiness _categoriasBusiness;

        public HomeController(ICategoriasBusiness categoriasBusiness)
        {
            _categoriasBusiness = categoriasBusiness;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}