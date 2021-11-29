using Xunit;
using LitExplore.Entity;
using Interfaces;

namespace Graph.Tests
{

    public class GraphTests : IDisposable
    {
        Graph _graph;
        public GraphTests()
        {
            _graph = new Graph();
        }
        //AddVertex  
        [Fact]
        public void Inserts_1_Publication_Vertex_Increses_Number_Of_Vertices_By_1()
        {
            _graph.AddVertex(new Vertex(0, new Publication()));

            var actual = _graph.NumberOfVertices();

            Assert.Equal(1, actual);
        }
        //AddEdge(Vertex,Vertex)
        [Fact]
        public void Inserts_1_Referance_Edge_With_addEdge_Vertex_Vertex_Increses_Number_Of_Edges_By_1()
        {
            _graph.AddEdge(new Vertex(0, new Publication()), new Vertex(1, new Publication()));

            var actual = _graph.NumberOfEdges();

            Assert.Equal(1, actual);
        }
        [Fact]
        public void Inserts_1_Referance_Edge_With_addEdge_Id_Id_Increses_Number_Of_Edges_By_1()
        {
            _graph.AddVertex(new Vertex(0, new Publication()));
            _graph.AddVertex(new Vertex(1, new Publication()));
            _graph.AddEdge(0, 1);

            var actual = _graph.NumberOfEdges();

            Assert.Equal(1, actual);
        }
        //GetAdj(Vertex)
        [Fact]
        public void GetAdj_Given_Vertex_Retruns_Connected_Vertices()
        {
            _graph.AddVertex(new Vertex(0, new Publication { Title = "Publication 0" }));
            _graph.AddVertex(new Vertex(1, new Publication { Title = "Publication 1" }));
            _graph.AddVertex(new Vertex(2, new Publication { Title = "Publication 2" }));

            _graph.AddEdge(0, 1);
            _graph.AddEdge(0, 2);


            var adj = _graph.GetAdj(new Vertex(0, new Publication { Title = "Publication 0" })).ToArray();

            Assert.Equal(2, adj.Length);
            Assert.True("Publication 1" == adj[0].Data.Title, $"The first adjesent vertex was not \"Publication 1\" but {adj[0].Data.Title}");
            Assert.True("Publication 2" == adj[1].Data.Title, $"The first adjesent vertex was not \"Publication 2\" but {adj[0].Data.Title}");
        }
        //GetAdj(Id)
        [Fact]
        public void GetAdj_Given_Id_Retruns_Connected_Vertices()
        {
            _graph.AddVertex(new Vertex(0, new Publication { Title = "Publication 0" }));
            _graph.AddVertex(new Vertex(1, new Publication { Title = "Publication 1" }));
            _graph.AddVertex(new Vertex(2, new Publication { Title = "Publication 2" }));

            _graph.AddEdge(0, 1);
            _graph.AddEdge(0, 2);


            var adj = _graph.GetAdj(0).ToArray();

            Assert.Equal(2, adj.Length);
            Assert.True("Publication 1" == adj[0].Data.Title, $"The first adjesent vertex was not \"Publication 1\" but {adj[0].Data.Title}");
            Assert.True("Publication 2" == adj[1].Data.Title, $"The first adjesent vertex was not \"Publication 2\" but {adj[0].Data.Title}");
        }


        public void Dispose()
        {
        }
    }


}