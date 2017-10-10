using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UX2017.Models.ProfileAndFinancialData
{
    public class IncomeStatement
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Sales { get; set; }
        public double CostGoods { get; set; }
        public double GrossProfit { get; set; }
        public double OperatingExpenses { get; set; }
        public double OperatingIncome { get; set; }
        public double InterestExpense { get; set; }
        public double OtherIncomeExpenses { get; set; }
        public double PreTaxIncome { get; set; }
        public double IncomeTax { get; set; }
        public double NetIncome { get; set; }
        public double BasicEpsContOp { get; set; }
        public double BasicEpsTotalOp { get; set; }
        public double DilutedEpsContOp { get; set; }
        public double DilutedEpsTotalOp { get; set; }
        public double Ebitda { get; set; }
    }
}