using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Pierres.Models;
using System.Threading.Tasks;

namespace Pierres.Controllers
{
  public class AccountController : Controller
  {
    private readonly PierresContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, PierresContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }
  }
}