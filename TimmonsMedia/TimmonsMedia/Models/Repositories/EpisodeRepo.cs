using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace TimmonsMedia.Models.Repositories
{
    public class EpisodeRepo
    {
        public DataContext _repo;

        public EpisodeRepo()
        {
            _repo = new DataContext(@"Data Source=RYANTIMMONS-PC\SQLExpress;Initial Catalog=timmonsmedia; Integrated Security=true;");
        }

        public List<Episode> GetEpisodes()
        {
            return _repo.ExecuteQuery<Episode>("select * from episode").ToList();
        }

        public List<Episode> GetEpisodesBySeries(int id)
        {
            return _repo.ExecuteQuery<Episode>("select * from episode where seriesId = " + id + " order by season, episodenum").ToList();
        }

        public List<Episode> GetEpisodeByID(int id)
        {
            return _repo.ExecuteQuery<Episode>("select * from episode where id = " + id).ToList();
        }
    }
}