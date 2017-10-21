using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UX2017.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsGenerator _generator =
            new NewsGenerator(new BarchartClient(new HttpClient(), new JsonParser()),
                              new WordGenerator(new Random()));

        public async Task<ActionResult> Index()
        {
            ViewBag.News = await _generator.GetEarningsNews("AAPL");
            return View("News");
        }
    }
}