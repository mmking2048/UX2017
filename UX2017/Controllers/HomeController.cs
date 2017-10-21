using System.Web.Mvc;

namespace UX2017.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            // TODO: display list of already generated news articles
            // To be done when NewsFetcher is implemented
            return View();
        }
    }
}