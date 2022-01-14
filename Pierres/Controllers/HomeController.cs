using Microsoft.AspNetCore.Mvc;

namespace Pierres.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}