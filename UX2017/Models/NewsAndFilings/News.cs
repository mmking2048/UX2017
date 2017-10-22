using System;
using System.Collections.Generic;

namespace UX2017.Models.NewsAndFilings
{
    public class News
    {
        public int NewsID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Source { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> Subcategories { get; set; }
        public string Headline { get; set; }
        public bool IsExternal { get; set; }
        public string HeadlineUrl { get; set; }
        public string Preview { get; set; }
        public string FullText { get; set; }
        public string ImageUrl { get; set; }
        public string ImageCaption { get; set; }
        public int? ImageHeight { get; set; }
        public int? ImageWidth { get; set; }
        public string PdfUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string LargeImageUrl { get; set; }
        public int? LargeImageHeight { get; set; }
        public int? LargeImageWidth { get; set; }
    }
}