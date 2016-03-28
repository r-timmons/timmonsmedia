using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimmonsMedia.Models;
using TimmonsMedia.Models.Repositories;

namespace TimmonsMedia.Controllers
{
    public class MediaController : Controller
    {

        private MediaRepo _repo;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            _repo = new MediaRepo(ConfigurationManager.AppSettings);
        }

        [HttpGet]
        public JsonResult GetSeries()
        {
            var repo = new SeriesRepo();
            List<Series> list = repo.GetSeries();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEpisodes(int id)
        {
            var repo = new EpisodeRepo();
            List<Episode> list = repo.GetEpisodesBySeries(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFilename(int id)
        {
            var repo = new EpisodeRepo();
            List<Episode> list = repo.GetEpisodeByID(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}