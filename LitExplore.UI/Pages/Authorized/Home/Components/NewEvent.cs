
namespace LitExplore.UI.Pages.Authorized.Home.Components;

public class NewEvent
{
  public delegate void OnUpdate();
  private static List<OnUpdate> subscribers = new List<OnUpdate>();

  public static void Notify() => subscribers.ForEach(sub => sub());
  public static void AddSub(OnUpdate subscriber) => subscribers.Add(subscriber);

}