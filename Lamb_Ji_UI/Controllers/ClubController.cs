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
    public class ClubController : Controller
    {
        CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetClubList()
        {
            List<Club> ClubsDb = db.Clubs.ToList();
            List<ClubViewModel> ClubsVM = new List<ClubViewModel>();
            foreach (var item in ClubsDb)
            {
                ClubViewModel clubvm = new ClubViewModel();
                clubvm.ClubID = item.ClubID;
                clubvm.ClubName = item.ClubName;
                clubvm.ClubAdresse = item.ClubAdresse;
                clubvm.ClubEmail = item.ClubEmail;
                clubvm.ClubDateCreation = item.ClubDateCreation;
                clubvm.imageUrl = item.imageUrl;



                ClubsVM.Add(clubvm);
            }

            return Json(ClubsVM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(ClubViewModel model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                try
                {

                    if (model.ClubID > 0)
                    {
                        Club club = db.Clubs.SingleOrDefault(x => x.ClubID == model.ClubID);
                        club.ClubName = model.ClubName;
                        club.ClubAdresse = model.ClubAdresse;
                        club.ClubEmail = model.ClubEmail;
                        club.ClubDateCreation = model.ClubDateCreation;
                        club.imageUrl = model.imageUrl;


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
                            model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Club"), fileName));
                        }

                        Club club = new Club();
                        club.ClubName = model.ClubName;
                        club.ClubAdresse = model.ClubAdresse;
                        club.ClubEmail = model.ClubEmail;
                        club.ClubDateCreation = model.ClubDateCreation;
                        club.imageUrl = model.imageUrl;

                        db.Clubs.Add(club);
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

        public JsonResult GetClubById(int ClubID)
        {
            Club model = db.Clubs.Where(x => x.ClubID == ClubID).FirstOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteClubRecord(int ClubID)
        {
            bool result = false;
            Club club = db.Clubs.SingleOrDefault(x => x.ClubID == ClubID);
            if (club != null)
            {
                db.Clubs.Remove(club);
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}