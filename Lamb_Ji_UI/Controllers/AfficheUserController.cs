using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lamb_Ji_DAL;
using Lamb_Ji_ViewModel;

namespace Lamb_Ji_UI.Controllers
{
    public class AfficheUserController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: AfficheUser
        public ActionResult Index()
        {
            var affiches = db.Affiches.Include(a => a.Lutteur).Include(a => a.Lutteur1).Include(a => a.Combat);
            return View(affiches.ToList());
        }

        // GET: AfficheUser/Details/5
        public ActionResult DetailsAfficheAvecAvis(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Affiche affiche = db.Affiches
                .Include(a => a.Combat)
                .Include(a => a.Lutteur)
                .Include(a => a.Lutteur1)               
                .Where(s => s.AfficheID == id).FirstOrDefault();
            List<AvisAffiche> listAvis = db.AvisAffiches.Where(c => c.AfficheID == id).ToList();
            if (affiche == null)
            {
                return HttpNotFound();
            }
            
            AfficheAvecAvis vm = new AfficheAvecAvis();
            vm.AfficheID = affiche.AfficheID;
            vm.AfficheNom = affiche.AfficheNom;
            vm.Combat = affiche.Combat;
            vm.Lutteur = affiche.Lutteur;
            vm.Lutteur1 = affiche.Lutteur1;
            vm.DateCombat = affiche.DateCombat;
            vm.Vaincqueur = affiche.Vaincqueur;
            vm.imageUrl = affiche.imageUrl;
           
            vm.Avis = listAvis;

            return View(vm);


        }

      
    }
}
