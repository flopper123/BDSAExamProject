using Xunit;
using LitExplore.Core.Graph;
using LitExplore.Tests.Entity;

namespace LitExplore.Tests.Core.Graph;

public class GraphHanderTests : AbsRepositoryTests<PublicationRepository>
{
    PublicationGraphHandeler _publicationGraphHandeler;
    public GraphHanderTests() : base()
    {
        _publicationGraphHandeler = new PublicationGraphHandeler(repository);
    }
    protected override void seed()
    {
        Reference ref1 = new Reference { Title = "Test pub 1" };
        Reference ref2 = new Reference { Title = "Test pub 2" };

        context.References.AddRange(
            ref1, ref2
        );

        Publication pub1 = new Publication { Title = "Test pub 1", References = new List<Reference> { ref2 } };

        context.Publications.AddRange(
          pub1,
          new Publication { Title = "Test pub 2", References = new List<Reference> { ref1 } },
          new Publication { Title = "Test pub 3", References = new List<Reference> { ref1, ref2 } }
        );
    }

    [Fact]
    public void buildsFromRootTitle()
    {
        // Arrange
        var expected = 3;
        // Act
        _publicationGraphHandeler.DefineRoot("Test pub 1");
        // Assert
        Assert.Equal(expected, (int)_publicationGraphHandeler.GetGraph().Size);
    }
    // [Fact]
    // public void TestName()
    // {
    //     // Act
    //     // Arrange
    //     // Assert
    // }
    // [Fact]
    // public void TestName()
    // {
    //     // Act
    //     // Arrange
    //     // Assert
    // }
    // [Fact]
    // public void TestName()
    // {
    //     // Act
    //     // Arrange
    //     // Assert
    // }
    // [Fact]
    // public void TestName()
    // {
    //     // Act
    //     // Arrange
    //     // Assert
    // }
    // [Fact]
    // public void TestName()
    // {
    //     // Act
    //     // Arrange
    //     // Assert
    // }
    // [Fact]
    // public void TestName()
    // {
    //     // Act
    //     // Arrange
    //     // Assert
    // }
    // [Fact]
    // public void TestName()
    // {
    //     // Act
    //     // Arrange
    //     // Assert
    // }
}