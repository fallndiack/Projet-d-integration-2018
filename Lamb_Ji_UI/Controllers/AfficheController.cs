using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lamb_Ji_DAL;

namespace Lamb_Ji_UI.Controllers
{
    public class AfficheController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: Affiche
        public ActionResult Index()
        {
            var affiches = db.Affiches
                .Include(a => a.Lutteur)
                .Include(a => a.Lutteur1)
                .Include(a => a.Combat);
            return View(affiches.ToList());
        }

        // GET: Affiche/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Affiche affiche = db.Affiches.Find(id);
            if (affiche == null)
            {
                return HttpNotFound();
            }
            return View(affiche);
        }

        // GET: Affiche/Create
        public ActionResult Create()
        {
            ViewBag.AfficheID = new SelectList(db.AvisAffiches, "AvisAfficheID", "Auteur");
            ViewBag.Lutteur_A = new SelectList(db.Lutteurs, "LutteurID", "LutteurName");
            ViewBag.Lutteru_B = new SelectList(db.Lutteurs, "LutteurID", "LutteurName");
            ViewBag.CombatID = new SelectList(db.Combats, "CombatID", "Combat_Description");
            return View();
        }

        // POST: Affiche/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AfficheID,AfficheNom,CombatID,Lutteur_A,Lutteru_B,DateCombat,Vaincqueur,imageUrl")] Affiche affiche, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                        string extension = Path.GetExtension(ImageUpload.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                        affiche.imageUrl = fileName;
                        ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Affiche"), fileName));
                    }
                    db.Affiches.Add(affiche);
                    db.SaveChanges();

                    ViewBag.AfficheID = new SelectList(db.AvisAffiches, "AvisAfficheID", "Auteur", affiche.AfficheID);
                    ViewBag.Lutteur_A = new SelectList(db.Lutteurs, "LutteurID", "LutteurName", affiche.Lutteur_A);
                    ViewBag.Lutteru_B = new SelectList(db.Lutteurs, "LutteurID", "LutteurName", affiche.Lutteru_B);
                    ViewBag.CombatID = new SelectList(db.Combats, "CombatID", "Combat_Description", affiche.CombatID);


                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw;
                }
               
            }

            return View(affiche);
        }

        public ActionResult GetLutteurPourCombat(int combatId)
        {
            var combat = db.Combats.Where(c => c.CombatID == combatId).SingleOrDefault();
            var lutts = db.Lutteurs.Where(l => l.LutteurPoids >= combat.Categorie.Categorie_Min &&
             l.LutteurPoids <= combat.Categorie.Categorie_Max).ToList();
            
            return Json(lutts, JsonRequestBehavior.AllowGet);
        }


        // GET: Affiche/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Affiche affiche = db.Affiches.Find(id);
            if (affiche == null)
            {
                return HttpNotFound();
            }
            ViewBag.AfficheID = new SelectList(db.AvisAffiches, "AvisAfficheID", "Auteur", affiche.AfficheID);
            ViewBag.Lutteur_A = new SelectList(db.Lutteurs, "LutteurID", "LutteurName", affiche.Lutteur_A);
            ViewBag.Lutteru_B = new SelectList(db.Lutteurs, "LutteurID", "LutteurName", affiche.Lutteru_B);
            ViewBag.CombatID = new SelectList(db.Combats, "CombatID", "Combat_Description", affiche.CombatID);
            return View(affiche);
        }

        // POST: Affiche/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AfficheID,AfficheNom,CombatID,Lutteur_A,Lutteru_B,DateCombat,Vaincqueur,imageUrl")] Affiche affiche, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {

                if (ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                    string extension = Path.GetExtension(ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                    affiche.imageUrl = fileName;
                    ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Affiche"), fileName));
                }
                db.Entry(affiche).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AfficheID = new SelectList(db.AvisAffiches, "AvisAfficheID", "Auteur", affiche.AfficheID);
            ViewBag.Lutteur_A = new SelectList(db.Lutteurs, "LutteurID", "LutteurName", affiche.Lutteur_A);
            ViewBag.Lutteru_B = new SelectList(db.Lutteurs, "LutteurID", "LutteurName", affiche.Lutteru_B);
            ViewBag.CombatID = new SelectList(db.Combats, "CombatID", "Combat_Description", affiche.CombatID);
            return View(affiche);
        }

        // GET: Affiche/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Affiche affiche = db.Affiches.Find(id);
            if (affiche == null)
            {
                return HttpNotFound();
            }
            return View(affiche);
        }

        // POST: Affiche/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Affiche affiche = db.Affiches.Find(id);
            List<AvisAffiche> laa = db.AvisAffiches.Where(v => v.AfficheID == id).ToList();
            foreach (var item in laa)
            {
                db.AvisAffiches.Remove(item);
            }
          
            db.Affiches.Remove(affiche);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
