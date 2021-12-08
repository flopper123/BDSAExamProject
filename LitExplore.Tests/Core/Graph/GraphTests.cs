using Xunit;
using LitExplore.Core.Graph;

namespace LitExplore.Tests.Core.Graph;

public class GraphTests
{
    // Contains test data for vertices from 0 to N
    IGraph<int> src = null!;
    const UInt32 N = 100;

    public GraphTests() {
        src = new Graph<int>(new Vertex<int>(0));
        for (int i = 1; i < N; i++) {
            src.Add(new Vertex<int>(i));
        }
    }

    [Fact]
    public void CanDeleteOne() {
        // Arrange
        IGraph<int> act = new Graph<int>(new Vertex<int> (0));
        
        // Act
        // act.Delete()

        // Assert
    }
    
    /*
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
    */
}