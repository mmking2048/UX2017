using System.Web.Mvc;

namespace UX2017.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsGenerator _generator = new NewsGenerator();

        public ActionResult Index()
        {
            ViewBag.News = _generator.GetEarningsNews();
            return View("News");
        }
    }
}