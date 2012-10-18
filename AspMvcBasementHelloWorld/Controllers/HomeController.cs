using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcBasementHelloWorld.ViewModels;

namespace AspMvcBasementHelloWorld.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new DialogueModel();
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post()
        {
            return View();
        }
    }
}
