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
            ViewBag.Quote = (await _barchartClient.GetQuote(new[] {symbol}, new[] {"R"},
                    new[] {"previousClose", "bid", "ask", "fiftyTwoWkLow", "fiftyTwoWkHigh", "avgVolume"}))
                .ElementAt(0);
            ViewBag.FinancialHighlight = (await _barchartClient.GetFinancialHighlights(new[] {symbol},
                    new[] {"beta", "peRatio", "ttmEPS"}))
                .ElementAt(0);
            ViewBag.Estimate = (await _barchartClient.GetEarningsEstimates(new[] {symbol},
                    new[] {"exDividendDate", "dividendRate"}))
                .ElementAt(0);
            ViewBag.Chart = await _barchartClient.GetChart(symbol);
            ViewBag.NewsArticles = (await _barchartClient.GetNews(symbol))
                .Take(10)
                .Select(n => new NewsArticle(n.NewsID, n.Headline, n.Preview));

            return View("Profile");
        }
    }
}