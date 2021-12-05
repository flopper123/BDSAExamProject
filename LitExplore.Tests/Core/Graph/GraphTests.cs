using Xunit;
using LitExplore.Core.Graph;

namespace LitExplore.Tests.Core.Graph;

public class GraphTests : IDisposable
{
    PublicationGraph _graph;
    public GraphTests()
    {
        _graph = new PublicationGraph();
    }
    //AddVertex  
    [Fact]
    public void Add_Vertex_Without_Title_Returns_False()
    {
        var vertex = new PublicationVertex("0", new PublicationDto { });
        var add = _graph.AddVertex(vertex);

        var actual = _graph.NumberOfVertices();
        Assert.False(add, $"added the vertex wrongly with the title of: \'{vertex.Data.Title}\'");
        Assert.Equal(0, actual);
    }
    //AddVertex  
    [Fact]
    public void Add_Vertex_Increses_Number_Of_Vertices_By_1()
    {
        var vertex = new PublicationVertex("0", new PublicationDto { Title = "Test" });
        var add = _graph.AddVertex(vertex);

        var actual = _graph.NumberOfVertices();
        Assert.True(add, "Failed to add the vertex");
        Assert.Equal(1, actual);
    }
    [Fact]
    public void Add_Edge_With_Vertex_Vertex_Returns_False_With_Empty_Data()
    {
        var edge = (new PublicationVertex("0", new PublicationDto()), new PublicationVertex("1", new PublicationDto()));
        var add = _graph.AddEdge(edge.Item1, edge.Item2);

        var actual = _graph.NumberOfEdges();

        Assert.False(add, $"added the edge wrongly with");
        Assert.Equal(0, actual);
    }
    [Fact]
    public void Add_Edge_With_Vertex_Vertex_Increses_Number_Of_Edges_By_1()
    {
        var add = _graph.AddEdge(new PublicationVertex("Test 0", new PublicationDto { Title = "Test 0" }), new PublicationVertex("Test 1", new PublicationDto { Title = "Test 1" }));
        var expected = 1;

        var actual = _graph.NumberOfEdges();

        Assert.True(add, $"Did not add The edge correctly");
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Add_Edge_With_Id_Id_Not_In_Set_Returns_False()
    {
        _graph.AddVertex(new PublicationVertex("0", new PublicationDto()));
        _graph.AddVertex(new PublicationVertex("1", new PublicationDto()));
        var add = _graph.AddEdge("-1", "2");

        var actual = _graph.NumberOfEdges();

        Assert.False(add, $"Did not add The edge correctly");
        Assert.Equal(0, actual);
    }
    [Fact]
    public void Add_Edge_With_Id_Id_Increses_Number_Of_Edges_By_1()
    {
        _graph.AddVertex(new PublicationVertex("T 1", new PublicationDto { Title = "T 1" }));
        _graph.AddVertex(new PublicationVertex("T 2", new PublicationDto { Title = "T 2" }));
        var expected = 1;

        var add = _graph.AddEdge("T 1", "T 2");

        var actual = _graph.NumberOfEdges();
        Assert.True(add, $"Did not add The edge correctly");
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Add_Edge_With_Vertex_Id_Increses_Number_Of_Edges_By_1()
    {
        // Arrange
        _graph.AddVertex(new PublicationVertex("T 1", new PublicationDto { Title = "T 1" }));
        var expected = 1;
        // Act
        var add = _graph.AddEdge("T 1", new PublicationVertex("T 2", new PublicationDto { Title = "T 2" }));
        // Assert
        var actual = _graph.NumberOfEdges();
        Assert.True(add, $"Did not add The edge correctly");
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Add_Edge_With_Id_Vertex_Increses_Number_Of_Edges_By_1()
    {
        // Arrange
        _graph.AddVertex(new PublicationVertex("T 2", new PublicationDto { Title = "T 2" }));
        var expected = 1;
        // Act
        var add = _graph.AddEdge(new PublicationVertex("T 1", new PublicationDto { Title = "T 1" }), "T 2");
        // Assert
        var actual = _graph.NumberOfEdges();
        Assert.True(add, $"Did not add The edge correctly");
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Add_Edge_With_Edge_Increses_Number_Of_Edges_By_1()
    {
        // Arrange
        var edge = new PublicationEdge(
            new PublicationVertex("T 1", new PublicationDto { Title = "T 1" }),
            new PublicationVertex("T 2", new PublicationDto { Title = "T 2" })
        );
        var expected = 1;
        // Act
        var add = _graph.AddEdge(edge);
        // Assert
        var actual = _graph.NumberOfEdges();
        Assert.True(add, $"Did not add The edge correctly");
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Add_Edge_With_A_PublicationEdge()
    {
        // Arrange
        _graph.AddVertex(new PublicationVertex("T 2", new PublicationDto { Title = "T 2" }));
        var expected = 1;
        // Act
        var add = _graph.AddEdge(new PublicationVertex("T 1", new PublicationDto { Title = "T 1" }), "T 2");
        // Assert
        var actual = _graph.NumberOfEdges();
        Assert.True(add, $"Did not add The edge correctly");
        Assert.Equal(expected, actual);
    }

    //GetAdj(Vertex)
    [Fact]
    public void GetAdj_Given_Vertex_Returns_Connected_Vertices()
    {
        graphSeedSmall();

        var adj = _graph.GetAdj(new PublicationVertex("Publication 0", new PublicationDto { Title = "Publication 0" })).ToArray();

        Assert.Equal(2, adj.Length);
        Assert.True("Publication 1" == adj[0].Data.Title, $"The first adjesent vertex was not \"Publication 1\" but {adj[0].Data.Title}");
        Assert.True("Publication 2" == adj[1].Data.Title, $"The first adjesent vertex was not \"Publication 2\" but {adj[0].Data.Title}");
    }
    //GetAdj(Id)
    [Fact]
    public void GetAdj_Given_Id_Returns_Connected_Vertices()
    {
        graphSeedSmall();

        var adj = _graph.GetAdj("Publication 0").ToArray();

        Assert.Equal(2, adj.Length);
        Assert.True("Publication 1" == adj[0].Data.Title, $"The first adjesent vertex was not \"Publication 1\" but {adj[0].Data.Title}");
        Assert.True("Publication 2" == adj[1].Data.Title, $"The first adjesent vertex was not \"Publication 2\" but {adj[0].Data.Title}");
    }
    [Fact]
    public void Known_Vertex_0_Is_Connected_To_Vertex_11()
    {
        graphSeedBig();

        var actual = connected("Publication 0", "Publication 11");

        Assert.True(actual);
    }
    [Fact]
    public void Vertex_Without_Connections_Is_Not_Connected_To_The_Graph()
    {
        graphSeedBig();
        _graph.AddVertex(new PublicationVertex("Publication 12", new PublicationDto { Title = "Publication 12" }));

        var acual = connected("Publication 0", "Publication 12");

        Assert.False(acual, "Unconnected vertex seems to be connected");
    }
    //this is a simple algorithem and shuld not be used if degrees of seperation is wanted
    private bool connected(string fromId, string toId)
    {
        var marked = new Dictionary<string, bool>(_graph.NumberOfVertices());
        var adjs = new Stack<PublicationVertex>();
        adjs.Push(new PublicationVertex("Publication 0", new PublicationDto()));
        for (var i = 0; i < _graph.NumberOfVertices(); i++)
        {
            marked.Add($"Publication {i}", false);
        }
        while (adjs.Count > 0)
        {
            var v = adjs.Pop();
            if (marked[v.Id]) continue;
            marked[v.Id] = true;
            foreach (var vertex in _graph.GetAdj(v.Id))
            {
                adjs.Push((PublicationVertex)vertex);
            }
        }
        return marked[toId];
    }
    private void graphSeedSmall()
    {
        _graph.AddVertex(new PublicationVertex("Publication 0", new PublicationDto { Title = "Publication 0" }));
        _graph.AddVertex(new PublicationVertex("Publication 1", new PublicationDto { Title = "Publication 1" }));
        _graph.AddVertex(new PublicationVertex("Publication 2", new PublicationDto { Title = "Publication 2" }));

        _graph.AddEdge("Publication 0", "Publication 1");
        _graph.AddEdge("Publication 0", "Publication 2");
    }
    private void graphSeedBig()
    {
        _graph.AddVertex(new PublicationVertex("Publication 0", new PublicationDto { Title = "Publication 0" }));
        _graph.AddVertex(new PublicationVertex("Publication 1", new PublicationDto { Title = "Publication 1" }));
        _graph.AddVertex(new PublicationVertex("Publication 2", new PublicationDto { Title = "Publication 2" }));
        _graph.AddVertex(new PublicationVertex("Publication 3", new PublicationDto { Title = "Publication 3" }));
        _graph.AddVertex(new PublicationVertex("Publication 4", new PublicationDto { Title = "Publication 4" }));
        _graph.AddVertex(new PublicationVertex("Publication 5", new PublicationDto { Title = "Publication 5" }));
        _graph.AddVertex(new PublicationVertex("Publication 6", new PublicationDto { Title = "Publication 6" }));
        _graph.AddVertex(new PublicationVertex("Publication 7", new PublicationDto { Title = "Publication 7" }));
        _graph.AddVertex(new PublicationVertex("Publication 8", new PublicationDto { Title = "Publication 8" }));
        _graph.AddVertex(new PublicationVertex("Publication 9", new PublicationDto { Title = "Publication 9" }));
        _graph.AddVertex(new PublicationVertex("Publication 10", new PublicationDto { Title = "Publication 10" }));
        _graph.AddVertex(new PublicationVertex("Publication 11", new PublicationDto { Title = "Publication 11" }));
        _graph.AddEdge("Publication 0", "Publication 1");
        _graph.AddEdge("Publication 0", "Publication 2");
        _graph.AddEdge("Publication 1", "Publication 3");
        _graph.AddEdge("Publication 3", "Publication 5");
        _graph.AddEdge("Publication 2", "Publication 3");
        _graph.AddEdge("Publication 3", "Publication 4");
        _graph.AddEdge("Publication 4", "Publication 5");
        _graph.AddEdge("Publication 4", "Publication 6");
        _graph.AddEdge("Publication 5", "Publication 6");
        _graph.AddEdge("Publication 6", "Publication 7");
        _graph.AddEdge("Publication 7", "Publication 8");
        _graph.AddEdge("Publication 8", "Publication 0");
        _graph.AddEdge("Publication 8", "Publication 9");
        _graph.AddEdge("Publication 8", "Publication 10");
        _graph.AddEdge("Publication 10", "Publication 11");
    }
    public void Dispose()
    {

    }
}