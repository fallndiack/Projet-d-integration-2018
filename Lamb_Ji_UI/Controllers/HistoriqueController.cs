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
                List<Affiche> vma = new List<Affiche>();
                foreach (var item in db.Affiches)
                {
                    if ((item.Lutteur_A == id) || item.Lutteru_B == id)
                    {
                        vma.Add(item);
                    }
                   
                }
                    //db.Affiches.Where(i => i.Lutteur_A == id || i.Lutteru_B == id).ToList();
                ViewBag.lutteurID = id.Value;
                viewModel.affiches = vma;

                //viewModel.affiches = viewModel.lutteurs.Where(
                //    i => i.LutteurID == id.Value).Single().Affiches1.ToList();
            }
            return View(viewModel);



        }

       
    }
}