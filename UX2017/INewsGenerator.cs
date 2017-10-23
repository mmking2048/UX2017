using System.Linq;
using System.Threading.Tasks;
using UX2017.Models;

namespace UX2017
{
    public interface INewsGenerator
    {
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

        public async Task<NewsArticle> GetEarningsSummary(string symbol)
        {
            string headline;
            string body;

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

            body = $"{profile.ExchangeName} (<a href=\"{_companyUrl + profile.Symbol}\">{profile.Exchange}:{profile.Symbol}</a>)" +
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