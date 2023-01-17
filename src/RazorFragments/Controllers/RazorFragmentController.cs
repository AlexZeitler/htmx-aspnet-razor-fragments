using Microsoft.AspNetCore.Mvc;
using RazorFragments.Models;

namespace RazorFragments.Controllers;

public class RazorFragmentController : Controller
{
  // GET
  public IActionResult Index()
  {
    return View(
      new ParentModel(
        new List<ChildModel>()
        {
          new(1),
          new(2)
        }
      )
    );
  }

  [Route("/detail/{id}")]
  public IActionResult Detail(
    [FromRoute] int id
  )
  {
    return View("Index", new ChildModel(id));
  }
}
