using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LitExplore.Core.Graph;

public class PublicationGraph : IGraph<PublicationDto, string>, 
                                IEnumerable,
                                IEnumerable<KeyValuePair<string, IVertex<PublicationDto, string>>>
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

    // TO:DO burde nok ikke bare return bool
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
    public int NumberOfVertices() => Vertices.Count;
    
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

    public IEnumerator<KeyValuePair<string, IVertex<PublicationDto, string>>> GetEnumerator()
    {
        return Vertices.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
