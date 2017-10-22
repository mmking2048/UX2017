using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using UX2017.Models;

namespace UX2017.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IBarchartClient _barchartClient =
            new BarchartClient(new HttpClient(), new JsonParser());

        public async Task<ActionResult> GetProfile(string symbol)
        {
            ViewBag.Profile = await _barchartClient.GetProfiles(symbol);
            ViewBag.Quote = await _barchartClient.GetQuote(symbol);
            ViewBag.Chart = await _barchartClient.GetChart(symbol);
            ViewBag.NewsArticles = (await _barchartClient.GetNews(symbol))
                .Take(10)
                .Select(n => new NewsArticle(n.NewsID, n.Headline, n.Preview));

            return View("Profile");
        }
    }
}