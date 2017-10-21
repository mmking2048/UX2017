using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UX2017.Models;

namespace UX2017
{
    public interface INewsGenerator
    {
        Task<News> GetEarningsNews(string symbol);
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

        public async Task<News> GetEarningsNews(string symbol)
        {
            var profile = (await _barchartClient.GetProfile(new[] {symbol})).First();
            //var earnings = (await _barchartClient.GetFinancialHighlights(new[] {symbol})).First();

            var stockIncreased = true;

            var headline = $"{profile.Symbol} stock {(stockIncreased ? _wordGenerator.GetIncrease() : _wordGenerator.GetDecrease())}" +
                           $" following earnings report";

            return new News
            {
                Headline = headline,
                Body = "New tree found"
            };
        }
    }
}