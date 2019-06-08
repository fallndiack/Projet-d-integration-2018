﻿using Lamb_Ji_DAL;
using Lamb_Ji_ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lamb_Ji_UI.Controllers
{
    public class LutteurController : Controller
    {
        CNGLUTTEDBEntities db = new CNGLUTTEDBEntities();

        public ActionResult Index()
        {
            List<Club> LutList = db.Clubs.ToList();
            ViewBag.ListOfLutteur = new SelectList(LutList, "ClubID", "ClubName");

            return View();
        }

       
        public JsonResult GetLutteurList()
        {
            List<Lutteur> LutteursDb = db.Lutteurs.ToList();
            List<LutteurViewModel> LutteursVM = new List<LutteurViewModel>();
            foreach (var item in LutteursDb)
            {
                LutteurViewModel Lvm = new LutteurViewModel();
                Lvm.LutteurID = item.LutteurID;
                Lvm.LutteurClubID = item.LutteurClubID;
                Lvm.LutteurName = item.LutteurName;
                Lvm.LutteurEmail = item.LutteurEmail;
                Lvm.LutteurDescription = item.LutteurDescription;
                Lvm.LutteurAddresse = item.LutteurAddresse;
                Lvm.LutteurDateNaissance = item.LutteurDateNaissance;
                Lvm.LutteurPoids = item.LutteurPoids;
                Lvm.LutteurTelephone = item.LutteurTelephone;
                Lvm.ClubName = item.Club.ClubName;

                LutteursVM.Add(Lvm);
            }

            return Json(LutteursVM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLutteurById(int id)
        {
            LutteurViewModel Lvm = new LutteurViewModel();
            
            Lutteur lut = db.Lutteurs.Where(x => x.LutteurID == id).Include(c => c.Club).FirstOrDefault();
           
            Lvm.LutteurID = lut.LutteurID;
            Lvm.LutteurClubID = lut.LutteurClubID;
            Lvm.LutteurName = lut.LutteurName;
            Lvm.LutteurEmail = lut.LutteurEmail;
            Lvm.LutteurDescription = lut.LutteurDescription;
            Lvm.LutteurAddresse = lut.LutteurAddresse;
            //Lvm.LutteurDateNaissance = lut.LutteurDateNaissance;

           
            //Lvm.LutteurDateNaissance = (DateTime.Parse(Lvm.LutteurDateNaissance.ToString("dd MMM yyyy",
            //        CultureInfo.CreateSpecificCulture("fr-FR"))));

            DateTime dt = lut.LutteurDateNaissance;
            CultureInfo iv = CultureInfo.InvariantCulture;
            Lvm.LutteurDateNaissance = Convert.ToDateTime(dt.ToString("d", iv));

            Lvm.LutteurPoids = lut.LutteurPoids;
            Lvm.LutteurTelephone = lut.LutteurTelephone;
            
            Lvm.imageUrl = lut.imageUrl;
            //Lvm.Club = lut.Club;
            //string value = string.Empty;
            //value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});
            return Json(Lvm, JsonRequestBehavior.AllowGet);
        }

      





        [HttpPost]
        public ActionResult SaveDataInDatabase(LutteurViewModel model)
        {
           
            if (ModelState.IsValid) { 

                    try
                {
          
                                if (model.LutteurID > 0)
                            {
                                Lutteur Lut = db.Lutteurs.Include(c => c.Club).SingleOrDefault(x => x.LutteurID == model.LutteurID);
                                Lut.LutteurName = model.LutteurName;
                                Lut.LutteurEmail = model.LutteurEmail;
                                Lut.LutteurDateNaissance = model.LutteurDateNaissance;
                                Lut.LutteurDescription = model.LutteurDescription;
                                Lut.LutteurPoids = model.LutteurPoids;
                                Lut.LutteurTelephone = model.LutteurTelephone;
                                Lut.LutteurAddresse = model.LutteurAddresse;
                                Lut.imageUrl = model.imageUrl;
                                Lut.LutteurClubID = model.LutteurClubID;

                                db.SaveChanges();
                               
                            }
                            else
                            {
                                    if (model.ImageUpload != null)
                                    {
                                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                                        string extension = Path.GetExtension(model.ImageUpload.FileName);
                                        fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                                        model.imageUrl = fileName;
                                        model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/Image-Lutteur"), fileName));
                                    }

                                    Lutteur Lut = new Lutteur();
                                Lut.LutteurName = model.LutteurName;
                                Lut.LutteurEmail = model.LutteurEmail;
                                Lut.LutteurDateNaissance = model.LutteurDateNaissance;
                                Lut.LutteurDescription = model.LutteurDescription;
                                Lut.LutteurPoids = model.LutteurPoids;
                                Lut.LutteurTelephone = model.LutteurTelephone;
                                Lut.LutteurAddresse = model.LutteurAddresse;
                                Lut.imageUrl = model.imageUrl;
                                Lut.LutteurClubID = model.LutteurClubID;
                                db.Lutteurs.Add(Lut);
                                db.SaveChanges();
                               
                            }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return RedirectToAction("Index");

        }

        public JsonResult DeleteLutteurRecord(int LutteurID)
        {
            bool result = false;
           
            Lutteur Lut = db.Lutteurs.SingleOrDefault(x => x.LutteurID == LutteurID);
            List<Licence> listLic = new List<Licence>();
            listLic = db.Licences.Where(x => x.LutteurID == LutteurID).ToList();

            try
            {
                if (listLic.Count > 0)
                {
                    foreach (var lic in listLic)
                    {
                        db.Licences.Remove(lic);
                    }
                    db.SaveChanges();
                    result = true;
                }

                else
                {

                    db.Lutteurs.Remove(Lut);
                    db.SaveChanges();
                    result = true;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
       

        public ActionResult DisplayLuteurForUser(string searching)
        {
            
            return View( db.Lutteurs.Where(x => x.LutteurName.Contains(searching) || searching == null).ToList());
        }
      
    }
}