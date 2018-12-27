using Lamb_Ji_DAL;
using Lamb_Ji_ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamb_Ji_UI.Controllers
{
    public class ArbitreController : Controller
    {
        CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: Arbitre
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetArbitreList()
        {
            List<Arbitre> ArbitresDb = db.Arbitres.ToList();
            List<ArbitreViewModel> ArbitresVM = new List<ArbitreViewModel>();
            foreach (var item in ArbitresDb)
            {
                ArbitreViewModel Avm = new ArbitreViewModel();
                Avm.ArbitreName = item.ArbitreName;
                Avm.ArbitreID = item.ArbitreID;
                Avm.ArbitreEmail = item.ArbitreEmail;
                Avm.ArbitreDateNaissance = item.ArbitreDateNaissance;
                Avm.imageUrl = item.imageUrl;
                

                ArbitresVM.Add(Avm);
            }

            return Json(ArbitresVM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(ArbitreViewModel model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                try
                {

                    if (model.ArbitreID > 0)
                    {
                        Arbitre arbitre = db.Arbitres.SingleOrDefault(x => x.ArbitreID == model.ArbitreID);
                        arbitre.ArbitreName = model.ArbitreName;
                        arbitre.ArbitreEmail = model.ArbitreEmail;
                        arbitre.ArbitreDateNaissance = model.ArbitreDateNaissance;
                        arbitre.imageUrl = model.imageUrl;


                        db.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        if (model.ImageUpload != null)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                            string extension = Path.GetExtension(model.ImageUpload.FileName);
                            fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                            model.imageUrl = fileName;
                            model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Arbitre"), fileName));
                        }

                        Arbitre arbt = new Arbitre();
                        arbt.ArbitreName = model.ArbitreName;
                        arbt.ArbitreEmail = model.ArbitreEmail;
                        arbt.ArbitreDateNaissance = model.ArbitreDateNaissance;
                        arbt.imageUrl = model.imageUrl;

                        db.Arbitres.Add(arbt);
                        db.SaveChanges();
                        result = true;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetArbitreById(int ArbitreID)
        {
            Arbitre model = db.Arbitres.Where(x => x.ArbitreID == ArbitreID).FirstOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteArbitreRecord(int ArbitreID)
        {
            bool result = false;
            Arbitre Arbt = db.Arbitres.SingleOrDefault(x => x.ArbitreID == ArbitreID);
            if (Arbt != null)
                {
                    db.Arbitres.Remove(Arbt);
                    db.SaveChanges();
                    result = true;
                }
            
           

            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}