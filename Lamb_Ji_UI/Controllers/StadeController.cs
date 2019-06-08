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
    public class StadeController : Controller
    {
        CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        // GET: Arbitre
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStadeList()
        {
            List<Stade> StadesDb = db.Stades.ToList();
            List<StadeViewModel> StadesVM = new List<StadeViewModel>();
            foreach (var item in StadesDb)
            {
                StadeViewModel stadevm = new StadeViewModel();
                stadevm.StadeID = item.StadeID;
                stadevm.StadeName = item.StadeName;
                stadevm.StadeAdresse = item.StadeAdresse;
                stadevm.imageUrl = item.imageUrl;
               


                StadesVM.Add(stadevm);
            }

            return Json(StadesVM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(StadeViewModel model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                try
                {

                    if (model.StadeID > 0)
                    {
                        Stade stade = db.Stades.SingleOrDefault(x => x.StadeID == model.StadeID);
                        stade.StadeName = model.StadeName;
                        stade.StadeAdresse = model.StadeAdresse;
                        stade.imageUrl = model.imageUrl;


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
                            model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Stade"), fileName));
                        }

                        Stade stade = new Stade();
                        stade.StadeName = model.StadeName;
                        stade.StadeAdresse = model.StadeAdresse;
                        stade.imageUrl = model.imageUrl;

                        db.Stades.Add(stade);
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

        public JsonResult GetStadeById(int StadeID)
        {
            Stade model = db.Stades.Where(x => x.StadeID == StadeID).FirstOrDefault();
            StadeViewModel stadevm = new StadeViewModel();
            stadevm.StadeID = model.StadeID;
            stadevm.StadeName = model.StadeName;
            stadevm.StadeAdresse = model.StadeAdresse;
            stadevm.imageUrl = model.imageUrl;
            return Json(stadevm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStadeRecord(int StadeID)
        {
            bool result = false;
            Stade stade = db.Stades.SingleOrDefault(x => x.StadeID == StadeID);
            if (stade != null)
            {
                db.Stades.Remove(stade);
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}