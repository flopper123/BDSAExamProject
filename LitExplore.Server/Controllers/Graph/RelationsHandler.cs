
namespace LitExplore.Server.Controllers.Graph;

using LitExplore.Core;


public class RelationsHandler : List<(PublicationDto pub, double factor)>
{
  public static RelationsHandler FromList(List<(PublicationDto, double)> list) {
    RelationsHandler rh = new RelationsHandler();
    rh.AddRange(list);
    return rh; 
  }

}