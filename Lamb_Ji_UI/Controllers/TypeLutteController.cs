﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamb_Ji_UI.Controllers
{
    public class TypeLutteController : Controller
    {
        // GET: TypeLutte
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LutteTraditionnelleSimple()
        {
            return View();
        }

        public ActionResult LutteTraditionnelleAvecFrappe()
        {
            return View();
        }
        public ActionResult LutteGrecoRomaine()
        {
            return View();
        }
    }
}