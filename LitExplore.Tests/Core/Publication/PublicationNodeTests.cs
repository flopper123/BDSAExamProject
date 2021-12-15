namespace LitExplore.Tests.Core.Publication;

using static PublicationGraphTests;

public class PublicationNodeTests {
    
    static internal PublicationNode NewTitledNode(string title) {
        return new PublicationNode(new PublicationDtoDetails { Title = title });
    }

    private static IEnumerable<object[]> UpdateDetails() {
        yield return new object[] {
            // Target
            new PublicationDtoDetails(), 
            // Update
            new PublicationDtoDetails { Title = "0xDEADBEEF" },
            // Expected
            new PublicationDtoDetails { Title = "0xDEADBEEF" },
        };

        yield return new object[] {
            new PublicationDtoDetails { Title = "1xDEADBEEF", References = GetHashSet("1")},
            new PublicationDtoDetails { Title = "1xDEADBEEF", Author = "BDSA" },
            new PublicationDtoDetails { Title = "1xDEADBEEF", Author = "BDSA", References = GetHashSet("1")},
        };

        
        yield return new object[] {
            new PublicationDtoDetails { Title = "2xDEADBEEF", Keywords = (new List<string> { "2" }).AsReadOnly(), 
                                        References = GetHashSet("1") },
            new PublicationDtoDetails { Title = "2xDEADBEEF", Keywords = (new List<string> { "3", "4", "5" }).AsReadOnly() },
            new PublicationDtoDetails { Title = "2xDEADBEEF", Keywords = (new List<string> { "3", "4", "5" }).AsReadOnly(), 
                                        References = GetHashSet("1")},
        };

        yield return new object[] {
            new PublicationDtoDetails { Title = "3xDEADBEEF", Keywords = (new List<string> { "3", "4", "5" }).AsReadOnly(), 
                                        References = GetHashSet("1") },
            new PublicationDtoDetails { Title = "3xDEADBEEF", Keywords = (new List<string> { "2" }).AsReadOnly() },
            new PublicationDtoDetails { Title = "3xDEADBEEF", Keywords = (new List<string> { "3", "4", "5" }).AsReadOnly(), 
                                        References = GetHashSet("1")},
        };
    }

    [Theory]
    [InlineData("0xDEADBEEF")]
    [InlineData("0xDEADBEEF ")]
    [InlineData("")]
    public void Equals_Title_IfSame(string title) {
        PublicationDtoDetails t = new PublicationDtoDetails { Title = title };
        PublicationNode act = new PublicationNode(t);
        Assert.True(act.Equals(t));
    }

    [Theory]
    [InlineData("0xDEADBEEF")]
    [InlineData("0xDEADBEEF ")]
    [InlineData("1111")]
    public void Not_Equals_Title_IfDifferent(string title)
    {
        PublicationDtoDetails t = new PublicationDtoDetails { Title = title };
        PublicationNode act = new PublicationNode(t);
        PublicationDtoDetails t_modified = new PublicationDtoDetails { Title = title.Substring(1) };
        Assert.False(act.Equals(t_modified));
    }

    [Theory]
    [MemberData(nameof(UpdateDetails))]
    public void Update_Only_NewDetails(PublicationDtoDetails tar, 
                                       PublicationDtoDetails update, 
                                       PublicationDtoDetails exp) 
    {
        PublicationNode node = new PublicationNode(tar);
        node.UpdateDetails(update);
        Assert.True(node.Details.CustomEquals(exp));
    }

    [Fact]
    public void CanAddToChild() {
        PublicationNode n = NewTitledNode("0xDEADBEEF");
        Assert.Empty(n.Children);
        var toAdd = NewTitledNode("1xDEADBEEF");
        n.Children.Add(toAdd);
        Assert.Single(n.Children);
        Assert.Contains(toAdd, n.Children);
    }

    [Fact]
    public void CanAddToParent() {
        PublicationNode n = NewTitledNode("0xDEADBEEF");
        Assert.Empty(n.Parents);
    
        var toAdd = NewTitledNode("1xDEADBEEF");
        n.Parents.Add(toAdd);
        Assert.Single(n.Parents);
        Assert.Contains(toAdd, n.Parents);
    }

    [Fact]
    public void CanRemoveFromChild() {
        PublicationNode n = NewTitledNode("0xDEADBEEF");
        var addition = NewTitledNode("1xDEADBEEF");

        n.Children.Add(addition);
        n.Children.Remove(addition);
        Assert.Empty(n.Children);
        Assert.DoesNotContain(addition, n.Children);
    }

    [Fact]
    public void CanRemoveFromParent() {
        PublicationNode n = NewTitledNode("0xDEADBEEF");
        var addition = NewTitledNode("1xDEADBEEF");

        n.Parents.Add(addition);
        n.Parents.Remove(addition);
        Assert.Empty(n.Parents);
        Assert.DoesNotContain(addition, n.Parents);
    }

    [Fact]
    public void CanFilterChildren() {
        throw new NotImplementedException();
    }
}