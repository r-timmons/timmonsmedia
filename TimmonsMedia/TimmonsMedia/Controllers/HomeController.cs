using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimmonsMedia.Models.Repositories;

namespace TimmonsMedia.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult WhoYouBe()
        {
            var repo = new PersonRepo();
            return Json(repo.IAm(), JsonRequestBehavior.AllowGet);
        }
    }
}