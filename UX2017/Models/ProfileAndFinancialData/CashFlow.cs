using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UX2017.Models.ProfileAndFinancialData
{
    public class CashFlow
    {
        public string Symbol { get; set; }
        public string Period { get; set; }
        public double NetIncome { get; set; }
        public double DepreciationAndAmortization { get; set; }
        public double DeferredIncomeTax { get; set; }
        public double AccountsReceivable { get; set; }
        public double AccountsPayable { get; set; }
        public double OtherWorkingCapital { get; set; }
        public double Loans { get; set; }
        public double OtherOperatingActivities { get; set; }
        public double NetCashFromOperatingActivities { get; set; }
        public double ChangeInDepositsWithBank { get; set; }
        public double InvestmentsInProperty { get; set; }
        public double NetAcquisitions { get; set; }
        public double PurchasesOfInvestments { get; set; }
        public double SalesOfInvestments { get; set; }
        public double PurchasesAndSaleOfIntangibles { get; set; }
        public double NetChangeInLoans { get; set; }
        public double OtherInvestingActivity { get; set; }
        public double NetCashUsedForInvestingActivities { get; set; }
        public double ChangeInDeposits { get; set; }
        public double ChangeInShortTermBorrowing { get; set; }
        public double DebtIssued { get; set; }
        public double DebtRepayment { get; set; }
        public double CommonStockIssued { get; set; }
        public double CommonStockRepurchased { get; set; }
        public double DividendPaid { get; set; }
        public double OtherFinancingActivity { get; set; }
        public double NetCashProvidedByFinancingActivities { get; set; }
        public double EffectOfExchangeRateChanges { get; set; }
        public double CashAtBeginningOfPeriod { get; set; }
        public double CashAtEndOfPeriod { get; set; }
        public double NetChangeInCash { get; set; }
        public double OperatingCashFlow { get; set; }
        public double CapitalExpenditure { get; set; }
        public double FreeCashFlow { get; set; }
    }
}