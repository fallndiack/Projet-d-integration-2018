using Lamb_Ji_DAL;
using Lamb_Ji_ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamb_Ji_UI.Controllers
{
    public class ActualiteController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();
        // GET: Actualite
        public ActionResult Index()
        {
            return View(db.Actualites.ToList());
        }

        public ActionResult SingleActuDetail(int id)
        {

            try
            {
                Actualite actu = db.Actualites.Where(s => s.actuID == id).FirstOrDefault();
                if (actu == null)
                {
                    return RedirectToAction("Index");
                }

                SingleActuViewModel act = new SingleActuViewModel();
                act.actuID = actu.actuID;
                act.actuNom = actu.actuNom;
                act.actuTitre = actu.actuTitre;
                act.actuTexte = actu.actuTexte;
                act.actuDate = actu.actuDate;
                act.actuImgUrl = actu.actuImgUrl;

                return View(act);
            }
            catch (Exception)
            {

                throw;
            }

        }

      
    }
}