
namespace LitExplore.UI.Pages.Authorized.Home.Components.Graph;

using LitExplore.Core.Publication;
using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;
using LitExplore.Controllers.Graph;

public class FilterEvent
{
  public delegate void OnUpdate(Filter<PublicationGraph> filter);
  private static List<OnUpdate> subscribers = new List<OnUpdate>();

  public static void Notify(Filter<PublicationGraph> filter) => subscribers.ForEach(sub => sub(filter));
  public static void AddSub(OnUpdate subscriber) => subscribers.Add(subscriber);

}