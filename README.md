# HTMX + ASP.NET Core Razor "Fragments" 

This is an attempt to implement some sort of Fragment support using ASP.NET Core Razor as described in the HTMX essay [Template Fragments](https://htmx.org/essays/template-fragments/).

## Usage

The goal is to have a master and detail view and host them within the same `cshtml` file.

First, we define some models:

```csharp
public class FragmentModel
{
  public FragmentModel(
    string fragmentId
  )
  {
    FragmentId = fragmentId;
  }

  public string FragmentId { get; set; }
}

public class ChildModel : FragmentModel
{
  public int Id { get; }

  public ChildModel(
    int id
  ) : base("Detail")
  {
    Id = id;
  }
}

public class ParentModel : FragmentModel
{
  public List<ChildModel> Childs { get; }

  public ParentModel(
    List<ChildModel> childs
  ) : base("Full")
  {
    Childs = childs;
  }
}
```

Then we define a View file:

```csharp
@model RazorFragments.Models.FragmentModel

@{
  ViewBag.Title = "title";
  Layout = "_Layout";
}


@{
  void RenderDetail(
    ChildModel child)
  {
    <div class="bg-gray-200">
      @Html.ActionLink(child.Id.ToString(), "Detail", new
      {
        id = child.Id
      })
    </div>
  }

  void RenderFull(
    ParentModel parent)
  {
    <div class="bg-blue-500 p-4">
      @{
        @foreach (var parentChild in parent.Childs)
        {
          RenderDetail(parentChild);
        }
      }
    </div>
  }
}


@{
  switch (Model.FragmentId)
  {
    case "Full":
      RenderFull(Model as ParentModel);
      break;
    case "Detail":
      RenderDetail(Model as ChildModel);
      break;
  }
}
```

And everything gets tied together in our controller:

```csharp
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
```

## Starting the sample

```bash
yarn
cd src/RazorFragments
yarn
libman restore
cd ../..
yarn start
```
Browse <https://localhost:5001/razorfragments>

If you want to become native fragment support part of Razor, please support this [issue](https://github.com/dotnet/aspnetcore/issues/43713) on GitHub.