using System;

namespace UX2017.Models.SplitsDividendsAndEarnings
{
    public class EarningsEstimate
    {
        public string Symbol { get; set; }
        public string SymbolName { get; set; }
        public string Period { get; set; }
        public double AverageEstimate { get; set; }
        public int? NumAnalysts { get; set; }
        public double? HighEstimate { get; set; }
        public double? LowEstimate { get; set; }
        public double? PriorYear { get; set; }
        public double? GrowthRateEstimate { get; set; }
        public DateTime? CurrentQtrExpectedReportDate { get; set; }
        public string ExpectedEarningsStatus { get; set; }
        public string ExpectedEarningsSource { get; set; }
        public string DividendType { get; set; }
        public double? IndicatedAnnualDvnd { get; set; }
        public string DividendStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? RecordDate { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public DateTime? ExDividendDate { get; set; }
        public double? DividendRate { get; set; }
    }
}