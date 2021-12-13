using LitExplore.Core.Graph;
using Xunit;

namespace LitExplore.Tests.Core.Graph;

public class VertexTests
{
    INode<int> tree = null!;
    
    // Any even number
    UInt64 tree_N = 100UL;

    public VertexTests() {
        
        tree = new Node<int>(0);
        var cur = tree;
        for (int i = 1; i < (int) tree_N - 1; i+=2) {

            var v2 = new Node<int>(i);
            var v3 = new Node<int>(i + 1);
            cur.Children.Add(v2);
            cur.Children.Add(v3);
            cur = v2;
        }

        cur.Children.Add(new Node<int>((int) tree_N));

        // Assert that construction is correct
        Assert.Equal(tree.Size, tree_N);
    }

    [Fact]
    public void HasDataAfterConstruction()
    {
        // Arrange
        INode<int> Node = new Node<int>(0);
        var expected = 0;

        // Act

        // Assert
        var actual = Node.Data;
        Assert.Equal(expected, actual);
    }
    
    
    [Fact]
    public void CheckIsRoot()
    {
        // Arrange
        INode<int> act = new Node<int>(6); // should be root at init.

        // Act
        INode<int> exp = act.Parent;

        // Assert
        Assert.True(act.IsRoot(), "Actual is not root node");
        Assert.Equal(exp, act);
    }
    
    [Fact]
    public void CheckSize1TreeVertexIsLeaf() {
        
        INode<int> exp = new Node<int>(0);
        Assert.True(exp.IsLeaf(), "A root Node is not a leaf");
    }

    [Fact]
    public void CheckIsLeaf() {

        INode<int> root = new Node<int>(0);
        INode<int> leaf = new Node<int>(1);

        root.Children.Add(leaf);
        Assert.True(leaf.IsLeaf(), "Failed assertion of leaf");
    }

    [Fact]
    public void AddChild()
    {
        // Arrange
        INode<int> vertex0 = new Node<int>(0);
        INode<int> vertex1 = new Node<int>(1);
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
        INode<int> vertex0 = new Node<int>(0);
        INode<int> vertex1 = new Node<int>(1);
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
        INode<int> vertex0 = new Node<int>(0);
        INode<int> vertex1 = new Node<int>(1);
        INode<int> exp = vertex0;

        // Act
        vertex0.Children.Add(vertex1);

        // Assert
        INode<int> act = vertex0.Parent;
        Assert.Equal(exp, act);
    }

    [Fact]
    public void DataFromContainingType()
    {
        // Arrange
        INode<int> vertex0 = new Node<int>(0);
        int exp = 0;

        // Assert
        int act = vertex0.Data;
        Assert.Equal(exp, act);
    }

    [Fact]
    public void AddingAChildUpdateParrentSize()
    {
        // Arrange
        INode<int> vertex0 = new Node<int>(0);
        INode<int> vertex1 = new Node<int>(1);
        UInt64 exp = vertex0.Size + 1UL;

        // Act
        vertex0.Children.Add(vertex1);
        
        // Assert
        UInt64 act = vertex0.Size;
        Assert.Equal(exp, act);
    }



    [Fact]
    public void CanFindOneSelf() {

        Node<int> v = new Node<int>(10);
        Assert.True(v.Find(10) != null);
    }

    [Fact]
    public void CanFindInSubtree() {

        for (int i = 1; i < ((int) tree_N / 2); i++) {
            INode<int>? tar = tree.Find(i);
            Assert.True(tar != null, $"Failed to find Node with data #{i} in subtree");
            Assert.Equal(i, (tar == null) ? Int32.MinValue : tar.Data);
        }
    }
    
    [Fact]
    public void CanFindInLeaf() {

        int exp = (int) tree_N;
        INode<int>? tar = tree.Find(exp);
        Assert.True(tar != null, $"Failed to find leaf Node with data #{exp}");

        if (tar != null) {
            Assert.True(tar.IsLeaf());
            Assert.Equal(exp, (tar == null) ? Int32.MinValue  : tar.Data);
        } 
    }
}