using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimmonsMedia.Models;
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

        public ActionResult Watch()
        {
            ViewBag.Message = "Watch Series";
            return View();
        }

        public JsonResult GetSeries()
        {
            var repo = new SeriesRepo();
            List<Series> list = repo.GetSeries();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEpisodes(int id)
        {
            var repo = new EpisodeRepo();
            List<Episode> list = repo.GetEpisodesBySeries(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFilename(int id)
        {
            var repo = new EpisodeRepo();
            List<Episode> list = repo.GetEpisodeByID(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}