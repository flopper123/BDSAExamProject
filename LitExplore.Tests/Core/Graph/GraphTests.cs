using Xunit;
using LitExplore.Core.Graph;

namespace LitExplore.Tests.Core.Graph;

public class GraphTests : IDisposable
{

    
    // public GraphTests()
    // {
    //     _graph = new PublicationGraph();
    // }
    // //AddVertex  
    // [Fact]
    // public void Inserts_1_Publication_Vertex_Without_Title_Returns_False()
    // {
    //     var vertex = new Vertex("0", new PublicationDto { });
    //     var add = _graph.AddVertex(vertex);

    //     var actual = _graph.NumberOfVertices();
    //     Assert.False(add, $"added the vertex wrongly with the title of: \'{vertex.Data.Title}\'");
    //     Assert.Equal(0, actual);
    // }
    // //AddVertex  
    // [Fact]
    // public void Inserts_1_Publication_Vertex_Increses_Number_Of_Vertices_By_1()
    // {
    //     var vertex = new Vertex("0", new PublicationDto { Title = "Test" });
    //     var add = _graph.AddVertex(vertex);

    //     var actual = _graph.NumberOfVertices();
    //     Assert.True(add, "Failed to add the vertex");
    //     Assert.Equal(1, actual);
    // }
    // //AddEdge(Vertex,Vertex)
    // [Fact]

    // public void Inserts_1_Referance_Edge_With_addEdge_Vertex_Vertex_Returns_False_With_Empty_Data()
    // {
    //     var edge = (new Vertex("0", new PublicationDto()), new Vertex("1", new PublicationDto()));
    //     var add = _graph.AddEdge(edge.Item1, edge.Item2);

    //     var actual = _graph.NumberOfEdges();

    //     Assert.False(add, $"added the edge wrongly with");
    //     Assert.Equal(0, actual);
    // }
    // //AddEdge(Vertex,Vertex)
    // [Fact]
    // public void Inserts_1_Referance_Edge_With_addEdge_Vertex_Vertex_Increses_Number_Of_Edges_By_1()
    // {
    //     var add = _graph.AddEdge(new Vertex("Test 0", new PublicationDto { Title = "Test 0" }), new Vertex("Test 1", new PublicationDto { Title = "Test 1" }));
    //     var expected = 1;


    //     var actual = _graph.NumberOfEdges();

    //     Assert.True(add, $"Did not add The edge correctly");
    //     Assert.Equal(expected, actual);
    // }
    // [Fact]
    // public void Inserts_1_Referance_Edge_With_addEdge_Id_Id_Out_Of_Range_Returns_False()
    // {
    //     _graph.AddVertex(new Vertex("0", new PublicationDto()));
    //     _graph.AddVertex(new Vertex("1", new PublicationDto()));
    //     var add = _graph.AddEdge("-1", "2");

    //     var actual = _graph.NumberOfEdges();

    //     Assert.False(add, $"Did not add The edge correctly");
    //     Assert.Equal(0, actual);
    // }
    // [Fact]
    // public void Inserts_1_Referance_Edge_With_addEdge_Id_Id_Increses_Number_Of_Edges_By_1()
    // {
    //     _graph.AddVertex(new Vertex("T 1", new PublicationDto { Title = "T 1" }));
    //     _graph.AddVertex(new Vertex("T 2", new PublicationDto { Title = "T 2" }));
    //     var expected = 1;

    //     var add = _graph.AddEdge("T 1", "T 2");

    //     var actual = _graph.NumberOfEdges();
    //     Assert.True(add, $"Did not add The edge correctly");
    //     Assert.Equal(expected, actual);
    // }
    // //GetAdj(Vertex)
    // [Fact]
    // public void GetAdj_Given_Vertex_Returns_Connected_Vertices()
    // {
    //     graphSeedSmall();

    //     var adj = _graph.GetAdj(new Vertex("Publication 0", new PublicationDto { Title = "Publication 0" })).ToArray();

    //     Assert.Equal(2, adj.Length);
    //     Assert.True("Publication 1" == adj[0].Data.Title, $"The first adjesent vertex was not \"Publication 1\" but {adj[0].Data.Title}");
    //     Assert.True("Publication 2" == adj[1].Data.Title, $"The first adjesent vertex was not \"Publication 2\" but {adj[0].Data.Title}");
    // }
    // //GetAdj(Id)
    // [Fact]
    // public void GetAdj_Given_Id_Returns_Connected_Vertices()
    // {
    //     graphSeedSmall();

    //     var adj = _graph.GetAdj("Publication 0").ToArray();

