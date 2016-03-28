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

        public JsonResult GetSeries()
        {
            return Json(_repo.GetSeries(), JsonRequestBehavior.AllowGet);
        }
    }
}