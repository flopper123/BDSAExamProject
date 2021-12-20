
namespace LitExplore.UI.Pages.Authorized.Home.Components.Graph;

using LitExplore.Controllers.Graph;

public class SelectedNodeEvent
{
  public delegate void OnUpdate(VisualGraphRelationNode node);
  private static List<OnUpdate> subscribers = new List<OnUpdate>();

  public static void Notify(VisualGraphRelationNode node) => subscribers.ForEach(sub => sub(node));
  public static void AddSub(OnUpdate subscriber) => subscribers.Add(subscriber);

  public static (double x, double y) ToVisualPoint((double x, double y) point)
  {
    return (
      GetPercentage(point.x),
      GetPercentage(point.y)
    );
  }

  public static double GetPercentage(double factor)
  {
    factor = Math.Min(Math.Max(factor, 0.1), 0.9);
    double padding = 20;
    return Math.Floor((factor * (100 - (2 * padding))) + padding);
  }

}