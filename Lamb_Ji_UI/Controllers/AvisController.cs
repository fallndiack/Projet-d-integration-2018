using Lamb_Ji_DAL;
using Lamb_Ji_ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamb_Ji_UI.Controllers
{
    public class AvisController : Controller
    {
        // GET: Avis
        public ActionResult LaisserUnAvis(int afficheID)
        {

            LaisserUnAvisViewModel vm = new LaisserUnAvisViewModel(); 
             
            using (var context = new CNGLUTTEDBEntities())
            {
                Affiche aff = context.Affiches.Where(a => a.AfficheID == afficheID).SingleOrDefault();
                if (aff == null)               
                    return RedirectToAction("Index", "AfficheUser");
                vm.AfficheID = aff.AfficheID;
                vm.NomAffiche = aff.AfficheNom;
            }

                return View(vm);
        }
        [HttpPost]
        public ActionResult SaveComment(string nom, string commentaire, string note, int afficheID)
        {
            AvisAffiche nouvelAvis = new AvisAffiche();
            
            nouvelAvis.DateAvis = DateTime.Now;
            nouvelAvis.Message = commentaire;
            nouvelAvis.Auteur = nom;
            double dnote = 0;
            if (!double.TryParse(note,NumberStyles.Any,CultureInfo.InvariantCulture, out dnote))
            {
                throw new Exception("Impossible de parser la note"+note);
            }

            nouvelAvis.note = dnote;
            using (var context = new CNGLUTTEDBEntities())
            {
                Affiche aff = context.Affiches.Where(a => a.AfficheID == afficheID).SingleOrDefault();
                if (aff == null)
                {
                    return RedirectToAction("Index", "AfficheUser");
                }
                //var p = context.AvisAffiches.ToList();
                //var maxId = p.Max(x => x.AvisAfficheID);
                //nouvelAvis.AvisAfficheID = maxId + 1;
                nouvelAvis.AfficheID = aff.AfficheID;
                context.AvisAffiches.Add(nouvelAvis);
                context.SaveChanges();
            }
            return RedirectToAction("DetailsAfficheAvecAvis", "AfficheUser", new {id = afficheID });
        }
    }
}