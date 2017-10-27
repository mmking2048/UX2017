using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using UX2017.Models;

namespace UX2017.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBarchartClient _barchartClient =
            new BarchartClient(new HttpClient(), new JsonParser());

        private readonly ITextRazorClient _textRazorClient = new TextRazorClient(new HttpClient());

        private readonly string _symbol = "AAPL";

        public async Task<ActionResult> Index()
        {
            ViewBag.NewsArticles = (await _barchartClient.GetNews(_symbol)).Select(n => new NewsArticle(n.NewsID, n.Headline, n.Preview));
            return View();
        }

        public async Task<ActionResult> News(string symbol)
        {
            ViewBag.NewsArticles = (await _barchartClient.GetNews(symbol)).Select(n => new NewsArticle(n.NewsID, n.Headline, n.Preview));
            return View("Index");
        }
    }
}