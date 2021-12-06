using System.Collections.Generic;

namespace LitExplore.Core.Graph;


public class PublicationGraph : IGraph<string, PublicationDto>
{
    private IDictionary<string, IVertex<string, PublicationDto>> Vertices;
    private IDictionary<string, List<IEdge<string, PublicationDto>>> Edges;
    public PublicationGraph()
    {
        Vertices = new Dictionary<string, IVertex<string, PublicationDto>>();
        Edges = new Dictionary<string, List<IEdge<string, PublicationDto>>>();
    }
    public bool AddVertex(IVertex<string, PublicationDto> vertex)
    {

        if (Vertices.ContainsKey(vertex.Id) ||
            vertex.Data.Title == null ||
            vertex.Id == "" ||
            vertex.Id == null)
        {
            return false;
        }
        Vertices.Add(vertex.Id, vertex);
        Edges.Add(vertex.Id, new List<IEdge<string, PublicationDto>>());
        return true;
    }
    public IVertex<string, PublicationDto> GetVertex(string Id)
    {
        return Vertices[Id];
    }
    /// <summary>
    /// This adds an edge with the use of 2 Vertecies
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns>bool</returns>
    //TO:DO burde nok ikke bare retrune bool
    public bool AddEdge(IVertex<string, PublicationDto> from, IVertex<string, PublicationDto> to)
    {
        // Does vertices exist in table
        if (from.Id.Equals(to.Id))
        {
            return false;
        }
        if (from == null || !Vertices.ContainsKey(from.Id))
        {
            if (!AddVertex(from!)) return false;
        }
        if (to == null || !Vertices.ContainsKey(to.Id))
        {
            if (!AddVertex(to!)) return false;
        }
        var _from = Vertices[from!.Id];
        var _to = Vertices[to!.Id];

        if (Edges.Keys.Contains(_to.Id))
        {
            Edges[_to.Id].Add(new PublicationEdge(_from, _to));
        }
        else
        {
            throw new NotImplementedException();//TO:DO
        }

        if (Edges.Keys.Contains(_from.Id))
        {
            Edges[_from.Id].Add(new PublicationEdge(_from, _to));
        }
        else
        {
            throw new NotImplementedException();
        }
        return true;
    }
    /// <summary>
    /// This method uses the id of the 2 vertecies
    /// This is probobly also slow af
    /// </summary>
    /// <param name="fromId"></param>
    /// <param name="toId"></param>
    /// <returns>bool</returns>
    public bool AddEdge(string fromId, string toId)
    {
        if (fromId.Equals(toId))
        {
            return false;
        }
        if (!Vertices.ContainsKey(fromId)) return false;
        if (!Vertices.ContainsKey(toId)) return false;

        var from = Vertices[fromId];
        var to = Vertices[toId];

        if (Edges.Keys.Contains(fromId))
        {
            Edges[fromId].Add(new PublicationEdge(from, to));
        }
        else throw new NotImplementedException();//TODO:
        if (Edges.Keys.Contains(toId))
        {
            Edges[toId].Add(new PublicationEdge(to, from));
        }
        else throw new NotImplementedException();//TODO:
        return true;
    }
    /// <summary>
    /// This method uses the id of "from" and a new vertecies for "to"
    /// This is probobly also slow af
    /// </summary>
    /// <param name="fromId"></param>
    /// <param name="to"></param>
    /// <returns>bool</returns>
    public bool AddEdge(string fromId, IVertex<string, PublicationDto> to)
    {
        if (fromId.Equals(to.Id))
        { return false; }

        if (!Vertices.ContainsKey(fromId)) return false;
        var from = Vertices[fromId];

        if (to == null || !Vertices.ContainsKey(to.Id))
        {
            if (!AddVertex(to!)) return false;
        }
        var _to = Vertices[to!.Id];

        if (Edges.Keys.Contains(fromId))
        {
            Edges[fromId].Add(new PublicationEdge(from, _to));
        }
        else throw new NotImplementedException();//TODO:

        if (Edges.Keys.Contains(_to.Id))
        {
            Edges[_to.Id].Add(new PublicationEdge(from, _to));
        }
        else
        {
            throw new NotImplementedException();
        }
        return true;
    }
    /// <summary>
    /// This method uses the id of one of vertecies and a new vertecies for the second one
    /// This is probobly also slow af
    /// </summary>
    /// <param name="from"></param>
    /// <param name="toId"></param>
    /// <returns>bool</returns>
    public bool AddEdge(IVertex<string, PublicationDto> from, string toId)
    {
        if (from.Id.Equals(toId))
        { return false; }

        if (!Vertices.ContainsKey(toId)) return false;
        if (from == null || !Vertices.ContainsKey(from.Id))
        { if (!AddVertex(from!)) return false; }

        var to = Vertices[toId];
        var _from = Vertices[from!.Id];

        if (Edges.Keys.Contains(toId))
        { Edges[toId].Add(new PublicationEdge(to, from)); }
        else throw new NotImplementedException();//TODO:
        if (Edges.Keys.Contains(_from.Id))
        { Edges[_from.Id].Add(new PublicationEdge(_from, to)); }
        else
        { throw new NotImplementedException(); }

        return true;
    }
    /// <summary>
    /// This method adds a PublicationEdge 
    /// </summary>
    /// <param name="edge"></param>
    /// <returns>bool</returns>
    public bool AddEdge(IEdge<string, PublicationDto> edge)
    {
        return AddEdge(edge.GetFrom(), edge.GetTo());
    }
    public IEnumerable<IVertex<string, PublicationDto>> GetAdj(IVertex<string, PublicationDto> vertex)
    {
        var _vertex = Vertices[vertex.Id];
        //if (_vertex == null) throw new NotImplementedException();//TO:DO
        foreach (var edge in Edges[vertex.Id])
        {
            yield return edge.GetTo();
        }
    }
    public IEnumerable<IVertex<string, PublicationDto>> GetAdj(string vertexId)
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
    /// <summary>
    /// This method returns the vertex as well as the vertecies seperated by the degree
    /// </summary>
    /// <param name="startVertex"></param>
    /// <param name="degree"></param>
    /// <returns></returns>
    public IEnumerable<IVertex<String, PublicationDto>> DegreesOfSeperation(IVertex<String, PublicationDto> startVertex, int degree)
    {
        var closeVertecies = new HashSet<IVertex<String, PublicationDto>>();
        DegreesOfSeperationRecursive(startVertex, degree, closeVertecies);
        return closeVertecies;
    }
    private void DegreesOfSeperationRecursive(IVertex<String, PublicationDto> currentVertex, int degree, HashSet<IVertex<string, PublicationDto>> closeVertecies)
    {
        if (degree == 0) { closeVertecies.Add(currentVertex); }
        else
        {
            closeVertecies.Add(currentVertex);
            foreach (var vertex in GetAdj(currentVertex.Id))
            {
                DegreesOfSeperationRecursive(vertex, degree - 1, closeVertecies);
            }
        }
    }

