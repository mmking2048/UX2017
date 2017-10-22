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
            var news = (await _barchartClient.GetNews("AAPL")).ElementAt(0);
            ViewBag.News = new NewsArticle(news.NewsID, news.Headline, news.FullText, news.LargeImageUrl);
            ViewBag.RelatedStocks = await _barchartClient.GetQuote(_symbols);
            return View("News");
        }

        public async Task<ActionResult> News(int newsID)
        {
            var news = await _barchartClient.GetNews(newsID);
            var article = new NewsArticle(news.NewsID, news.Headline, news.FullText, news.LargeImageUrl);
            ViewBag.News = article;
            ViewBag.RelatedStocks = await _barchartClient.GetQuote(_symbols);
            ViewBag.NewsArticles = (await _barchartClient.GetNews(GetSymbol(article)))
                .Where(n => n.NewsID != newsID)
                .Take(3)
                .Select(n => new NewsArticle(n.NewsID, n.Headline, n.Preview));
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

        private string GetSymbol(NewsArticle news)
        {
            foreach (var symbol in _symbols)
            {
                if (news.Headline.Contains(symbol))
                {
                    return symbol;
                }
            }

            foreach (var symbol in _symbols)
            {
                if (news.Body.Contains(symbol))
                {
                    return symbol;
                }
            }

            return "AAPL";
        }
    }
}