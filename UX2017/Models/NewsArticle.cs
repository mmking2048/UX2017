using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UX2017.Models
{
    public class NewsArticle
    {
        public NewsArticle(string headline, string body)
        {
            Headline = headline;
            Body = body;
        }

        public string Headline { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
    }
}