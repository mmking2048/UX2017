namespace UX2017.Models.ProfileAndFinancialData
{
    public class FinancialRatio
    {
        public string Symbol { get; set; }
        public double Eps { get; set; }
        public double ProfitMargin { get; set; }
        public double Roe { get; set; }
        public double Roa { get; set; }
        public double PriceSales { get; set; }
        public double PriceEarnings { get; set; }
        public double PriceBook { get; set; }
        public double? DebtEquity { get; set; }
        public double? InterestCoverage { get; set; }
        public double? BookValue { get; set; }
        public double? DividendPayout { get; set; }
    }
}