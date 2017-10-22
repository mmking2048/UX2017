using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using UX2017.Models;

namespace UX2017.Controllers
{
    public class NewsController : Controller
    {
        private readonly IBarchartClient _barchartClient =
            new BarchartClient(new HttpClient(), new JsonParser());
        private readonly INewsGenerator _newsGenerator =
            new NewsGenerator(new BarchartClient(new HttpClient(), new JsonParser()));

        public async Task<ActionResult> Index()
        {
            var news = (await _barchartClient.GetNews("AAPL")).ElementAt(0);
            ViewBag.News = new NewsArticle(news.NewsID, news.Headline, news.FullText, news.LargeImageUrl);
            return View("News");
        }

        public async Task<ActionResult> News(int newsID)
        {
            var news = await _barchartClient.GetNews(newsID);
            ViewBag.News = new NewsArticle(news.NewsID, news.Headline, news.FullText, news.LargeImageUrl);
            return View("News");
        }

        public async Task<ActionResult> EarningsNews(string symbol = "AAPL")
        {
            ViewBag.News = await _newsGenerator.GetEarningsSummary(symbol);
            return View("News");
        }

        public async Task<ActionResult> DividendsAnnouncement(string symbol = "AAPL")
        {
            ViewBag.News = await _newsGenerator.GetDividendAnnouncement(symbol);
            return View("News");
        }
    }
}