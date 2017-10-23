using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using UX2017.Models;

namespace UX2017.Controllers
{
    public class NewsController : Controller
    {
        private readonly IEnumerable<string> _symbols = new []{"AAPL", "GOOGL", "MSFT", "IBM"};
        private readonly IBarchartClient _barchartClient =
            new BarchartClient(new HttpClient(), new JsonParser());
        private readonly INewsGenerator _newsGenerator =
            new NewsGenerator(new BarchartClient(new HttpClient(), new JsonParser()));

        public async Task<ActionResult> Index()
        {
            var news = (await _barchartClient.GetNews("AAPL"))?.ElementAt(0);
            ViewBag.News = new NewsArticle(news.NewsID, news.Headline, news.FullText, news.LargeImageUrl);
            ViewBag.RelatedStocks = await _barchartClient.GetQuote(_symbols);
            return View("News");
        }

        public async Task<ActionResult> News(int newsID)
        {
            var news = await _barchartClient.GetNews(newsID);
            var article = new NewsArticle(news.NewsID, news.Headline, news.FullText, news.LargeImageUrl);
            var symbol = await GetSymbol(article);

            var profile = await _barchartClient.GetProfiles(symbol);
            var quote = (await _barchartClient.GetQuote(new[]{symbol}, new[]{"D"},
                new []{"previousClose", "avgVolume", "averageWeeklyVolume"}))
                ?.ElementAt(0);
            var technical = (await _barchartClient.GetTechnicals(new[] {symbol},
                new[] {"priceChangeYTD"}))?.ElementAt(0);
            var earning = await _barchartClient.GetCorporateActions(symbol, EventType.earnings);
            var dividend = await _barchartClient.GetCorporateActions(symbol, EventType.dividend);
            var estimate = await _barchartClient.GetEarningsEstimates(symbol);

            ViewBag.News = article;
            ViewBag.RelatedStocks = await _barchartClient.GetQuote(_symbols);
            ViewBag.NewsArticles = (await _barchartClient.GetNews(symbol))
                .Where(n => n.NewsID != newsID)
                .Take(3)
                .Select(n => new NewsArticle(n.NewsID, n.Headline, n.Preview));

            var addition = $"<h4>About {profile.ExchangeName}</h4>" +
                           $"<ul><li>{symbol} closed at ${quote.Close}, up {quote.PercentChange}% from last trading day close of ${quote.PreviousClose}.</li>" +
                           $"<li>{symbol} had a volume of {quote.Volume}, 77.67% below <Calculation> the year-to day volume of {quote.AvgVolume}." +
                           $" The average volume over the last five days ({quote.AverageWeeklyVolume}) is down 74.10% <Calculation>compared to the average.</li>" +
                           $"<li>{symbol} has changed {technical.PriceChangeYtd}% since the start of the year.</li>" +
                           $"<li>{symbol} earning {earning.Value} is XXX% <Calculation> than {estimate.AverageEstimate} as estimated.</li>" +
                           $"{(dividend != null ? $"<li>{symbol} last dividend was {dividend.Value}.</li></ul>" : "")}";

            article.Body += addition;

            return View("News");
        }

        public async Task<ActionResult> EarningsNews(string symbol = "AAPL")
        {
            ViewBag.News = await _newsGenerator.GetEarningsSummary(symbol);
            ViewBag.RelatedStocks = await _barchartClient.GetQuote(_symbols);
            return View("News");
        }

        public async Task<ActionResult> DividendsAnnouncement(string symbol = "AAPL")
        {
            ViewBag.News = await _newsGenerator.GetDividendAnnouncement(symbol);
            ViewBag.RelatedStocks = await _barchartClient.GetQuote(_symbols);
            return View("News");
        }

        private async Task<string> GetSymbol(NewsArticle news)
        {
            var companies = (await _barchartClient.GetProfiles(_symbols)).ToArray();
            var upperHeadline = news.Headline.ToUpper();
            var upperBody = news.Body.ToUpper();

            foreach (var company in companies)
            {
                var companyName = company.ExchangeName.Split(' ')[0];
                
                if (upperHeadline.Contains(companyName.ToUpper())
                    || upperHeadline.Contains(company.Symbol.ToUpper()))
                {
                    return company.Symbol;
                }
            }

            foreach (var company in companies)
            {
                var companyName = company.ExchangeName.Split(' ')[0];
                if (upperBody.Contains(companyName.ToUpper())
                    || upperBody.Contains(company.Symbol.ToUpper()))
                {
                    return company.Symbol;
                }
            }

            return "AAPL";
        }
    }
}