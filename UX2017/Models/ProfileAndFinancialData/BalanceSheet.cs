using System;

namespace UX2017.Models.ProfileAndFinancialData
{
    public class BalanceSheet
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Cash { get; set; }
        public double MarketableSec { get; set; }
        public double Receivables { get; set; }
        public double PrepaidExpenses { get; set; }
        public double Inventories { get; set; }
        public double DeferredIncomeTax { get; set; }
        public double OtherCurrentAssets { get; set; }
        public double TotalCurrentAssets { get; set; }
        public double PpeNet { get; set; }
        public double EquityOtherInvestments { get; set; }
        public double Intangibles { get; set; }
        public double OtherNonCurrentAssets { get; set; }
        public double TotalNonCurrentAssets { get; set; }
        public double TotalAssets { get; set; }
        public double ShorttermDebt { get; set; }
        public double AccountsPayable { get; set; }
        public double IncomeTaxPayable { get; set; }
        public double AccruedExpenses { get; set; }
        public double OtherCurrentLiabilities { get; set; }
        public double TotalCurrentLiabilities { get; set; }
        public double LongTermDebt { get; set; }
        public double DeferredLongRevenues { get; set; }
        public double OtherNonCurrentLiabilities { get; set; }
        public double TotalNonCurrentLiabilities { get; set; }
        public double TotalLiabilities { get; set; }
        public double CommonShares { get; set; }
        public double RetainedEarnings { get; set; }
        public double OtherEquity { get; set; }
        public double TotalShareholdersEquity { get; set; }
        public double TotalLiabilitiesAndEquity { get; set; }
    }
}