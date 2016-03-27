using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimmonsMedia.Models
{
    public class Episode
    {
        public int ID { get; set; }
        public int seriesId { get; set; }
        public int Season { get; set; }
        public int EpisodeNum { get; set; }
        public string Title { get; set; }
    }
}