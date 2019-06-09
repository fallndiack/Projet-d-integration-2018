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
            try
            {
                var affiches = db.Affiches
             .Include(a => a.Lutteur)
             .Include(a => a.Lutteur1)
             .Include(a => a.Combat)
             .Include(a => a.AvisAffiches).Where(x => x.DateCombat > DateTime.Now).OrderBy(c => c.DateCombat);


                foreach (var aff in affiches)
                {
                    List<AvisAffiche> listAvis = db.AvisAffiches.Where(c => c.AfficheID == aff.AfficheID).ToList();
                    aff.AvisAffiches = listAvis;
                    if (aff.AvisAffiches.Count == 0)
                    {

                        aff.Note = 0;
                    }

                    else
                        aff.Note = Math.Round(aff.AvisAffiches.Average(a => a.note), 2);
                }
                return View(affiches.ToList());
            }
            catch (Exception)
            {

                throw;
            }
         

            
        }

        // GET: AfficheUser/Details/5
        public ActionResult DetailsAfficheAvecAvis(int id)
        {
          
            try
            {
                Affiche affiche = db.Affiches
              .Include(a => a.Combat)
              .Include(a => a.Lutteur)
              .Include(a => a.Lutteur1)
              .Where(s => s.AfficheID == id).FirstOrDefault();
                List<AvisAffiche> listAvis = db.AvisAffiches.Where(c => c.AfficheID == id).ToList();
                if (affiche == null)
                {
                    return RedirectToAction("Index");
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
                if (listAvis.Count != 0)
                {
                    vm.Avis = listAvis;
                }

                return View(vm);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Error");
            }

          
           

           


        }

      
    }
}
