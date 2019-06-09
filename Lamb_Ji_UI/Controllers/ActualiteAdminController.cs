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
    public class ActualiteAdminController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: ActualiteAdmin
        public ActionResult Index()
        {
            return View(db.Actualites.ToList());
        }

        // GET: ActualiteAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actualite actualite = db.Actualites.Find(id);
            if (actualite == null)
            {
                return HttpNotFound();
            }
            return View(actualite);
        }

        // GET: ActualiteAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActualiteAdmin/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "actuID,actuNom,actuTitre,actuDate,actuTexte,actuImgUrl")] Actualite actualite, HttpPostedFileBase ImageUpload)
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
                        actualite.actuImgUrl = fileName;
                        ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Actu"), fileName));
                    }
                    actualite.actuDate = DateTime.Now;
                    db.Actualites.Add(actualite);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw;
                }
               
            }

            return View(actualite);
        }

        // GET: ActualiteAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actualite actualite = db.Actualites.Find(id);
            if (actualite == null)
            {
                return RedirectToAction("Index");
            }
            return View(actualite);
        }

        // POST: ActualiteAdmin/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "actuID,actuNom,actuTitre,actuDate,actuTexte,actuImgUrl")] Actualite actualite, HttpPostedFileBase ImageUpload)
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
                        actualite.actuImgUrl = fileName;
                        ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Actu"), fileName));
                    }
                    actualite.actuDate = DateTime.Now;
                    db.Entry(actualite).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    return RedirectToAction("Index", "Error");
                }
               
            }
            return View(actualite);
        }

        // GET: ActualiteAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Actualite actualite = db.Actualites.Find(id);
            if (actualite == null)
            {
                return HttpNotFound();
            }
            return View(actualite);
        }

        // POST: ActualiteAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actualite actualite = db.Actualites.Find(id);
            db.Actualites.Remove(actualite);
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
