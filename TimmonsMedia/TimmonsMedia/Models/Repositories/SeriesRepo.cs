using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace TimmonsMedia.Models.Repositories
{
    public class SeriesRepo
    {
        public DataContext _repo;

        public SeriesRepo()
        {
            _repo = new DataContext(@"Data Source=RYANTIMMONS-PC\SQLExpress;Initial Catalog=timmonsmedia; Integrated Security=true;");
        }

        public List<Series> GetSeries()
        {
            return _repo.ExecuteQuery<Series>("select * from series").ToList();
        }
    }
}