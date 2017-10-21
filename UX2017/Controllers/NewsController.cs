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
        private readonly INewsGenerator _generator =
            new NewsGenerator(new BarchartClient(new HttpClient(), new JsonParser()),
                              new WordGenerator(new Random()));

        public async Task<ActionResult> Index()
        {
            var news = (await _barchartClient.GetNews("AAPL")).ElementAt(0);
            ViewBag.News = new NewsArticle(news.Headline, news.FullText);
            return View("News");
        }
    }
}