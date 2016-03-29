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
            base.Initialize(requestContext);
            _repo = new MediaRepo(ConfigurationManager.AppSettings);
        }

        //Returns list of all series in the DB
        public JsonResult GetSeries()
        {
            return Json(_repo.GetSeries(), JsonRequestBehavior.AllowGet);
        }

        //Returns list of Episodes in the given series
        public JsonResult GetEpisodesBySeries(int id)
        {
            return Json(_repo.GetEpisodesBySeries(id), JsonRequestBehavior.AllowGet);
        }

        //Returns episode with given id
        public JsonResult GetEpisodeByID(int id)
        {
            return Json(_repo.GetEpisodeByID(id), JsonRequestBehavior.AllowGet);
        }
    }
}