    //kan bruges til at se den matematiske notering af en graph med 12 V og 
    public static void Main(string[] args)
    {
        var g = new PublicationGraph();
        g.AddVertex(new PublicationVertex("PublicationDto 0", new PublicationDto { Title = "PublicationDto 0" }));
        g.AddVertex(new PublicationVertex("PublicationDto 1", new PublicationDto { Title = "PublicationDto 1" }));
        g.AddVertex(new PublicationVertex("PublicationDto 2", new PublicationDto { Title = "PublicationDto 2" }));
        g.AddVertex(new PublicationVertex("PublicationDto 3", new PublicationDto { Title = "PublicationDto 3" }));
        g.AddVertex(new PublicationVertex("PublicationDto 4", new PublicationDto { Title = "PublicationDto 4" }));
        g.AddVertex(new PublicationVertex("PublicationDto 5", new PublicationDto { Title = "PublicationDto 5" }));
        g.AddVertex(new PublicationVertex("PublicationDto 6", new PublicationDto { Title = "PublicationDto 6" }));
        g.AddVertex(new PublicationVertex("PublicationDto 7", new PublicationDto { Title = "PublicationDto 7" }));
        g.AddVertex(new PublicationVertex("PublicationDto 8", new PublicationDto { Title = "PublicationDto 8" }));
        g.AddVertex(new PublicationVertex("PublicationDto 9", new PublicationDto { Title = "PublicationDto 9" }));
        g.AddVertex(new PublicationVertex("PublicationDto 10", new PublicationDto { Title = "PublicationDto 10" }));
        g.AddVertex(new PublicationVertex("PublicationDto 11", new PublicationDto { Title = "PublicationDto 11" }));
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
