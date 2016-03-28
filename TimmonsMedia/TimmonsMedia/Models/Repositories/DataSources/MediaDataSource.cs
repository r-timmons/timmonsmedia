using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace TimmonsMedia.Models.Repositories.DataSources
{
    public class MediaDataSource
    {
        public string _conn;

        public MediaDataSource(string conn)
        {
            _conn = conn;
        }

        // Stored procedure calls only in here

        //public List<Episode> GetEpisodes()
        //{
        //    using (var db = new DataContext(_conn))
        //    {
        //        return db.ExecuteQuery<Episode>("select * from episode").ToList();
        //    }
            
        //}

        //public List<Episode> GetEpisodesBySeries(int id)
        //{
        //    using (var db = new DataContext(_conn))
        //    {
        //        return db.ExecuteQuery<Episode>("select * from episode where seriesId = " + id + " order by season, episodenum").ToList();
        //    }
        //}

        //public List<Episode> GetEpisodeByID(int id)
        //{
        //    return db.ExecuteQuery<Episode>("select * from episode where id = " + id).ToList();
        //}

        public List<Series> GetSeries(int? id)
        {
            using (var db = new DataContext(_conn))
            {
                // this will be stored procedure call - J will do later
                return db.ExecuteQuery<Series>("SELECT ID, Title FROM Series ORDER BY Title").ToList();
            }
        }
    }
}