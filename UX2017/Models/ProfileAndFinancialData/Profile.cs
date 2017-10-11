using System;
using System.Collections.Generic;

namespace UX2017.Models.ProfileAndFinancialData
{
    public class Profiles
    {
        public Status Status { get; set; }
        public IEnumerable<Profile> Results { get; set; }
    }

    public class Profile
    {
        public string Symbol { get; set; }
        public string SymbolCode { get; set; }
        public string Exchange { get; set; }
        public string ExchangeName { get; set; }
        public string SicSector { get; set; }
        public string Industry { get; set; }
        public string SubIndustry { get; set; }
        public IEnumerable<string> IndexMembership { get; set; }
        public string BusinessSummary { get; set; }
        public string Ceo { get; set; }
        public double QtrOneEarnings { get; set; }
        public DateTime? QtrOneEarningsDate { get; set; }
        public double? QtrTwoEarnings { get; set; }
        public DateTime? QtrTwoEarningsDate { get; set; }
        public double? QtrThreeEarnings { get; set; }
        public DateTime? QtrThreeEarningsDate { get; set; }
        public double? QtrFourEarnings { get; set; }
        public DateTime? QtrFourEarningsDate { get; set; }
        public string PeRatio { get; set; }
        public string EpsGrowth { get; set; }
        public string RecentEarnings { get; set; }
        public string AnnualEps { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string InstrumentType { get; set; }
    }
}