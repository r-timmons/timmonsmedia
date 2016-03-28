using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Linq;
using System.Linq;
using System.Web;
using TimmonsMedia.Models.Repositories.DataSources;

namespace TimmonsMedia.Models.Repositories
{
    public class MediaRepo
    {
        private static string MEDIADB_KEY = "TimmonsMedia-J";
        public MediaDataSource _db;

        public MediaRepo(NameValueCollection config) 
        {
            _db = new MediaDataSource(config[MEDIADB_KEY]);
        }

        public List<Series> GetSeries()
        {
            return _db.GetSeries(null).ToList();
        }
    }
}