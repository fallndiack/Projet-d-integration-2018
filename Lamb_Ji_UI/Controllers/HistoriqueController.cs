using Lamb_Ji_DAL;
using Lamb_Ji_ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamb_Ji_UI.Controllers
{
    public class HistoriqueController : Controller
    {
        CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();
        // GET: Historique
        public ActionResult Index(int? id)
        {
            var viewModel = new HistoriqueIndexData();
            viewModel.lutteurs = db.Lutteurs;
                
            if (id != null)
            {
                ViewBag.lutteurID = id.Value;
                viewModel.affiches = viewModel.lutteurs.Where(
                    i => i.LutteurID == id.Value).Single().Affiches1;
            }

            return View(viewModel);
            
        }

       
    }
}