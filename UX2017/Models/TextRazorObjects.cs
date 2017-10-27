using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UX2017.Models
{
    public class TextRazorResponse
    {
        public Response Response { get; set; }
        public bool Ok { get; set; }
        public double Time { get; set; }
    }

    public class Response
    {
        public string Language { get; set; }
        public List<Topic> CoarseTopics { get; set; }
        public bool LanguageIsReliable { get; set; }
        public List<Topic> Topics { get; set; }
    }

    public class Topic
    {
        public string Label { get; set; }
        public string WikiLink { get; set; }
        public long Id { get; set; }
        public double Score { get; set; }
        public string WikidataId { get; set; }
    }
}