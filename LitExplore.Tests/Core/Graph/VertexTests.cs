using LitExplore.Core.Graph;
using Xunit;

namespace LitExplore.Tests.Core.Graph;

public class VertexTests
{
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
        IVertex<int> exp = act.GetParent();

        // Assert
        Assert.True(act.IsRoot(), "Actual is not root node");
        Assert.Equal(exp, act);
    }
    [Fact]
    public void AddChild()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        IVertex<int> vertex1 = new Vertex<int>(1);
        var expected = 1;
        // Act
        vertex0.AddChild(vertex1);
        // Assert
        var actual = vertex0.GetChildren().Count;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddChildIncrementsDepthByOne()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        IVertex<int> vertex1 = new Vertex<int>(1);
        var expected = vertex0.Depth;

        // Act
        vertex0.AddChild(vertex1);

        // Assert
        var actual = vertex0.Depth;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ChildCanRetrunParrent()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        IVertex<int> vertex1 = new Vertex<int>(1);
        var expected = vertex0;
        // Act
        vertex0.AddChild(vertex1);
        // Assert
        var actual = vertex0.GetParent();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DataFromContainingType()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        var expected = 0;

        // Assert
        var actual = vertex0.Data;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddingAChildUpdateParrentSize()
    {
        // Arrange
        IVertex<int> vertex0 = new Vertex<int>(0);
        IVertex<int> vertex1 = new Vertex<int>(1);
        var expected = vertex0.Size+1;
        // Act
        vertex0.AddChild(vertex1);
        // Assert
        var actual = vertex0.Size;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeleteChild()
    {
        // Arrange
        Vertex<int> v_1 = new Vertex<int>(0);
        IVertex<int> v_2 = new Vertex<int>(1);
        //Act
        v_1.AddChild(v_2);
        v_1.AddChild(new Vertex<int>(2));
        
        Assert.True(v_1.Size == 3); // succesfully add

        // Failed delete check
        Assert.True(!v_1.Delete(v_1), "Delete was succesful, when we expected false");
        Assert.True(v_1.Size == 3);

        // Succesful delete check
        Assert.True(v_1.Delete(v_2), "Did not Delete With Success");
        Assert.True(v_1.Size == 2); 

        foreach(IVertex<int> child in v_1.GetChildren()) {
            Assert.True(!child.Equals(v_2), $"Failed deletion of child {v_2} from {v_1}");           
        }            
    }
}