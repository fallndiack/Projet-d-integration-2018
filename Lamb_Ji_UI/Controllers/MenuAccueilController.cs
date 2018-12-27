using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Lamb_Ji_UI.Controllers
{
    public class MenuAccueilController : Controller
    {
        // GET: MenuAccueil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {

            List<MenuDynamique> list = new List<MenuDynamique>();
            list.Add(new MenuDynamique { Lien = "#", NomLien = "ACCUEIL" });
            list.Add(new MenuDynamique { Lien = "/Lutteur/DisplayLuteurForUser", NomLien = "LUTTEURS" });
            list.Add(new MenuDynamique { Lien = "#", NomLien = "COMBATS A VENIR" });
            list.Add(new MenuDynamique { Lien = "#", NomLien = "ACTUALITES" });
            list.Add(new MenuDynamique { Lien = "#",/*/Club/AddNewClub*/ NomLien = "STADES" });



            return PartialView("MenuDynam", list);
        }
    }
}