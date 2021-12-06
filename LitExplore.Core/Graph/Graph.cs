namespace LitExplore.Core.Graph;

public class Graph<T> : IGraph<T>
    where T : IEquatable<T>
{
    private bool disposedValue;

    UInt64 _size = UInt64.MaxValue;

    public UInt64 Size
    {
        get => Root.Size;
    }

    public IVertex<T> Root { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Graph(IVertex<T> root)
    { // IGraph<T>
        Root = root;
    }

    public bool Add(IVertex<T> v)
    {
        throw new NotImplementedException();
    }

    public bool Delete(IVertex<T> v)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            Root = null;
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Graph()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    bool IGraph<T>.Add(IVertex<T> v)
    {
        throw new NotImplementedException();
    }

    bool IGraph<T>.Delete(IVertex<T> v)
    {
        throw new NotImplementedException();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    void IDisposable.Dispose()
    {
        throw new NotImplementedException();
    }
}

// public class PublicationGraph : IGraph<PublicationDto>, 
//                                 IEnumerable<KeyValuePair<string, IGraph<string, PublicationDto>>>
// {
//     private IDictionary<string, IGraph<string, PublicationDto>> Vertices;
//     private IDictionary<string, List<IEdge<PublicationDto, string>>> Edges;

//     public PublicationGraph()
//     {
//         Vertices = new Dictionary<string, IGraph<string, PublicationDto>>();
//         Edges = new Dictionary<string, List<IEdge<PublicationDto, string>>>();
//     }

//     public bool AddVertex(IGraph<string, PublicationDto> vertex)
//     {
//         if (Vertices.ContainsKey(vertex.Id) ||
//             vertex.Data.Title == null ||
//             vertex.Id == "" ||
//             vertex.Id == null)
//         {
//             return false;
//         }
//         Vertices.Add(vertex.Id, vertex);
//         Edges.Add(vertex.Id, new List<IEdge<PublicationDto, string>>());
//         return true;
//     }

//     // TO:DO burde nok ikke bare return bool
//     public bool AddEdge(IGraph<string, PublicationDto> from, IGraph<string, PublicationDto> to)
//     {
//         // Does vertices exist in table
//         if (from.Id.Equals(to.Id))
//         {
//             return false;
//         }
//         if (from == null || !Vertices.ContainsKey(from.Id))
//         {
//             if (!AddVertex(from)) return false;
//         }
//         if (to == null || !Vertices.ContainsKey(to.Id))
//         {
//             if (!AddVertex(to)) return false;
//         }
//         var _from = Vertices[from.Id];
//         var _to = Vertices[to.Id];

//         if (Edges.Keys.Contains(_to.Id))
//         {
//             Edges[_from.Id].Add(new Edge(_from, _to));
//         }
//         else
//         {
//             throw new NotImplementedException();//TO:DO
//         }

//         if (Edges.Keys.Contains(_to.Id))
//         {
//             Edges[_to.Id].Add(new Edge(_from, _to));
//         }
//         else
//         {
//             throw new NotImplementedException();
//         }
//         return true;
//     }

//     /// <summary>
//     /// This method uses the id of the vertecies (strings)
//     /// This is probobly also slow af
//     /// </summary>
//     /// <param name="fromId"></param>
//     /// <param name="toId"></param>
//     /// <returns></returns>
//     public bool AddEdge(string fromId, string toId)
//     {
//         if (!Vertices.ContainsKey(fromId)) return false;
//         if (!Vertices.ContainsKey(toId)) return false;
//         var from = Vertices[fromId];
//         var to = Vertices[toId];
//         if (Edges.Keys.Contains(fromId))
//         {
//             Edges[fromId].Add(new Edge(from, to));
//         }
//         else throw new NotImplementedException();//TO:DO
//         if (Edges.Keys.Contains(toId))
//         {
//             Edges[toId].Add(new Edge(to, from));
//         }
//         else throw new NotImplementedException();//TO:DO
//         return true;
//     }

//     public IEnumerable<IGraph<string, PublicationDto>> GetAdj(IGraph<string, PublicationDto> vertex)
//     {
//         var _vertex = Vertices[vertex.Id];
//         //if (_vertex == null) throw new NotImplementedException();//TO:DO
//         foreach (var edge in Edges[vertex.Id])
//         {
//             yield return edge.GetTo();
//         }
//     }
//     public IEnumerable<IGraph<string, PublicationDto>> GetAdj(string vertexId)
//     {
//         var _vertex = Vertices[vertexId];
//         if (_vertex == null) throw new NotImplementedException(); //TO:DO
//         foreach (var edge in Edges[vertexId])
//         {
//             yield return edge.GetTo();
//         }
//     }
//     public int NumberOfVertices() => Vertices.Count;

//     public int NumberOfEdges()
//     {
//         var numEdges = 0;
//         foreach (var edges in Edges.Values)
//         {
//             foreach (var edge in edges)
//             {
//                 numEdges++;
//             }
//         }
//         return numEdges / 2;//This is divided in 2 because both of the vetecies of an edge knows the edge
//     }

//     public IEnumerator<KeyValuePair<string, IGraph<string, PublicationDto>>> GetEnumerator()
//     {
//         return Vertices.GetEnumerator();
//     }

//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return GetEnumerator();
//     }

//     public override string ToString()
//     {
//         string s = "";
//         s += NumberOfVertices() + " vertices, " + NumberOfEdges() + " edges \n";
//         foreach (var v in Vertices.Keys)
//         {
//             s += v + ": ";
//             foreach (var w in GetAdj(v))
//             {
//                 s += w.Id + " ";
//             }
//             s += "\n";
//         }
//         return s;
//     }
