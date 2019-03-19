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
    public class AfficheUserController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: AfficheUser
        public ActionResult Index()
        {
            var affiches = db.Affiches.Include(a => a.AvisAffiche).Include(a => a.Lutteur).Include(a => a.Lutteur1).Include(a => a.Combat);
            return View(affiches.ToList());
        }

        // GET: AfficheUser/Details/5
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

      
    }
}
