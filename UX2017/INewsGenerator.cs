using System.Threading.Tasks;
using UX2017.Models;

namespace UX2017
{
    public interface INewsGenerator
    {
        //Task<News> GetEarningsNews(string symbol);
        //Task<News> GetDividendsNews(string symbol);
    }

    public class NewsGenerator : INewsGenerator
    {
        private readonly IBarchartClient _barchartClient;
        private readonly IWordGenerator _wordGenerator;

        public NewsGenerator(IBarchartClient barchartClient, IWordGenerator wordGenerator)
        {
            _barchartClient = barchartClient;
            _wordGenerator = wordGenerator;
        }

        //public async Task<News> GetEarningsNews(string symbol)
        //{
        //    var profile = await _barchartClient.GetProfiles(symbol);
        //    var quoteEod = await _barchartClient.GetQuoteEod(symbol);
        //    var incomeStatement = await _barchartClient.GetIncomeStatements(symbol, Frequency.Annual);

        //    var stockIncreased = quoteEod.Close > quoteEod.Open;

        //    var headline = $"{profile.Symbol} stock {(stockIncreased ? _wordGenerator.GetIncrease() : _wordGenerator.GetDecrease())}" +
        //                   $" following earnings report";

        //    return new News(headline, "New tree found");
        //}

        //public async Task<News> GetDividendsNews(string symbol)
        //{
        //    var profile = await _barchartClient.GetProfiles(symbol);
        //    var dividends = await _barchartClient.GetCorporateActions(symbol, EventType.dividend);

        //    var headline = $"{profile.ExchangeName} declares ${dividends.Value} dividends";

        //    var body = $"{profile.ExchangeName} ({profile.Exchange}: {profile.Symbol}) declares ${dividends.Value} per share dividend. \n\n" +
        //               $"About {profile.ExchangeName}\n\n" +
        //               $"{profile.BusinessSummary}";

        //    return new News(headline, body);
        //}
    }
}