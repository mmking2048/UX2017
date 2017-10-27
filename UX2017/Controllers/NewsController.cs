using System;
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
        // TODO: dynamically generate list of symbols (GetCompetitors)
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
            article.Body += _newsGenerator.GetFinancialSummary(symbol);

            ViewBag.News = article;
            ViewBag.RelatedStocks = await _barchartClient.GetQuote(_symbols);
            ViewBag.NewsArticles = (await _barchartClient.GetNews(symbol))
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

        private async Task<string> GetSymbol(NewsArticle news)
        {
            // TODO: Use TextRazor on news body, look in topics for associated company?

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