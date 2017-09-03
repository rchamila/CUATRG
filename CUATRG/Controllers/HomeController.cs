using log4net;
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }

        public ActionResult Albums()
        {
            ViewBag.Message = "Albums.";

            return View();
        }

        [HttpPost]
        public void ContactUs(string name, string email, string message)
                                    
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("rchamila@gmail.com", "B3mmu11egedar@"),
                EnableSsl = true
            };
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Name :" + name);
            builder.AppendLine("Message :" + message);

            client.Send(email, "rchamila@gmail.com", "Message Received", builder.ToString());
        }

        public ActionResult Resources()
        {
            ViewBag.Message = "Resources.";

            return View();
        }
    }
}
