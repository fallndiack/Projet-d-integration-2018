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
    public class AvisAdminController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: AvisAdmin
        public ActionResult Index()
        {
            var avisAffiches = db.AvisAffiches.Include(a => a.Affiche);
            return View(avisAffiches.ToList());
        }

       
       

        // GET: AvisAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvisAffiche avisAffiche = db.AvisAffiches.Find(id);
            if (avisAffiche == null)
            {
                return HttpNotFound();
            }
            ViewBag.AfficheID = new SelectList(db.Affiches, "AfficheID", "AfficheNom", avisAffiche.AfficheID);
            return View(avisAffiche);
        }

        // POST: AvisAdmin/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AvisAfficheID,Auteur,Message,note,DateAvis,AfficheID")] AvisAffiche avisAffiche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avisAffiche).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AfficheID = new SelectList(db.Affiches, "AfficheID", "AfficheNom", avisAffiche.AfficheID);
            return View(avisAffiche);
        }

        // GET: AvisAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvisAffiche avisAffiche = db.AvisAffiches.Find(id);
            if (avisAffiche == null)
            {
                return HttpNotFound();
            }
            return View(avisAffiche);
        }

        // POST: AvisAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AvisAffiche avisAffiche = db.AvisAffiches.Find(id);
            db.AvisAffiches.Remove(avisAffiche);
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
