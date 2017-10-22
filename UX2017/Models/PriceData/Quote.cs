using System;

namespace UX2017.Models.PriceData
{
    public class Quote
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string DayCode { get; set; }
        public DateTime ServerStamp { get; set; }
        public string Mode { get; set; }
        public double LastPrice { get; set; }
        public int TradeSize { get; set; }
        public DateTime TradeTimestamp { get; set; }
        public double NetChange { get; set; }
        public double PercentChange { get; set; }
        public string Tick { get; set; }
        public double PreviousLastPrice { get; set; }
        public DateTime PreviousTimestamp { get; set; }
        public double? Bid { get; set; }
        public int? BidSize { get; set; }
        public double? Ask { get; set; }
        public int? AskSize { get; set; }
        public string UnitCode { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int NumTrades { get; set; }
        public double DollarVolume { get; set; }
        public string Flag { get; set; }
        public double? PreviousClose { get; set; }
        public double? Settlement { get; set; }
        public double? PreviousSettlement { get; set; }
        public int Volume { get; set; }
        public int PreviousVolume { get; set; }
        public double? OpenInterest { get; set; }
        public double? FiftyTwoWkHigh { get; set; }
        public DateTime? FiftyTwoWkHighDate { get; set; }
        public double? FiftyTwoWkLow { get; set; }
        public DateTime? FiftyTwoWkLowDate { get; set; }
        public int? AvgVolume { get; set; }
        public int? SharesOutstanding { get; set; }
        public float? DividendRateAnnual { get; set; }
        public float? DividendYieldAnnual { get; set; }
        public DateTime? ExDividendDate { get; set; }
        public double? ImpliedVolatility { get; set; }
        public double? TwentyDayAvgVol { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string LastTradingDay { get; set; }
        public double? TwelveMnthPct { get; set; }
        public DateTime? TwelveMnthPctDate { get; set; }
        public double? PreMarketPrice { get; set; }
        public double? PreMarketNetChange { get; set; }
        public double? PreMarketPercentChange { get; set; }
        public DateTime? PreMarketTimestamp { get; set; }
        public double? AfterHoursPrice { get; set; }
        public double? AfterHoursNetChange { get; set; }
        public double? AfterHoursPercentChange { get; set; }
        public DateTime? AfterHoursTimestamp { get; set; }
        public int? AverageWeeklyVolume { get; set; }
        public int? AverageMonthlyVolume { get; set; }
        public int? AverageQuarterlyVolume { get; set; }
        public string ExchangeMargin { get; set; }
        public double? OneMonthHigh { get; set; }
        public DateTime? OneMonthHighDate { get; set; }
        public double? OneMonthLow { get; set; }
        public DateTime? OneMonthLowDate { get; set; }
        public double? ThreeMonthHigh { get; set; }
        public DateTime? ThreeMonthHighDate { get; set; }
        public double? ThreeMonthLow { get; set; }
        public DateTime? ThreeMonthLowDate { get; set; }
        public double? SixMonthHigh { get; set; }
        public DateTime? SixMonthHighDate { get; set; }
        public double? SixMonthLow { get; set; }
        public DateTime? SixMonthLowDate { get; set; }
    }
}