using Interfaces;
using LitExplore.Entity;
using System.Collections.Generic;

namespace LitExplore.Core.Graph;

// TODO: Fix IVertex etc

public class PublicationGraph : IGraph<PublicationDto>
{
    private List<IVertex<PublicationDto>> Vertices;
    private IDictionary<int, List<IEdge<PublicationDto>>> Edges;
    public PublicationGraph()
    {
        Vertices = new List<IVertex<PublicationDto>>();
        Edges = new Dictionary<int, List<IEdge<PublicationDto>>>();
    }
    public bool AddVertex(IVertex<PublicationDto> vertex)
    {
        if (vertex.Data.Title == "" ||
            vertex.Data.Title == null ||
            vertex.Id < Vertices.Count) return false;
        Vertices.Add(vertex);
        Edges.Add(vertex.Id, new List<IEdge<PublicationDto>>());
        return true;
    }
    //TO:DO burde nok ikke bare retrune bool
    public bool AddEdge(IVertex<PublicationDto> from, IVertex<PublicationDto> to)
    {
        if (from.Equals(to)) return false;
        var _from = Vertices.FirstOrDefault(v => v.Data.Title == from.Data.Title && v.Id == from.Id);
        if (from == null) return false;
        else if (!AddVertex(from)) return false;
        else _from = from;
        var _to = Vertices.FirstOrDefault(v => v.Data.Title == to.Data.Title && v.Id == to.Id);
        if (to == null) return false;
        else if (!AddVertex(to)) return false;
        else _to = to;

        if (Edges.Keys.Contains(_to.Id))
        {
            Edges[_from.Id].Add(new Edge(_from, _to));
        }
        else throw new NotImplementedException();//TO:DO
        if (Edges.Keys.Contains(_to.Id))
        {
            Edges[_to.Id].Add(new Edge(_from, _to));
        }
        else throw new NotImplementedException();//TO:DO
        return true;
    }
    public bool AddEdge(int fromId, int toId)
    {
        if (fromId < 0 || fromId >= NumberOfVertices()) return false;
        if (toId < 0 || toId >= NumberOfVertices()) return false;
        if (Edges.Keys.Contains(fromId))
        {
            Edges[fromId].Add(new Edge(Vertices[fromId], Vertices[toId]));
        }
        else throw new NotImplementedException();//TO:DO
        if (Edges.Keys.Contains(toId))
        {
            Edges[toId].Add(new Edge(Vertices[toId], Vertices[fromId]));
        }
        else throw new NotImplementedException();//TO:DO
        return true;
    }
    public IEnumerable<IVertex<PublicationDto>> GetAdj(IVertex<PublicationDto> vertex)
    {
        var _vertex = Vertices.FirstOrDefault(v => v.Data.Title == vertex.Data.Title && v.Id == vertex.Id);
        if (_vertex == null) throw new NotImplementedException();//TO:DO
        foreach (var edge in Edges[vertex.Id])
        {
            yield return edge.GetTo();
        }
    }
    public IEnumerable<IVertex<PublicationDto>> GetAdj(int vertexId)
    {
        if (vertexId < 0 || vertexId >= NumberOfVertices()) throw new NotImplementedException(); //TO:DO
        foreach (var edge in Edges[vertexId])
        {
            yield return edge.GetTo();
        }
    }
    public int NumberOfVertices() =>
        Vertices.Count;
    public int NumberOfEdges()
    {
        var numEdges = 0;
        foreach (var edges in Edges.Values)
        {
            foreach (var edge in edges)
            {
                numEdges++;
            }
        }
        return numEdges/2;//This is divided in 2 because both of the vetecies of an edge knows the edge
    }

    //kan bruges til at se den matematiske notering af en graph med 12 V og 
    public static void Main(string[] args)
    {
        var g = new PublicationGraph();
        g.AddVertex(new Vertex(0, new PublicationDto { Title = "PublicationDto 0" }));
        g.AddVertex(new Vertex(1, new PublicationDto { Title = "PublicationDto 1" }));
        g.AddVertex(new Vertex(2, new PublicationDto { Title = "PublicationDto 2" }));
        g.AddVertex(new Vertex(3, new PublicationDto { Title = "PublicationDto 3" }));
        g.AddVertex(new Vertex(4, new PublicationDto { Title = "PublicationDto 4" }));
        g.AddVertex(new Vertex(5, new PublicationDto { Title = "PublicationDto 5" }));
        g.AddVertex(new Vertex(6, new PublicationDto { Title = "PublicationDto 6" }));
        g.AddVertex(new Vertex(7, new PublicationDto { Title = "PublicationDto 7" }));
        g.AddVertex(new Vertex(8, new PublicationDto { Title = "PublicationDto 8" }));
        g.AddVertex(new Vertex(9, new PublicationDto { Title = "PublicationDto 9" }));
        g.AddVertex(new Vertex(10, new PublicationDto { Title = "PublicationDto 10" }));
        g.AddVertex(new Vertex(11, new PublicationDto { Title = "PublicationDto 11" }));
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(3, 5);
        g.AddEdge(2, 3);
        g.AddEdge(3, 4);
        g.AddEdge(4, 5);
        g.AddEdge(4, 6);
        g.AddEdge(5, 6);
        g.AddEdge(6, 7);
        g.AddEdge(7, 8);
        g.AddEdge(8, 0);
        g.AddEdge(8, 9);
        g.AddEdge(8, 10);
        g.AddEdge(10, 11);
        Console.WriteLine(g);
    }

    public override string ToString()
    {
        string s = "";
        s += NumberOfVertices() + " vertices, " + NumberOfEdges() + " edges \n";
        for (int v = 0; v < NumberOfVertices(); v++)
        {
            s += v + ": ";
            foreach (var w in GetAdj(v))
            {
                s += w.Id + " ";
            }
            s += "\n";
        }
        return s;
    }
}
