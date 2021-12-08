using Xunit;
using LitExplore.Core.Graph;

namespace LitExplore.Tests.Core.Graph;

public class GraphTests
{
    // Contains test data for vertices from 0 to N
    IGraph<int> src = null!;
    const UInt32 N = 100;

    public GraphTests()
    {
        
        // root
        var par = new Vertex<int>(0);
        src = new Graph<int>(par);

        for (int i = 1; i < N-1 ; i+=2)
        {
            var v2 = new Vertex<int>(i);
            var v3 = new Vertex<int>(i + 1);
            v2.Parent = par;
            v3.Parent = par;

            Assert.True(src.Add(v2), $"Failed to add element #{i} - {v2}");
            Assert.True(src.Add(v3), $"Failed to add element #{i} - {v3}");
            par = v2;
        }

        var end = new Vertex<int>((int) N);
        end.Parent = par;

        Assert.True(src.Add(end), $"Failed to add last element {end}");
        
        // Assert that construction is correct
        Assert.Equal(src.Size, N);
    }

    [Fact]
    public void CanDeleteOne()
    {
        // Arrange
        IGraph<UInt64> act = new Graph<UInt64>(new Vertex<UInt64>(0xD3ADB33F));
        IVertex<UInt64> toDelete = new Vertex<UInt64>(0xD3ADB33F);

        // Act
        Assert.True(act.Delete(toDelete), $"Failed deletion of {toDelete}");
    }

    [Fact]
    public void CanDeleteMultiple()
    {
        for (int i = (int) N; i >= 0; i--)
        {
            IVertex<int> exp = new Vertex<int>(i);
            Assert.Equal(i, (int) src.Size);
            Assert.True(src.Delete(exp), $"Failed deletion of {exp}");
            Assert.Equal(i - 1, (int)src.Size);
        }
    }
}