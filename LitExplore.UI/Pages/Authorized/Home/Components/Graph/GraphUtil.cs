
namespace LitExplore.UI.Pages.Authorized.Home.Components.Graph;

public class GraphUtil
{

  public static (double x, double y) ToVisualPoint((double x, double y) point)
  {
    return (
      GetPercentage(point.x),
      GetPercentage(point.y)
    );
  }

  public static double GetPercentage(double factor)
  {
    double padding = 20;
    return Math.Floor((factor * (100 - (2 * padding))) + padding);
  }

}