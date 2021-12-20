
namespace LitExplore.UI.Pages.Authorized.Home.Components.Graph;

using LitExplore.Controllers.Graph;

public class SelectedNodeEvent
{
  public delegate void OnUpdate(VisualGraphRelationNode node);
  private static List<OnUpdate> subscribers = new List<OnUpdate>();

  public static void Notify(VisualGraphRelationNode node) => subscribers.ForEach(sub => sub(node));
  public static void AddSub(OnUpdate subscriber) => subscribers.Add(subscriber);

}