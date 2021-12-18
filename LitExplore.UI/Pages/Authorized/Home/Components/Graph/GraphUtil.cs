
namespace LitExplore.UI.Pages.Authorized.Home.Components.Graph;

public class GraphUtil
{
  public delegate void OnUpdate();
  private static List<OnUpdate> subscribers = new List<OnUpdate>();

  public static void Notify() => subscribers.ForEach(sub => sub());
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