using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace HomeServer
{
    public class BusinessLogicLayer
    {
        public DataSet GetSeries()
        {
            //Retrieve all accounts belonging to that card no
            DataAccessLayer dal = new DataAccessLayer();
            return dal.GetSeries();
        }

        public DataSet GetSeasons(int season)
        {
            //Retrieve all accounts belonging to that card no
            DataAccessLayer dal = new DataAccessLayer();
            return dal.GetSeasons(season);
        }

        public DataSet GetEpisodes(int season, int seriesId)
        {
            //Retrieve all accounts belonging to that card no
            DataAccessLayer dal = new DataAccessLayer();
            return dal.GetEpisodes(seriesId, season);
        }

        public String GetFileName(int episodeId)
        {
            DataAccessLayer dal = new DataAccessLayer();
            return dal.GetFileName(episodeId);
        }

        public bool EpisodeExists(int season, int episodenum, string title, int seriesId)
        {
            DataAccessLayer dal = new DataAccessLayer();
            return dal.EpisodeExists(season, episodenum, title, seriesId);
        }

        public bool InsertEpisode(int season, int episodenum, string title, int seriesId)
        {
            DataAccessLayer dal = new DataAccessLayer();
            return dal.InsertEpisode(season, episodenum, title, seriesId);
        }
    }
}
