using System;
using System.Linq;
using System.Threading.Tasks;
using UX2017.Models;

namespace UX2017
{
    public interface INewsGenerator
    {
        Task<string> GetFinancialSummary(string symbol);
        Task<NewsArticle> GetEarningsSummary(string symbol);
        Task<NewsArticle> GetDividendAnnouncement(string symbol);
    }

    public class NewsGenerator : INewsGenerator
    {
        private readonly IBarchartClient _barchartClient;
        private readonly string _companyUrl = "https://finance.google.com/finance?q=";

        public NewsGenerator(IBarchartClient barchartClient)
        {
            _barchartClient = barchartClient;
        }

        public async Task<string> GetFinancialSummary(string symbol)
        {
            var profile = await _barchartClient.GetProfiles(symbol);
            var quote = (await _barchartClient.GetQuote(new[] { symbol }, new[] { "D" },
                    new[] { "previousClose", "avgVolume", "averageWeeklyVolume" }))
                ?.ElementAt(0);
            var technical = (await _barchartClient.GetTechnicals(new[] { symbol },
                new[] { "priceChangeYTD" }))?.ElementAt(0);
            var earning = await _barchartClient.GetCorporateActions(symbol, EventType.earnings);
            var dividend = await _barchartClient.GetCorporateActions(symbol, EventType.dividend);
            var estimate = await _barchartClient.GetEarningsEstimates(symbol);

            var oneDayVolumeChange = (float)quote.Volume / quote.AvgVolume.Value - 1;
            var fiveDayVolumeChange = (float)quote.AverageWeeklyVolume.Value / quote.AvgVolume.Value - 1;
            var earningsChange = (float)earning.Value / estimate.AverageEstimate - 1;

            return $"<h4>About {profile.ExchangeName}</h4>" +
                   $"<ul><li>{symbol} closed at ${quote.Close}, a {quote.PercentChange}% change from last trading day close of ${quote.PreviousClose}.</li>" +
                   $"<li>{symbol} had a volume of {quote.Volume}, {Math.Abs(oneDayVolumeChange): 0.##}% {(oneDayVolumeChange > 0 ? "above" : "below")} the year-to day volume of {quote.AvgVolume}." +
                   $" The average volume over the last five days ({quote.AverageWeeklyVolume}) is {(fiveDayVolumeChange > 0 ? "up" : "down")} {Math.Abs(fiveDayVolumeChange): 0.##}% compared to the average.</li>" +
                   $"<li>{symbol} has {(technical.PriceChangeYtd > 0 ? "increased" : "decreased")} {Math.Abs(technical.PriceChangeYtd.Value)}% since the start of the year.</li>" +
                   $"<li>{symbol} earnings of {earning.Value} is {Math.Abs(earningsChange): 0.##}% {(earningsChange > 0 ? "higher" : "lower")} than {estimate.AverageEstimate} as estimated.</li>" +
                   $"{(dividend != null ? $"<li>{symbol} last dividend was {dividend.Value}.</li></ul>" : "")}";
        }

        public async Task<NewsArticle> GetEarningsSummary(string symbol)
        {
            string headline;

            var profile = (await _barchartClient.GetProfiles(new []{symbol},
                new []
                {
                    "qtrOneEarnings", "qtrOneEarningsDate", "qtrTwoEarnings", "qtrTwoEarningsDate",
                    "qtrThreeEarnings", "qtrThreeEarningsDate", "qtrFourEarnings", "qtrFourEarningsDate"
                })).FirstOrDefault();
            var chart = await _barchartClient.GetChart(symbol);
            var earnings = await _barchartClient.GetCorporateActions(new[] { symbol }, null, null, EventType.earnings);
            var earning = earnings.FirstOrDefault();
            var estimate = await _barchartClient.GetEarningsEstimates(new[] {symbol});
            var qtrEstimate = estimate.FirstOrDefault(x => x.Period.Contains("Qtr"));

            if (earning.EventDate == profile.QtrOneEarningsDate.Value)
            {
                headline = $"{profile.ExchangeName} reports first quarter earnings of {profile.QtrOneEarnings}";
            }
            else if (earning.EventDate == profile.QtrTwoEarningsDate.Value)
            {
                headline = $"{profile.ExchangeName} reports second quarter earnings of {profile.QtrTwoEarnings}";
            }
            else if (earning.EventDate == profile.QtrThreeEarningsDate.Value)
            {
                headline = $"{profile.ExchangeName} reports third quarter earnings of {profile.QtrThreeEarnings}";
            }
            else
            {
                headline = $"{profile.ExchangeName} reports fourth quarter earnings of {profile.QtrFourEarnings}";
            }

            var body = $"{profile.ExchangeName} (<a href=\"{_companyUrl + profile.Symbol}\">{profile.Exchange}:{profile.Symbol}</a>)" +
                          $" reported on {earning.EventDate.DayOfWeek}, {earning.EventDate : MMMM dd} earnings of {earning.Value}." +
                          $"Last quarter's earnings was {earnings.ElementAt(1).Value}. Next" +
                          $" quarter earnings are projected to be {qtrEstimate.AverageEstimate}.";

            return new NewsArticle(headline, body, chart.ImageUrl);
        }

        public async Task<NewsArticle> GetDividendAnnouncement(string symbol)
        {
            var profile = await _barchartClient.GetProfiles(symbol);
            var chart = await _barchartClient.GetChart(symbol);
            var earning = await _barchartClient.GetCorporateActions(new[] { symbol }, null, null, EventType.earnings);
            var dividends = await _barchartClient.GetCorporateActions(new[] {symbol}, null, null, EventType.dividend);
            var estimate = await _barchartClient.GetEarningsEstimates(new[] {symbol},
                new[] {"dividendType", "declarationDate", "paymentDate"});
            var currentEstimate = estimate.FirstOrDefault(x => x.PaymentDate != null);

            var headline = $"{profile.ExchangeName} announces dividends of {dividends.FirstOrDefault().Value}";
            var body = $"{profile.ExchangeName} (<a href=\"{_companyUrl + profile.Symbol}\">{profile.Exchange}:{profile.Symbol}</a>)" +
                          $" announced on {currentEstimate.DeclarationDate.Value.DayOfWeek}, {currentEstimate.DeclarationDate.Value: MMMM dd}" +
                          $" dividends of {dividends.FirstOrDefault().Value} to be paid as {currentEstimate.DividendType.ToLower()} on" +
                          $" {currentEstimate.PaymentDate.Value.DayOfWeek}, {currentEstimate.PaymentDate.Value: MMMM dd}. Earnings this quarter was" +
                          $" {earning.FirstOrDefault().Value}, compared to last quarters {earning.ElementAt(1).Value}.";

            return new NewsArticle(headline, body, chart.ImageUrl);
        }

        //public async Task<NewsArticle> GetStockPerformance(string symbol)
        //{
        //    var profile = await _barchartClient.GetProfiles(symbol);
        //    var chart = await _barchartClient.GetChart(symbol);
        //    var quoteEod = await _barchartClient.GetQuoteEod(symbol);
        //}

        //public async Task<NewsArticle> GetTechnicalTrading(string symbol)
        //{
        //    var profile = await _barchartClient.GetProfiles(symbol);
        //    var chart = await _barchartClient.GetChart(symbol);
        //}
    }
}