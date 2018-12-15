using Lamb_Ji_DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamb_Ji_UI.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        public ActionResult SliderAccueil()
        {
            CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();
            var data = (from d in db.Images select d).ToList();
            return PartialView("SliderAccueil",data);
        }
        [HttpGet]
        public ActionResult AddImage()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddImage(Image dto)
        {
            string fileName = Path.GetFileNameWithoutExtension(dto.ImageFile.FileName);
            string fileExtension = Path.GetExtension(dto.ImageFile.FileName);
            string fullName = fileName + fileExtension;
            dto.ImagePath = "~/Images/" + fullName;
            fullName = Path.Combine(Server.MapPath("/Images/" + fullName));
            dto.ImageFile.SaveAs(fullName);
            using (CNGLUTTEDBEntities db = new CNGLUTTEDBEntities())
            {
                db.Images.Add(dto);
                db.SaveChanges();
                ViewBag.Message = "Uploaded successfully.";
            }
            ModelState.Clear();
            return View();
        }
    }
}