using System;

namespace UX2017.Models.PriceData
{
    public class QuoteEod
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }
        public int TotalTrades { get; set; }
        public string DataSource { get; set; }
    }
}