using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UX2017.Models
{
    public class NewsArticle
    {
        public NewsArticle(string headline, string body, string imageUrl = "")
            : this(0, headline, body, imageUrl) { }

        public NewsArticle(int newsID, string headline, string body, string imageUrl = "")
        {
            NewsID = newsID;
            Headline = headline;
            Body = body;
            ImageUrl = imageUrl;
        }

        public int NewsID { get; set; }
        public string Headline { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
    }
}