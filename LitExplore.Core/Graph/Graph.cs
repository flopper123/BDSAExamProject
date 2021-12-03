using System.Collections.Generic;

namespace LitExplore.Core.Graph;


public class PublicationGraph : IGraph<PublicationDto, string>
{
    private IDictionary<string, IVertex<PublicationDto, string>> Vertices;
    private IDictionary<string, List<IEdge<PublicationDto, string>>> Edges;
    public PublicationGraph()
    {
        Vertices = new Dictionary<string, IVertex<PublicationDto, string>>();
        Edges = new Dictionary<string, List<IEdge<PublicationDto, string>>>();
    }
    public bool AddVertex(IVertex<PublicationDto, string> vertex)
    {

        if (Vertices.ContainsKey(vertex.Id) ||
            vertex.Data.Title == null ||
            vertex.Id == "" ||
            vertex.Id == null)
        {
            return false;
        }
        Vertices.Add(vertex.Id, vertex);
        Edges.Add(vertex.Id, new List<IEdge<PublicationDto, string>>());
        return true;
    }

    //TO:DO burde nok ikke bare retrune bool
    public bool AddEdge(IVertex<PublicationDto, string> from, IVertex<PublicationDto, string> to)
    {
        // Does vertices exist in table
        if (from.Id.Equals(to.Id))
        {
            return false;
        }
        if (from == null || !Vertices.ContainsKey(from.Id))
        {
            if (!AddVertex(from)) return false;
        }
        if (to == null || !Vertices.ContainsKey(to.Id))
        {
            if (!AddVertex(to)) return false;
        }
        var _from = Vertices[from.Id];
        var _to = Vertices[to.Id];

        if (Edges.Keys.Contains(_to.Id))
        {
            Edges[_from.Id].Add(new Edge(_from, _to));
        }
        else
        {
            throw new NotImplementedException();//TO:DO
        }

        if (Edges.Keys.Contains(_to.Id))
        {
            Edges[_to.Id].Add(new Edge(_from, _to));
        }
        else
        {
            throw new NotImplementedException();
        }
        return true;
    }
    /// <summary>
    /// This method uses the id of the vertecies (strings)
    /// This is probobly also slow af
    /// </summary>
    /// <param name="fromId"></param>
    /// <param name="toId"></param>
    /// <returns></returns>
    public bool AddEdge(string fromId, string toId)
    {
        if (!Vertices.ContainsKey(fromId)) return false;
        if (!Vertices.ContainsKey(toId)) return false;
        var from = Vertices[fromId];
        var to = Vertices[toId];
        if (Edges.Keys.Contains(fromId))
        {
            Edges[fromId].Add(new Edge(from, to));
        }
        else throw new NotImplementedException();//TO:DO
        if (Edges.Keys.Contains(toId))
        {
            Edges[toId].Add(new Edge(to, from));
        }
        else throw new NotImplementedException();//TO:DO
        return true;
    }
    public IEnumerable<IVertex<PublicationDto, string>> GetAdj(IVertex<PublicationDto, string> vertex)
    {
        var _vertex = Vertices[vertex.Id];
        //if (_vertex == null) throw new NotImplementedException();//TO:DO
        foreach (var edge in Edges[vertex.Id])
        {
            yield return edge.GetTo();
        }
    }
    public IEnumerable<IVertex<PublicationDto, string>> GetAdj(string vertexId)
    {
        var _vertex = Vertices[vertexId];
        if (_vertex == null) throw new NotImplementedException(); //TO:DO
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
        return numEdges / 2;//This is divided in 2 because both of the vetecies of an edge knows the edge
    }

    //kan bruges til at se den matematiske notering af en graph med 12 V og 
    public static void Main(string[] args)
    {
        var g = new PublicationGraph();
        g.AddVertex(new Vertex("PublicationDto 0", new PublicationDto { Title = "PublicationDto 0" }));
        g.AddVertex(new Vertex("PublicationDto 1", new PublicationDto { Title = "PublicationDto 1" }));
        g.AddVertex(new Vertex("PublicationDto 2", new PublicationDto { Title = "PublicationDto 2" }));
        g.AddVertex(new Vertex("PublicationDto 3", new PublicationDto { Title = "PublicationDto 3" }));
        g.AddVertex(new Vertex("PublicationDto 4", new PublicationDto { Title = "PublicationDto 4" }));
        g.AddVertex(new Vertex("PublicationDto 5", new PublicationDto { Title = "PublicationDto 5" }));
        g.AddVertex(new Vertex("PublicationDto 6", new PublicationDto { Title = "PublicationDto 6" }));
        g.AddVertex(new Vertex("PublicationDto 7", new PublicationDto { Title = "PublicationDto 7" }));
        g.AddVertex(new Vertex("PublicationDto 8", new PublicationDto { Title = "PublicationDto 8" }));
        g.AddVertex(new Vertex("PublicationDto 9", new PublicationDto { Title = "PublicationDto 9" }));
        g.AddVertex(new Vertex("PublicationDto 10", new PublicationDto { Title = "PublicationDto 10" }));
        g.AddVertex(new Vertex("PublicationDto 11", new PublicationDto { Title = "PublicationDto 11" }));
        g.AddEdge("PublicationDto 0", "PublicationDto 1");
        g.AddEdge("PublicationDto 0", "PublicationDto 1");
        g.AddEdge("PublicationDto 1", "PublicationDto 3");
        g.AddEdge("PublicationDto 3", "PublicationDto 5");
        g.AddEdge("PublicationDto 2", "PublicationDto 3");
        g.AddEdge("PublicationDto 3", "PublicationDto 4");
        g.AddEdge("PublicationDto 4", "PublicationDto 5");
        g.AddEdge("PublicationDto 4", "PublicationDto 6");
        g.AddEdge("PublicationDto 5", "PublicationDto 6");
        g.AddEdge("PublicationDto 6", "PublicationDto 7");
        g.AddEdge("PublicationDto 7", "PublicationDto 8");
        g.AddEdge("PublicationDto 8", "PublicationDto 0");
        g.AddEdge("PublicationDto 8", "PublicationDto 9");
        g.AddEdge("PublicationDto 8", "PublicationDto 10");
        g.AddEdge("PublicationDto 10", "PublicationDto 11");
        Console.WriteLine(g);
    }

    public override string ToString()
    {
        string s = "";
        s += NumberOfVertices() + " vertices, " + NumberOfEdges() + " edges \n";
        foreach (var v in Vertices.Keys)
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
