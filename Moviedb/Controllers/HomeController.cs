using System.Web.Mvc;

namespace Moviedb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Movies");
        }
    }
}