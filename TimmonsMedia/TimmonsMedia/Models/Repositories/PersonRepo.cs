using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace TimmonsMedia.Models.Repositories
{
    public class PersonRepo
    {
        public DataContext _repo;

        public PersonRepo()
        {
            _repo = new DataContext(@"Data Source=RYANTIMMONS-PC\SQLExpress;Initial Catalog=timmonsmedia; Integrated Security=true;");
        }

        public List<Person> IAm()
        {
            return _repo.ExecuteQuery<Person>("select * from person").ToList();
        }
    }
}