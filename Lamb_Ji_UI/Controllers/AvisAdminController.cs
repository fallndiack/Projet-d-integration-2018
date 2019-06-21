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
    [Authorize(Roles = "Admin")]
    public class AvisAdminController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: AvisAdmin
        public ActionResult Index()
        {
            var avisAffiches = db.AvisAffiches.Include(a => a.Affiche);
            return View(avisAffiches.ToList());
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
