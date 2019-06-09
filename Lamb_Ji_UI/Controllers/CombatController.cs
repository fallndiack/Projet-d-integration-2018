using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lamb_Ji_DAL;
using Lamb_Ji_ViewModel;

namespace Lamb_Ji_UI.Controllers
{
    public class CombatController : Controller
    {
        private CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: Combat
        public ActionResult Index(int? id)
        {
            var viewModel = new CombatIndexData();
            viewModel.combats = db.Combats
                .Include(c => c.Categorie)
                .Include(c => c.Stade)
                .Include(c => c.TypeLutte)
                .Include(c => c.Arbitres);
            if (id != null)
            {
                ViewBag.CombatID = id.Value;
                viewModel.arbitres = viewModel.combats.Where(
                    i => i.CombatID == id.Value).Single().Arbitres;
            }
            
            return View(viewModel);
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
            var combat = new Combat();
            combat.Arbitres = new List<Arbitre>();
            PopulateAssignedArbitre(combat);
            
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
        public ActionResult Create([Bind(Include = "CombatID,Combat_Description,TypeLutteID,CategorieID,StadeID,Combat_Etat")] Combat combat, string[] selectedArbitres)
        {

            if (selectedArbitres != null)
            {
                combat.Arbitres = new List<Arbitre>();
                foreach (var arbitre in selectedArbitres)
                {
                    var arbitreeToAdd = db.Arbitres.Find(int.Parse(arbitre));
                    combat.Arbitres.Add(arbitreeToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                db.Combats.Add(combat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Categorie_Libele", combat.CategorieID);
            ViewBag.StadeID = new SelectList(db.Stades, "StadeID", "StadeName", combat.StadeID);
            ViewBag.TypeLutteID = new SelectList(db.TypeLuttes, "TypeLutteID", "TypeLutte_Libele", combat.TypeLutteID);

            PopulateAssignedArbitre(combat);
            return View(combat);
        }

        // GET: Combat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Combat combat = db.Combats
                .Include(i => i.Arbitres)
                .Where(i => i.CombatID == id)
                .Single();
            PopulateAssignedArbitre(combat);
            if (combat == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Categorie_Libele", combat.CategorieID);
            ViewBag.StadeID = new SelectList(db.Stades, "StadeID", "StadeName", combat.StadeID);
            ViewBag.TypeLutteID = new SelectList(db.TypeLuttes, "TypeLutteID", "TypeLutte_Libele", combat.TypeLutteID);
            return View(combat);
        }

        private void PopulateAssignedArbitre(Combat combat)
        {
            var allArbitres = db.Arbitres;
            var CombatArbitre = new HashSet<int>(combat.Arbitres.Select(c => c.ArbitreID));
            var viewModel = new List<AssignerArbitreAuCombat>();
            foreach (var arbitre in allArbitres)
            {
                viewModel.Add(new AssignerArbitreAuCombat
                {
                    ArbitreID = arbitre.ArbitreID,
                    ArbitreName = arbitre.ArbitreName,
                    Assigned = CombatArbitre.Contains(arbitre.ArbitreID)
                });
            }
            ViewBag.Arbitres = viewModel;
        }

        // POST: Combat/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedArbitres)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }
            Combat combatToUpdate = db.Combats
                .Include(i => i.Arbitres)
                .Where(i => i.CombatID == id)
                .Single();
            if (TryUpdateModel(combatToUpdate, "",
            new string[] { "Combat_Description", "TypeLutteID", "CategorieID", "StadeID", "Combat_Etat" }))
            {
                try
            {
                UpdateCombatArbitres(selectedArbitres, combatToUpdate);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Categorie_Libele", combatToUpdate.CategorieID);
            ViewBag.StadeID = new SelectList(db.Stades, "StadeID", "StadeName", combatToUpdate.StadeID);
            ViewBag.TypeLutteID = new SelectList(db.TypeLuttes, "TypeLutteID", "TypeLutte_Libele", combatToUpdate.TypeLutteID);
            }
            PopulateAssignedArbitre(combatToUpdate);
            return View(combatToUpdate);
        
        }

        private void UpdateCombatArbitres(string[] selectedArbitress, Combat combatToUpdate)
        {
            if (selectedArbitress == null)
            {
                combatToUpdate.Arbitres = new List<Arbitre>();
                return;
            }

            var selectedArbitresHS = new HashSet<string>(selectedArbitress);
            var CombatArbitres = new HashSet<int>
                (combatToUpdate.Arbitres.Select(c => c.ArbitreID));
            foreach (var arbitre in db.Arbitres)
            {
                if (selectedArbitresHS.Contains(arbitre.ArbitreID.ToString()))
                {
                    if (!CombatArbitres.Contains(arbitre.ArbitreID))
                    {
                        combatToUpdate.Arbitres.Add(arbitre);
                    }
                }
                else
                {
                    if (CombatArbitres.Contains(arbitre.ArbitreID))
                    {
                        combatToUpdate.Arbitres.Remove(arbitre);
                    }
                }
            }
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
            Combat combat = db.Combats.Include(i => i.Arbitres).Where(i => i.CombatID == id).Single();
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
