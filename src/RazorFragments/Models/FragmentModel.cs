namespace RazorFragments.Models;

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
