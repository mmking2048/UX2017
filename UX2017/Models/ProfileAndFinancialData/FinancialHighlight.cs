using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UX2017.Models.ProfileAndFinancialData
{
    public class FinancialHighlight
    {
        public string Symbol { get; set; }
        public int MarketCapitalization { get; set; }
        public double InsiderShareholders { get; set; }
        public int AnnualRevenue { get; set; }
        public int TtmRevenue { get; set; }
        public int SharesOutstanding { get; set; }
        public double InstitutionalShareholders { get; set; }
        public int? AnnualNetIncome { get; set; }
        public double? TtmNetIncome { get; set; }
        public double? TtmNetProfitMargin { get; set; }
        public double OneYearReturn { get; set; }
        public double ThreeYearReturn { get; set; }
        public double FiveYearReturn { get; set; }
        public double FiveYearRevenueGrowth { get; set; }
        public double FiveYearEarningsGrowth { get; set; }
        public double FiveYearDividendGrowth { get; set; }
        public double? LastQtrEps { get; set; }
        public double AnnualEps { get; set; }
        public double TtmEps { get; set; }
        public double AnnualDividendRate { get; set; }
        public double AnnualDividendYield { get; set; }
        public double? TwelveMonthEpsChg { get; set; }
        public double? PeRatio { get; set; }
        public double? RecentEarnings { get; set; }
        public double? RecentDividends { get; set; }
        public string RecentSplit { get; set; }
        public double? Beta { get; set; }
        public double WeightAlpha { get; set; }
    }
}