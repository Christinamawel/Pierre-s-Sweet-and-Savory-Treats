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

    public FlavorsController(PierresContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.OrderBy(x => x.Name).ToList();
      return View(model);
    }
  }
}