using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Pierres.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Pierres.Controllers
{ 
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierresContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userTreats.OrderByDescending(x => x.Rating));
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat Treat, int CategoryId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Treat.User = currentUser;
      _db.Treats.Add(Treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit (int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treat => Treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat Treat)
    {
      _db.Entry(Treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult ShareTreat(int treatId)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treat => Treat.TreatId == treatId);
      thisTreat.Shared = !thisTreat.Shared;
      _db.Entry(thisTreat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = thisTreat.TreatId});
    }

    public ActionResult AddFlavor(int id, string str)
    {
      // create error message to show on view with str
      ViewBag.Same = str;
      var thisTreat = _db.Treats
          .Include(Treat => Treat.JoinEntities)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(Treats => Treats.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat Treat, int FlavorId)
    {
      // check if the flavor has already been added
      bool alreadyExists = _db.FlavorTreat.Any(FlavorTreat => FlavorTreat.FlavorId == FlavorId && FlavorTreat.TreatId == Treat.TreatId);
      if (FlavorId != 0 && !alreadyExists)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = Treat.TreatId });
      }
      _db.SaveChanges();
      if(alreadyExists)
      {
        return RedirectToAction("AddFlavor", new { id = Treat.TreatId, str = "This Treat already contains that Flavor" });
      }
      return RedirectToAction("Details", new { id = Treat.TreatId});
    }

    public ActionResult Details (int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treat => Treat.TreatId == id);
      return View(thisTreat);
    }

    public ActionResult Delete (int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int joinId, int treatId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = treatId });
    }
  }
}