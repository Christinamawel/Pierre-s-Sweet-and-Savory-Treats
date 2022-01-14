using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Pierres.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pierres.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly PierresContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, PierresContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.OrderBy(x => x.Name).ToList();
      return View(model);
    }

    public ActionResult Details(int id)
    {
      var thisFlavor = _db.Flavors
          .Include(flavor => flavor.JoinEntities)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(string name)
    {
      bool alreadyExists = _db.Flavors.Any(x => x.Name == name);
      if(!alreadyExists)
      {
        Flavor newFlavor = new Flavor();
        newFlavor.Name = name.ToLower();
        _db.Flavors.Add(newFlavor);
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}