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

        for (int i = 1; i < N - 1; i += 2)
        {
            var v2 = new Vertex<int>(i);
            var v3 = new Vertex<int>(i + 1);
            v2.Parent = par;
            v3.Parent = par;

            Assert.True(src.Add(v2), $"Failed to add element #{i} - {v2}");
            Assert.True(src.Add(v3), $"Failed to add element #{i} - {v3}");
            par = v2;
        }
        var end = new Vertex<int>((int)N - 1, par);
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
        for (int i = (int)N - 1; i > 0; i--)
        {
            IVertex<int> exp = new Vertex<int>(i);
            Assert.True(i + 1 == (int)src.Size, $"loop {N - i - 1}, before delete");
            Assert.True(src.Delete(exp), $"Failed deletion of {exp}, at loop {N - i}");
            Assert.True(i == (int)src.Size, $"loop {N - i - 1}, after delete");
        }
    }

    [Fact]
    public void CanGetEnumerator()
    {
        // Arrange
        IVertex<UInt64> root = new Vertex<UInt64>(0UL);
        IGraph<UInt64> _src = new Graph<UInt64>(root);
        IVertex<UInt64> v1 = new Vertex<UInt64>(1UL, root);
        IVertex<UInt64> v2 = new Vertex<UInt64>(2UL, root);
        root.Children.Add(v1);
        root.Children.Add(v2);
        // Act
        var actEnumerator = _src.GetEnumerator();
        actEnumerator.MoveNext(); //this is nessesary because stupid >:(

        // Assert
        Assert.True(root.Data == actEnumerator.Current, $"\tActual data is {actEnumerator.Current}\n\tExpected was {root.Data}");
        Assert.True(actEnumerator.MoveNext(), "Could not move to next expected Item");
        Assert.True(v1.Data == actEnumerator.Current, $"\tActual data is {actEnumerator.Current}\n\tExpected was {v1.Data}");
        Assert.True(actEnumerator.MoveNext(), "Could not move to next expected Item");
        Assert.True(v2.Data == actEnumerator.Current, $"\tActual data is {actEnumerator.Current}\n\tExpected was {v2.Data}");
    }
    [Fact]
    public void CanGetEnumerator100Elements()
    {
        // Arrange
        ///---- src is src see constructor
        var tmp = new List<int>();
        // Act
        var actEnumerator = src.GetEnumerator();
        while (actEnumerator.MoveNext())
        {
            tmp.Add(actEnumerator.Current);
        }

        // Assert
        for (int i = 0; i < N; i++)
        {
            Assert.True(tmp.Contains(i),$"The enumerator did not contain {i} but shuld have");
        }

    }
}