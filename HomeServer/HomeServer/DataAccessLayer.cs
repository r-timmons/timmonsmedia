using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace HomeServer
{
    public class DataAccessLayer
    {
        private MySqlConnection conn;
        private MySqlDataAdapter da;
        private MySqlCommandBuilder builder;
        private DataSet ds;

        public DataAccessLayer()
        {
            //Retrieve cs an create connection
            string cs = ConfigurationManager.ConnectionStrings["cs_homeserver"].ConnectionString;
            conn = new MySqlConnection(cs);
        }

        private void FillDataSet(MySqlCommand cmd)
        {
            //Generic method to fill dataset based on command entered
            da = new MySqlDataAdapter(cmd);
            ds = new DataSet();

            da.Fill(ds, "Data");
        }

        public DataSet GetSeries()
        {
            //Retrieve series list
            string query = "SELECT * FROM tvseries";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            FillDataSet(cmd);
            return ds;
        }

        public DataSet GetSeasons(int seriesID)
        {
            //Retrieve distinct seasons based on the series id
            string query = "SELECT DISTINCT season FROM episode "
               + "WHERE seriesid = @seriesID order by season asc";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@seriesID", seriesID);
            FillDataSet(cmd);
            return ds;
        }

        public DataSet GetEpisodes(int seriesId, int season)
        {
            //Retrieve all episodes from season based on series and season
            string query = "SELECT * FROM episode "
               + "WHERE season = @season AND seriesid = @seriesid order by episodenum asc";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@seriesid", seriesId);
            cmd.Parameters.AddWithValue("@season", season);
            FillDataSet(cmd);
            return ds;
        }

        public String GetFileName(int episodeId)
        {
            string query = "SELECT tvseries.title, episode.season, episode.episodenum, episode.title FROM episode "
                + "INNER JOIN tvseries ON tvseries.seriesid = episode.seriesid WHERE episodeid = @episodeid";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@episodeid", episodeId);

            FillDataSet(cmd);

            string series = (string)ds.Tables[0].Rows[0][0];
            int season = (int)ds.Tables[0].Rows[0][1];
            int episode = (int)ds.Tables[0].Rows[0][2];
            string title = (string)ds.Tables[0].Rows[0][3];

            //All videos in "videos/Series/[SXXEXX] episodename.ext"
            string filename = series + "/";
            //If Season number is less than 10 add a 0 infront of the number
            if (season < 10)
                filename = filename + "[S0" + season;
            else
                filename = filename + "[S" + season;
            //If episode number is less than 10 add a 0 infront of the number
            if (episode < 10)
                filename = filename +"E0"+ episode + "] ";
            else
                filename = filename + "E" + episode + "] ";
            //Append the episode title and extension
            filename = filename + title + ".mp4";
            return filename;
        }

        public bool InsertEpisode(int season, int episodenum, string title, int seriesId)
        {
            //Insert new episode into episodes
            string query = "SELECT * from episode";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            FillDataSet(cmd);
            DataRow dr = ds.Tables["Data"].NewRow();

            //Set params
            dr["seriesid"] = seriesId;
            dr["season"] = season;
            dr["episodenum"] = episodenum;
            dr["title"] = title;

            //Add row
            ds.Tables["Data"].Rows.Add(dr);

            builder = new MySqlCommandBuilder(da);
            int rowsInserted = da.Update(ds, "Data");
            //If works return true
            if (rowsInserted > 0) return true;
            else return false;
        }

        public bool EpisodeExists(int season, int episodenum, string title, int seriesId)
        {
            string query = "SELECT COUNT(*) FROM episode WHERE (season = @season AND episodenum = @episodenum "
                + "AND seriesid = @seriesId) OR (seriesid = @seriesId AND title = @title)";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@episodenum", episodenum);
            cmd.Parameters.AddWithValue("@season", season);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@seriesId", seriesId);

            int rowsFound = Convert.ToInt32(GetInfo(cmd));
            if (rowsFound > 0) return true;
            else return false;
        }

        private object GetInfo(MySqlCommand cmd)
        {
            //Retrieve info as object, allowing any method to access and retrieve whatever it needs
            conn.Open();
            object str = cmd.ExecuteScalar();
            return str;
        }
    }
}
