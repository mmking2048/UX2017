using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using UX2017.Models;
using UX2017.Models.SplitsDividendsAndEarnings;

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
                ?.ElementAt(0);
            ViewBag.FinancialHighlight = (await _barchartClient.GetFinancialHighlights(new[] {symbol},
                    new[] {"beta", "peRatio", "ttmEPS"}))
                ?.ElementAt(0);
            ViewBag.Estimate = (await _barchartClient.GetEarningsEstimates(new[] {symbol},
                    new[] {"exDividendDate", "dividendRate"}))
                ?.ElementAt(0);
            ViewBag.Chart = await _barchartClient.GetChart(symbol);
            ViewBag.NewsArticles = (await _barchartClient.GetNews(symbol))
                .Take(10)
                .Select(n => new NewsArticle(n.NewsID, n.Headline, n.Preview));

            var earnings = await _barchartClient.GetCorporateActions(new[] {symbol}, null, null, EventType.earnings, 5);
            var dividends = await _barchartClient.GetCorporateActions(new[] { symbol }, null, null, EventType.dividend, 5);

            if (dividends == null)
            {
                ViewBag.Earnings = earnings.Select(e => new EarningsData {Earnings = e});
            }
            else if (earnings == null)
            {
                ViewBag.Earnings = dividends.Select(d => new EarningsData {Dividends = d});
            }
            else
            {
                ViewBag.Earnings = earnings.Join(dividends, e => e.EventDate.Month, d => d.EventDate.Month,
                    (e, d) => new EarningsData {Earnings = e, Dividends = d});
            }

            return View("Profile");
        }
    }

    public class EarningsData
    {
        public CorporateAction Earnings { get; set; }
        public CorporateAction Dividends { get; set; }
    }
}