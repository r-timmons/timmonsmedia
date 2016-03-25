using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.IO;

namespace HomeServer
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BusinessLogicLayer bll = new BusinessLogicLayer();
                ddl_tv.DataSource = bll.GetSeries();
                ddl_tv.DataTextField = "Title";
                ddl_tv.DataValueField = "seriesid";
                ddl_tv.DataBind();
                ListItem selectSeries = new ListItem("Select a series..", "-1");
                ddl_tv.Items.Insert(0, selectSeries);
            }
        }

        protected void ddl_tv_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If they select "Select a series.."
            if (Int32.Parse(ddl_tv.SelectedValue) == -1)
            {
                //Make Seasons DDL and LBL hidden
                lblSeason.Visible = false;
                ddl_seasons.Visible = false;
                //Make Episodes DDL and LBL hidden
                lblEpisode.Visible = false;
                ddl_episodes.Visible = false;

                btnScan.Visible = false;
            }
            else
            {
                BusinessLogicLayer bll = new BusinessLogicLayer();
                ddl_seasons.DataSource = bll.GetSeasons(ddl_tv.SelectedIndex);
                ddl_seasons.DataTextField = "season";
                ddl_seasons.DataValueField = "season";
                ddl_seasons.DataBind();

                lblSeason.Visible = true;
                ddl_seasons.Visible = true;
                btnScan.Visible = true;

                foreach (ListItem li in ddl_seasons.Items)
                {
                    li.Text = "Season " + li.Text;
                }

                ListItem selectSeason = new ListItem("Select a season..", "-1");
                ddl_seasons.Items.Insert(0, selectSeason);
            }
        }
        protected void ddl_seasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If they select "Select a season.."
            if (Int32.Parse(ddl_seasons.SelectedValue) == -1)
            {
                videoDiv.InnerHtml = "";
                //Make Episodes DDL and LBL hidden
                lblEpisode.Visible = false;
                ddl_episodes.Visible = false;
            }
            else
            {
                BusinessLogicLayer bll = new BusinessLogicLayer();
                ddl_episodes.DataSource = bll.GetEpisodes(Int32.Parse(ddl_seasons.SelectedValue), ddl_tv.SelectedIndex);
                ddl_episodes.DataTextField = "Title";
                ddl_episodes.DataValueField = "episodeid";
                ddl_episodes.DataBind();

                ListItem selectEps = new ListItem("Select an episode..", "-1");
                ddl_episodes.Items.Insert(0, selectEps);

                ddl_episodes.Visible = true;
                lblEpisode.Visible = true;
            }
        }

        protected void ddl_episodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Int32.Parse(ddl_episodes.SelectedValue) == -1)
            {
                videoDiv.InnerHtml = "";
            }
            else
            {
                BusinessLogicLayer bll = new BusinessLogicLayer();
                string filename = bll.GetFileName(Int32.Parse(ddl_episodes.SelectedItem.Value));

                videoDiv.InnerHtml = "<video id='vidPlayer' src=\"/Videos/" + filename + "\" controls='controls' runat='server' type='video/mp4' width='100%' height='100%'></video>";
            }
        }

        protected void btnScan_Click(object sender, EventArgs e)
        {

            int seasonid = Int32.Parse(ddl_tv.SelectedValue);
            string[] filepaths = Directory.GetFiles(Server.MapPath("~/videos/" + ddl_tv.SelectedItem.Text));
            
            int numAdded = 0;
            int numExisted = 0;

            foreach(string file in filepaths)
            {
                //Split by \ and take last piece
                string[] split = file.Split('\\');
                string fileName = split[split.Length - 1];
                //Substrings containing season, episode, and title
                string stringseason = fileName.Substring(2, 2);
                string stringepi = fileName.Substring(5, 2);
                string title = fileName.Substring(9);
                //Parse ints for season and episode
                int season = Int32.Parse(stringseason);
                int episodenum = Int32.Parse(stringepi);
                //remove file extension
                string[] titleSplit = title.Split('.');
                title = titleSplit[0];
                for (int x = 1; x < titleSplit.Length - 1; x++)
                {
                    title = title + '.' + titleSplit[x];
                }

                //Check DB to see if it exists
                BusinessLogicLayer bll = new BusinessLogicLayer();
                if(!bll.EpisodeExists(season, episodenum, title, seasonid))
                {
                    bll.InsertEpisode(season, episodenum, title, seasonid);
                    numAdded++;
                }
                else
                {
                    numExisted++;
                }
            }
            lblScanSuccess.Text = "Added : " + numAdded + "\t Existed : " + numExisted;
            lblScanSuccess.Visible = true;
        }
    }
}