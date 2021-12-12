namespace LitExplore.Tests.Server.Controllers.Graph;

using LitExplore.Server.Controllers.Graph;

public class GraphTestData
{

  public static List<ReferenceDto> GetReferences() {
    var ref0 = new ReferenceDto() { Title = "Test reference0" };
    var ref1 = new ReferenceDto() { Title = "Test reference1" };
    var ref2 = new ReferenceDto() { Title = "Test reference2" };
    var ref3 = new ReferenceDto() { Title = "Test reference3" };
    var ref4 = new ReferenceDto() { Title = "Test reference4" };
    return new List<ReferenceDto> { ref0, ref1, ref2, ref3, ref4 };
  }
  public static List<PublicationDto> GetPublications() {

    var refs = GetReferences(); 

    var pub0 = new PublicationDto { Title = "Test publication", References = new HashSet<ReferenceDto> { refs[0], refs[1], refs[2], refs[3] } };

    // Pub1 and pub0 share same title
    var pub1 = new PublicationDto { Title = "Test publication", References = new HashSet<ReferenceDto> { refs[1], refs[2], refs[3] } };

    // Pub1 and pub2 share the same references
    var pub2 = new PublicationDto { Title = "Test publication2", References = new HashSet<ReferenceDto> { refs[1], refs[2], refs[3] } };

    var pub3 = new PublicationDto { Title = "Test publication3", References = new HashSet<ReferenceDto> { refs[1], refs[2], refs[4] } };
    var pub4 = new PublicationDto { Title = "Test publication4", References = new HashSet<ReferenceDto> { refs[2], refs[3], refs[4] } };
    var pub5 = new PublicationDto { Title = "Test publication5", References = new HashSet<ReferenceDto> { refs[3], refs[4] } };
    var pub6 = new PublicationDto { Title = "Test publication6", References = new HashSet<ReferenceDto> { refs[4] } };
    var pub7 = new PublicationDto { Title = "Test publication7", References = new HashSet<ReferenceDto>() };

    return new List<PublicationDto> { pub0, pub1, pub2, pub3, pub4, pub5, pub6, pub7 };
  }

}