    //     Assert.Equal(2, adj.Length);
    //     Assert.True("Publication 1" == adj[0].Data.Title, $"The first adjesent vertex was not \"Publication 1\" but {adj[0].Data.Title}");
    //     Assert.True("Publication 2" == adj[1].Data.Title, $"The first adjesent vertex was not \"Publication 2\" but {adj[0].Data.Title}");
    // }

    // [Fact]
    // public void Known_Vertex_0_Is_Connected_To_Vertex_11()
    // {
    //     graphSeedBig();

    //     var actual = connected("Publication 0", "Publication 11");

    //     Assert.True(actual);
    // }
    // [Fact]
    // public void Vertex_Without_Connections_Is_Not_Connected_To_The_Graph()
    // {
    //     graphSeedBig();
    //     _graph.AddVertex(new Vertex("Publication 12", new PublicationDto { Title = "Publication 12" }));

    //     var acual = connected("Publication 0", "Publication 12");

    //     Assert.False(acual, "Unconnected vertex seems to be connected");
    // }
    // //this is a simple algorithem and shuld not be used if degrees of seperation is wanted
    // private bool connected(string fromId, string toId)
    // {
    //     var marked = new Dictionary<string, bool>(_graph.NumberOfVertices());
    //     var adjs = new Stack<Vertex>();
    //     adjs.Push(new Vertex("Publication 0", new PublicationDto()));
    //     for (var i = 0; i < _graph.NumberOfVertices(); i++)
    //     {
    //         marked.Add($"Publication {i}", false);
    //     }
    //     while (adjs.Count > 0)
    //     {
    //         var v = adjs.Pop();
    //         if (marked[v.Id]) continue;
    //         marked[v.Id] = true;
    //         foreach (var vertex in _graph.GetAdj(v.Id))
    //         {
    //             adjs.Push((Vertex)vertex);
    //         }
    //     }
    //     return marked[toId];
    // }

    // private void graphSeedSmall()
    // {
    //     _graph.AddVertex(new Vertex("Publication 0", new PublicationDto { Title = "Publication 0" }));
    //     _graph.AddVertex(new Vertex("Publication 1", new PublicationDto { Title = "Publication 1" }));
    //     _graph.AddVertex(new Vertex("Publication 2", new PublicationDto { Title = "Publication 2" }));

    //     _graph.AddEdge("Publication 0", "Publication 1");
    //     _graph.AddEdge("Publication 0", "Publication 2");
    // }
    // private void graphSeedBig()
    // {
    //     _graph.AddVertex(new Vertex("Publication 0", new PublicationDto { Title = "Publication 0" }));
    //     _graph.AddVertex(new Vertex("Publication 1", new PublicationDto { Title = "Publication 1" }));
    //     _graph.AddVertex(new Vertex("Publication 2", new PublicationDto { Title = "Publication 2" }));
    //     _graph.AddVertex(new Vertex("Publication 3", new PublicationDto { Title = "Publication 3" }));
    //     _graph.AddVertex(new Vertex("Publication 4", new PublicationDto { Title = "Publication 4" }));
    //     _graph.AddVertex(new Vertex("Publication 5", new PublicationDto { Title = "Publication 5" }));
    //     _graph.AddVertex(new Vertex("Publication 6", new PublicationDto { Title = "Publication 6" }));
    //     _graph.AddVertex(new Vertex("Publication 7", new PublicationDto { Title = "Publication 7" }));
    //     _graph.AddVertex(new Vertex("Publication 8", new PublicationDto { Title = "Publication 8" }));
    //     _graph.AddVertex(new Vertex("Publication 9", new PublicationDto { Title = "Publication 9" }));
    //     _graph.AddVertex(new Vertex("Publication 10", new PublicationDto { Title = "Publication 10" }));
    //     _graph.AddVertex(new Vertex("Publication 11", new PublicationDto { Title = "Publication 11" }));
    //     _graph.AddEdge("Publication 0", "Publication 1");
    //     _graph.AddEdge("Publication 0", "Publication 2");
    //     _graph.AddEdge("Publication 1", "Publication 3");
    //     _graph.AddEdge("Publication 3", "Publication 5");
    //     _graph.AddEdge("Publication 2", "Publication 3");
    //     _graph.AddEdge("Publication 3", "Publication 4");
    //     _graph.AddEdge("Publication 4", "Publication 5");
    //     _graph.AddEdge("Publication 4", "Publication 6");
    //     _graph.AddEdge("Publication 5", "Publication 6");
    //     _graph.AddEdge("Publication 6", "Publication 7");
    //     _graph.AddEdge("Publication 7", "Publication 8");
    //     _graph.AddEdge("Publication 8", "Publication 0");
    //     _graph.AddEdge("Publication 8", "Publication 9");
    //     _graph.AddEdge("Publication 8", "Publication 10");
    //     _graph.AddEdge("Publication 10", "Publication 11");
    // }

    public void Dispose()
    {

    }
}