using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvcBasementHelloWorld.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            string ausgabe = "GET returns IP: " + this.Request.UserHostAddress;
            ausgabe += "<br><br><br><form name=\"mvcForm\" method=\"post\" action=\"\"><input type=\"submit\" name=\"SubmitBtn\" value=\"Submit\" /></form>";
            ViewBag.result = this.Request.UserHostAddress;
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post()
        {
            string ausgabe = "POST returns Texst of the SubmitBtn: " + this.Request.Form["SubmitBtn"];
            ausgabe += "<br><br><br><form name=\"mvcForm\" method=\"post\" action=\"\"><input type=\"submit\" name=\"SubmitBtn\" value=\"Submit\" /></form>";
            ViewBag.result = this.Request.Form["SubmitBtn"];
            //return ausgabe;
            return View();
        }
    }
}
