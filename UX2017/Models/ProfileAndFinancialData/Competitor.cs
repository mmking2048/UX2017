using System;

namespace UX2017.Models.ProfileAndFinancialData
{
    public class Competitor
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public int MarketCap { get; set; }
        public double? FiftyTwoWkHigh { get; set; }
        public DateTime? FiftyTwoWkHighDate { get; set; }
        public double? FiftyTwoWkLow { get; set; }
        public DateTime? FiftyTwoWkLowDate { get; set; }
    }

    // TODO: getRatings
}