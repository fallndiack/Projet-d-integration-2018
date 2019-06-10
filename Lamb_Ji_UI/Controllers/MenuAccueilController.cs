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
            list.Add(new MenuDynamique { Lien = "/Lutteur/DisplayLuteurForUser", NomLien = "Liste des Lutteurs" });
            list.Add(new MenuDynamique { Lien = "/Actualite/Index", NomLien = "Voir les Actualités" });
            list.Add(new MenuDynamique { Lien = "/Galerie/Index", NomLien = "Visiter la Galerie" });
            list.Add(new MenuDynamique { Lien = "/TypeLutte/Index", NomLien = "La Lutte au Sénégal" });



            return PartialView("MenuDynam", list);
        }
    }
}