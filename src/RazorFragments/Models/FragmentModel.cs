namespace RazorFragments.Models;

public abstract record FragmentModel
{
  private FragmentModel() { } //Block additional subtyping

  public record Child(int Id) : FragmentModel;
  public record Parent(List<Child> Childs) : FragmentModel;
}
