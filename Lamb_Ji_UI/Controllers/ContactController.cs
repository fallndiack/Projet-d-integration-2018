using Lamb_Ji_ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Lamb_Ji_UI.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnvoyerEmail(string nom, string email, string sujet, string message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(email);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("cnglamb@gmail.com");//Where mail will be sent 
                    
                    msz.Subject = sujet;
                    msz.Body = message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("cnglamb@gmail.com", "lambfacile");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Merci! votre message à bien été envoyé nous vous répondrons dés que possible ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Désolé nous avons rencontré un probléme {ex.Message}";
                }
            }

            return View("Index");
        }
      

       
    }
}