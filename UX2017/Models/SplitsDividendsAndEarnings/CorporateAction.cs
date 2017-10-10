using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UX2017.Models.SplitsDividendsAndEarnings
{
    public class CorporateAction
    {
        public string Symbol { get; set; }
        public DateTime EventDate { get; set; }
        public string EventType { get; set; }
        public double Value { get; set; }
    }
}