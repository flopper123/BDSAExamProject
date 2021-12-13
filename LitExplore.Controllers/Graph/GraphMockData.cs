namespace LitExplore.Controllers.Graph;

using LitExplore.Core;

/**
 * ! REMOVE THIS FILE AND UPDATE GRAPH CONTROLLER ONCE REPOS AND GRAPH IS FULLY SETUP
 */
public class GraphMockData
{

  public static List<PublicationTitle> GetReferences() {
    var ref0 = new PublicationTitle() { Title = "Test reference0" };
    var ref1 = new PublicationTitle() { Title = "Test reference1" };
    var ref2 = new PublicationTitle() { Title = "Test reference2" };
    var ref3 = new PublicationTitle() { Title = "Test reference3" };
    var ref4 = new PublicationTitle() { Title = "Test reference4" };
    return new List<PublicationTitle> { ref0, ref1, ref2, ref3, ref4 };
  }
  
  public static List<PublicationDto> GetPublications() {

    var refs = GetReferences(); 

    var pub0 = new PublicationDto { Title = "Advanced ML in Denmark", References = new HashSet<PublicationTitle> { refs[0], refs[1], refs[2] } };

    var pub1 = new PublicationDto { Title = "CS speedup using downloadable RAM", References = new HashSet<PublicationTitle> { refs[1], refs[2]  } };

    var pub2 = new PublicationDto { Title = "How SKRRRT may outperform google lmao", References = new HashSet<PublicationTitle> { refs[1], refs[2] } };

    var pub3 = new PublicationDto { Title = "idk what to write anymore", References = new HashSet<PublicationTitle> { refs[1], refs[2] } };
    var pub4 = new PublicationDto { Title = "yeeeet", References = new HashSet<PublicationTitle> { refs[3], refs[4] } };
    var pub5 = new PublicationDto { Title = "maybe just skrrrt", References = new HashSet<PublicationTitle> { refs[3], refs[4] } };
    var pub6 = new PublicationDto { Title = "whats 9 + 10?", References = new HashSet<PublicationTitle> { refs[4] } };
    var pub7 = new PublicationDto { Title = "gg go next", References = new HashSet<PublicationTitle>() };

    return new List<PublicationDto> { pub0, pub1, pub2, pub3, pub4, pub5, pub6, pub7 };
  }

}