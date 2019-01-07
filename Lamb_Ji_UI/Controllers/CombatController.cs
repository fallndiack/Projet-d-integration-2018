using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lamb_Ji_DAL;

namespace Lamb_Ji_UI.Controllers
{
    public class CombatController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: Combat
        public ActionResult Index()
        {
            var combats = db.Combats.Include(c => c.Categorie).Include(c => c.Stade).Include(c => c.TypeLutte);
            return View(combats.ToList());
        }

        // GET: Combat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Combat combat = db.Combats.Find(id);
            if (combat == null)
            {
                return HttpNotFound();
            }
            return View(combat);
        }

        // GET: Combat/Create
        public ActionResult Create()
        {
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Categorie_Libele");
            ViewBag.StadeID = new SelectList(db.Stades, "StadeID", "StadeName");
            ViewBag.TypeLutteID = new SelectList(db.TypeLuttes, "TypeLutteID", "TypeLutte_Libele");
            return View();
        }

        // POST: Combat/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CombatID,Combat_Description,TypeLutteID,CategorieID,StadeID,Combat_Etat")] Combat combat)
        {
            if (ModelState.IsValid)
            {
                db.Combats.Add(combat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Categorie_Libele", combat.CategorieID);
            ViewBag.StadeID = new SelectList(db.Stades, "StadeID", "StadeName", combat.StadeID);
            ViewBag.TypeLutteID = new SelectList(db.TypeLuttes, "TypeLutteID", "TypeLutte_Libele", combat.TypeLutteID);
            return View(combat);
        }

        // GET: Combat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Combat combat = db.Combats.Find(id);
            if (combat == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Categorie_Libele", combat.CategorieID);
            ViewBag.StadeID = new SelectList(db.Stades, "StadeID", "StadeName", combat.StadeID);
            ViewBag.TypeLutteID = new SelectList(db.TypeLuttes, "TypeLutteID", "TypeLutte_Libele", combat.TypeLutteID);
            return View(combat);
        }

        // POST: Combat/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CombatID,Combat_Description,TypeLutteID,CategorieID,StadeID,Combat_Etat")] Combat combat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(combat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Categorie_Libele", combat.CategorieID);
            ViewBag.StadeID = new SelectList(db.Stades, "StadeID", "StadeName", combat.StadeID);
            ViewBag.TypeLutteID = new SelectList(db.TypeLuttes, "TypeLutteID", "TypeLutte_Libele", combat.TypeLutteID);
            return View(combat);
        }

        // GET: Combat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Combat combat = db.Combats.Find(id);
            if (combat == null)
            {
                return HttpNotFound();
            }
            return View(combat);
        }

        // POST: Combat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Combat combat = db.Combats.Find(id);
            db.Combats.Remove(combat);
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
