using Xunit;
using LitExplore.Entity;

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
    public void Inserts_1_Publication_Vertex_Without_Title_Returns_False()
    {
        var vertex = new Vertex(0, new Publication { });
        var add = _graph.AddVertex(vertex);

        var actual = _graph.NumberOfVertices();
        Assert.False(add, $"added the vertex wrongly with the title of: \'{vertex.Data.Title}\'");
        Assert.Equal(0, actual);
    }
    //AddVertex  
    [Fact]
    public void Inserts_1_Publication_Vertex_Increses_Number_Of_Vertices_By_1()
    {
        var vertex = new Vertex(0, new Publication { Title = "Test" });
        var add = _graph.AddVertex(vertex);

        var actual = _graph.NumberOfVertices();
        Assert.True(add, "Failed to add the vertex");
        Assert.Equal(1, actual);
    }
    //AddEdge(Vertex,Vertex)
    [Fact]

    public void Inserts_1_Referance_Edge_With_addEdge_Vertex_Vertex_Returns_False_With_Empty_Data()
    {
        var edge = (new Vertex(0, new Publication()), new Vertex(1, new Publication()));
        var add = _graph.AddEdge(edge.Item1, edge.Item2);

        var actual = _graph.NumberOfEdges();

        Assert.False(add, $"added the edge wrongly with");
        Assert.Equal(0, actual);
    }
    //AddEdge(Vertex,Vertex)
    [Fact]
    public void Inserts_1_Referance_Edge_With_addEdge_Vertex_Vertex_Increses_Number_Of_Edges_By_1()
    {
        var add = _graph.AddEdge(new Vertex(0, new Publication { Title = "Test 0" }), new Vertex(1, new Publication { Title = "Test 1" }));
        var expected = 1;


        var actual = _graph.NumberOfEdges();

        Assert.True(add, $"Did not add The edge correctly");
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Inserts_1_Referance_Edge_With_addEdge_Id_Id_Out_Of_Range_Returns_False()
    {
        _graph.AddVertex(new Vertex(0, new Publication()));
        _graph.AddVertex(new Vertex(1, new Publication()));
        var add = _graph.AddEdge(-1, 2);

        var actual = _graph.NumberOfEdges();

        Assert.False(add, $"Did not add The edge correctly");
        Assert.Equal(0, actual);
    }
    [Fact]
    public void Inserts_1_Referance_Edge_With_addEdge_Id_Id_Increses_Number_Of_Edges_By_1()
    {
        _graph.AddVertex(new Vertex(0, new Publication { Title = "T 1" }));
        _graph.AddVertex(new Vertex(1, new Publication { Title = "T 2" }));
        var expected = 1;

        var add = _graph.AddEdge(0, 1);

        var actual = _graph.NumberOfEdges();
        Assert.True(add, $"Did not add The edge correctly");
        Assert.Equal(expected, actual);
    }
    //GetAdj(Vertex)
    [Fact]
    public void GetAdj_Given_Vertex_Returns_Connected_Vertices()
    {
        graphSeedSmall();

        var adj = _graph.GetAdj(new Vertex(0, new Publication { Title = "Publication 0" })).ToArray();

        Assert.Equal(2, adj.Length);
        Assert.True("Publication 1" == adj[0].Data.Title, $"The first adjesent vertex was not \"Publication 1\" but {adj[0].Data.Title}");
        Assert.True("Publication 2" == adj[1].Data.Title, $"The first adjesent vertex was not \"Publication 2\" but {adj[0].Data.Title}");
    }
    //GetAdj(Id)
    [Fact]
    public void GetAdj_Given_Id_Returns_Connected_Vertices()
    {
        graphSeedSmall();

        var adj = _graph.GetAdj(0).ToArray();

        Assert.Equal(2, adj.Length);
        Assert.True("Publication 1" == adj[0].Data.Title, $"The first adjesent vertex was not \"Publication 1\" but {adj[0].Data.Title}");
        Assert.True("Publication 2" == adj[1].Data.Title, $"The first adjesent vertex was not \"Publication 2\" but {adj[0].Data.Title}");
    }

    [Fact]
    public void Known_Vertex_0_Is_Connected_To_Vertex_11()
    {
        graphSeedBig();

        var actual = connected(0, 11);

        Assert.True(actual);
    }
    [Fact]
    public void Vertex_Without_Connections_Is_Not_Connected_To_The_Graph()
    {
        graphSeedBig();
        _graph.AddVertex(new Vertex(12,new Publication{Title = "Publication 12"}));

        var acual = connected(0,12);

        Assert.False(acual,"Unconnected vertex seems to be connected");
    }
    //this is a simple algorithem and shuld not be used if degrees of seperation is wanted
    private bool connected(int fromId, int toId) 
    {
        var marked = new bool[_graph.NumberOfVertices()];
        var adjs = new Stack<Vertex>();
        adjs.Push(new Vertex(0, new Publication()));
        while (adjs.Count > 0)
        {
            var v = adjs.Pop();
            if (marked[v.Id]) continue;
            marked[v.Id] = true;
            foreach (var vertex in _graph.GetAdj(v.Id))
            {
                adjs.Push((Vertex)vertex);
            }
        }
        return marked[toId];
    }

    private void graphSeedSmall()
    {
        _graph.AddVertex(new Vertex(0, new Publication { Title = "Publication 0" }));
        _graph.AddVertex(new Vertex(1, new Publication { Title = "Publication 1" }));
        _graph.AddVertex(new Vertex(2, new Publication { Title = "Publication 2" }));

        _graph.AddEdge(0, 1);
        _graph.AddEdge(0, 2);
    }
    private void graphSeedBig()
    {
        _graph.AddVertex(new Vertex(0, new Publication { Title = "Publication 0" }));
        _graph.AddVertex(new Vertex(1, new Publication { Title = "Publication 1" }));
        _graph.AddVertex(new Vertex(2, new Publication { Title = "Publication 2" }));
        _graph.AddVertex(new Vertex(3, new Publication { Title = "Publication 3" }));
        _graph.AddVertex(new Vertex(4, new Publication { Title = "Publication 4" }));
        _graph.AddVertex(new Vertex(5, new Publication { Title = "Publication 5" }));
        _graph.AddVertex(new Vertex(6, new Publication { Title = "Publication 6" }));
        _graph.AddVertex(new Vertex(7, new Publication { Title = "Publication 7" }));
        _graph.AddVertex(new Vertex(8, new Publication { Title = "Publication 8" }));
        _graph.AddVertex(new Vertex(9, new Publication { Title = "Publication 9" }));
        _graph.AddVertex(new Vertex(10, new Publication { Title = "Publication 10" }));
        _graph.AddVertex(new Vertex(11, new Publication { Title = "Publication 11" }));
        _graph.AddEdge(0, 1);
        _graph.AddEdge(0, 2);
        _graph.AddEdge(1, 3);
        _graph.AddEdge(3, 5);
        _graph.AddEdge(2, 3);
        _graph.AddEdge(3, 4);
        _graph.AddEdge(4, 5);
        _graph.AddEdge(4, 6);
        _graph.AddEdge(5, 6);
        _graph.AddEdge(6, 7);
        _graph.AddEdge(7, 8);
        _graph.AddEdge(8, 0);
        _graph.AddEdge(8, 9);
        _graph.AddEdge(8, 10);
        _graph.AddEdge(10, 11);
    }

    public void Dispose()
    {

    }
}