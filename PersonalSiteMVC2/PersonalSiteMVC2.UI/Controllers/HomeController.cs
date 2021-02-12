using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalSiteMVC2.UI.Models;
using System.Net.Mail;
using System.Net;

namespace PersonalSiteMVC2.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            //Steps to send an email:
            //1) Create a string fot he message
            string emailBody = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response the following message: <br /><br /> {cvm.Message}";
            //2) Create the MailMessage object - System.Net.Mail
            MailMessage msg = new MailMessage
            (
                "no-reply@johndavidswift.com",
                "jdavidswift@gmail.com",
                "Email from johndavidswift.com",
                emailBody
            );

            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("mail.johndavidswift.com");
            
            client.Credentials = new NetworkCredential("no-reply@johndavidswift.com", "Ravens75!");
            client.Port = 8889;

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Sorry, something went wrong. Error message: {ex.Message}<br />{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }
    }
}