using LitExplore.Core.Graph;
using Xunit;

namespace LitExplore.Tests.Core.Graph;

public class VertexTests
{
    IVertex<int> tree = null!;
    
    // Any even number
    UInt64 tree_N = 100UL;

    public VertexTests() {
        
        tree = new Vertex<int>(0);
        var cur = tree;
        for (int i = 1; i < (int) tree_N - 1; i+=2) {

            var v2 = new Vertex<int>(i);
            var v3 = new Vertex<int>(i + 1);
            cur.Children.Add(v2);
            cur.Children.Add(v3);
            cur = v2;
        }

        cur.Children.Add(new Vertex<int>((int) tree_N));

        // Assert that construction is correct
        Assert.Equal(tree.Size, tree_N);
    }

    [Fact]
    public void HasDataAfterConstruction()
    {
        // Arrange
        IVertex<int> vertex = new Vertex<int>(0);
        var expected = 0;

        // Act

        // Assert
        var actual = vertex.Data;
        Assert.Equal(expected, actual);
    }
    
    
    [Fact]
    public void CheckIsRoot()
    {
        // Arrange
        IVertex<int> act = new Vertex<int>(6); // should be root at init.

        // Act
        IVertex<int> exp = act.Parent;

        // Assert
        Assert.True(act.IsRoot(), "Actual is not root node");
        Assert.Equal(exp, act);
    }
    
    [Fact]
    public void CheckSize1TreeVertexIsLeaf() {
        
        IVertex<int> exp = new Vertex<int>(0);
        Assert.True(exp.IsLeaf(), "A root vertex is not a leaf");
    }

    [Fact]
    public void CheckIsLeaf() {

        IVertex<int> root = new Vertex<int>(0);
        IVertex<int> leaf = new Vertex<int>(1);

        root.Children.Add(leaf);
        Assert.True(leaf.IsLeaf(), "Failed assertion of leaf");
    }

    [Fact]
    public void AddChild()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        IVertex<int> vertex1 = new Vertex<int>(1);
        var exp = 1;

        // Act
        vertex0.Children.Add(vertex1);

        // Assert
        var act = vertex0.Children.Count;
        Assert.Equal(exp, act);
    }

    [Fact]
    public void AddChildIncrementsDepthByOne()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        IVertex<int> vertex1 = new Vertex<int>(1);
        UInt64 exp = vertex0.Depth;

        // Act
        vertex0.Children.Add(vertex1);

        // Assert
        UInt64 act = vertex0.Depth;
        Assert.Equal(exp, act);
    }

    [Fact]
    public void ChildCanReturnParent()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        IVertex<int> vertex1 = new Vertex<int>(1);
        IVertex<int> exp = vertex0;

        // Act
        vertex0.Children.Add(vertex1);

        // Assert
        IVertex<int> act = vertex0.Parent;
        Assert.Equal(exp, act);
    }

    [Fact]
    public void DataFromContainingType()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        int exp = 0;

        // Assert
        int act = vertex0.Data;
        Assert.Equal(exp, act);
    }

    [Fact]
    public void AddingAChildUpdateParrentSize()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        IVertex<int> vertex1 = new Vertex<int>(1);
        UInt64 exp = vertex0.Size + 1UL;

        // Act
        vertex0.Children.Add(vertex1);
        
        // Assert
        UInt64 act = vertex0.Size;
        Assert.Equal(exp, act);
    }



    [Fact]
    public void CanFindOneSelf() {

        Vertex<int> v = new Vertex<int>(10);
        Assert.True(v.Find(10) != null);
    }

    [Fact]
    public void CanFindInSubtree() {

        for (int i = 1; i < ((int) tree_N / 2); i++) {
            IVertex<int>? tar = tree.Find(i);
            Assert.True(tar != null, $"Failed to find Vertex with data #{i} in subtree");
            Assert.Equal(i, (tar == null) ? Int32.MinValue : tar.Data);
        }
    }
    
    [Fact]
    public void CanFindInLeaf() {

        int exp = (int) tree_N;
        IVertex<int>? tar = tree.Find(exp);
        Assert.True(tar != null, $"Failed to find leaf Vertex with data #{exp}");

        if (tar != null) {
            Assert.True(tar.IsLeaf());
            Assert.Equal(exp, (tar == null) ? Int32.MinValue  : tar.Data);
        } 
    }
}