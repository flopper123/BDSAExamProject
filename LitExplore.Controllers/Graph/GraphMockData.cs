namespace LitExplore.Controllers.Graph;

public class GraphMockData
{
    
  public static List<PublicationDtoTitle> GetReferences() {
    var ref0 = new PublicationDtoTitle() { Title = "gg go next" };
    var ref1 = new PublicationDtoTitle() { Title = "whats 9 + 10?" };
    var ref2 = new PublicationDtoTitle() { Title = "maybe just skrrrt" };
    var ref3 = new PublicationDtoTitle() { Title = "yeeeet" };
    var ref4 = new PublicationDtoTitle() { Title = "idk what to write anymore" };

    return new List<PublicationDtoTitle> { ref0, ref1, ref2, ref3, ref4 };
  }

  public static List<PublicationDtoDetails> GetPublications() {

    var refs = GetReferences(); 

    var pub0 = new PublicationDtoDetails { Title = "Advanced ML in Denmark", References = new HashSet<PublicationDtoTitle> { refs[0], refs[1], refs[2] } };
    var pub1 = new PublicationDtoDetails { Title = "CS speedup using downloadable RAM", References = new HashSet<PublicationDtoTitle> { refs[1], refs[2]  } };
    var pub2 = new PublicationDtoDetails { Title = "How SKRRRT may outperform google lmao", References = new HashSet<PublicationDtoTitle> { refs[1], refs[2] } };
    var pub3 = new PublicationDtoDetails { Title = "idk what to write anymore", References = new HashSet<PublicationDtoTitle> { refs[1], refs[2] } };
    var pub4 = new PublicationDtoDetails { Title = "yeeeet", References = new HashSet<PublicationDtoTitle> { refs[3], refs[4] } };
    var pub5 = new PublicationDtoDetails { Title = "maybe just skrrrt", References = new HashSet<PublicationDtoTitle> { refs[3], refs[4] } };
    var pub6 = new PublicationDtoDetails { Title = "whats 9 + 10?", References = new HashSet<PublicationDtoTitle> { refs[4] } };
    var pub7 = new PublicationDtoDetails { Title = "gg go next", References = new HashSet<PublicationDtoTitle>() };

    return new List<PublicationDtoDetails> { pub0, pub1, pub2, pub3, pub4, pub5, pub6, pub7 };
  }

}