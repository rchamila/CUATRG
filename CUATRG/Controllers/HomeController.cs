﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CUATRG.Controllers
{
    public class HomeController : Controller
    {
        ILog log = log4net.LogManager.GetLogger("CUATRG");
        public ActionResult Index()
        {
            ViewBag.Message = "Home";
            log.Info("Test");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About";

            return View();
        }

        public ActionResult Contact(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public ActionResult Albums()
        {
            ViewBag.Message = "Albums.";

            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(string name, string email, string message)
                                    
        {
            var msg = "";
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("cuatrg@gmail.com", "cuatrg2018"),
                    EnableSsl = true
                };
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Email :" + email);
                builder.AppendLine("Name :" + name);
                builder.AppendLine("Message :" + message);

                log.InfoFormat("Contact us : " + builder.ToString());

                client.Send(email, "rchamila@gmail.com,cuatrg@gmail.com", "Message Received", builder.ToString());
                client.EnableSsl = true;
                msg = "Thank you for submitting your inquery.  We will contact you shortly";
            }
            catch(Exception ex)
            {
                log.Error(ex);
                //msg = "Error contacting CUATRG team. Please try again."; 
                msg = "Thank you for submitting your inquery.";
            }
            return RedirectToAction("Contact", new { message = msg });
        }

        public ActionResult Resources()
        {
            ViewBag.Message = "Resources.";

            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Error";
            log.Info("Test");

            return View();
        }
    }
}
