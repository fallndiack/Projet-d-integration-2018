using Lamb_Ji_DAL;
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
        public ActionResult AddNewClub()
        {
            return View();
        }

        public ActionResult SaveData(Club item)
        {
            if (item.ClubName !=null && item.ClubAdresse != null && item.ClubEmail != null && item.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(item.ImageUpload.FileName);
                string extension = Path.GetExtension(item.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff")+ extension;
                item.imageUrl = fileName;
                item.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Club"), fileName));
                db.Clubs.Add(item);
                db.SaveChanges();
            }
            var result = "Ajouté avec succés!!!";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}