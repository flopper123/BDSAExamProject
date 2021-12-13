namespace LitExplore.Tests.Controllers.Graph;

public class GraphTestData
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

    var pub0 = new PublicationDto { Title = "Test publication", References = new HashSet<PublicationTitle> { refs[0], refs[1], refs[2], refs[3] } };

    // Pub1 and pub0 share same title
    var pub1 = new PublicationDto { Title = "Test publication", References = new HashSet<PublicationTitle> { refs[1], refs[2], refs[3] } };

    // Pub1 and pub2 share the same references
    var pub2 = new PublicationDto { Title = "Test publication2", References = new HashSet<PublicationTitle> { refs[1], refs[2], refs[3] } };

    var pub3 = new PublicationDto { Title = "Test publication3", References = new HashSet<PublicationTitle> { refs[1], refs[2], refs[4] } };
    var pub4 = new PublicationDto { Title = "Test publication4", References = new HashSet<PublicationTitle> { refs[2], refs[3], refs[4] } };
    var pub5 = new PublicationDto { Title = "Test publication5", References = new HashSet<PublicationTitle> { refs[3], refs[4] } };
    var pub6 = new PublicationDto { Title = "Test publication6", References = new HashSet<PublicationTitle> { refs[4] } };
    var pub7 = new PublicationDto { Title = "Test publication7", References = new HashSet<PublicationTitle>() };

    return new List<PublicationDto> { pub0, pub1, pub2, pub3, pub4, pub5, pub6, pub7 };
  }

}