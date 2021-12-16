namespace LitExplore.Core.Publication;

using System.Collections;
using System.Linq;
using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;
using LitExplore.Core.Graph;

public class PublicationGraph : ISerialize
{
    protected Dictionary<string, PublicationNode> _Nodes = new Dictionary<string, PublicationNode>();
    protected Filter<PublicationGraph> fhistory = EmptyFilter<PublicationGraph>.Get(); // holds all applied filter
    
    // Public access so we can modify from filters
    public Dictionary<string, PublicationNode> Nodes {
        get { return this._Nodes;  }
        set { this._Nodes = value; }
    }
    
    public Filter<PublicationGraph> History {
        get { return this.fhistory;  }
        set { fhistory = value; }     
    }

    public int Size { get { return this.Nodes.Count(); } }

    public string Serialize() { return this.fhistory.Serialize(); }

    /// <summary>
    ///  retrieves the node saved under title from the dictionary.
    ///  If no such key found, it returns a new empty PublicationNode with attached details. 
    /// </summary>
    public PublicationNode TryGetNode(PublicationDtoTitle key) {
        
        PublicationNode? node = null;
        Nodes.TryGetValue(key.Title, out node);
        
        if (node == null) {
            node = new PublicationNode(new PublicationDtoDetails { Title = key.Title } );
            this.Nodes.Add(key.Title, node);
        }

        return node;
    }

    // Returns default if no such key found
    public PublicationNode? GetNode(string key) { return this.Nodes.GetValueOrDefault(key); }

    public IEnumerable<PublicationNode> GetNodes() {
        foreach (PublicationNode n in Nodes.Values) {
            yield return n;
        }
    }

    // Transforms this graph by applying the transformation of parameter @f
    public void Filter(Filter<PublicationGraph> f) {
        this.fhistory = this.fhistory.Decorate(f);
        f.Invoke(this); 
    }

    // Expands the given PublicationDtoDetails into the graph.
    public void Add(PublicationDtoDetails details) {
        PublicationNode n = TryGetNode(details);
        n.UpdateDetails(details);
        AddChildren(n, details.References.ToList());
    }

    // For now it just shallow copy nodes
    public void Copy(PublicationGraph other) { this.Nodes = other.Nodes; }

    // Delete key from all children, parents and the Nodes dictionary
    public void Delete(string key)
    {
        PublicationNode? node;
        // if not in dictionary, we just exit
        if (!Nodes.TryGetValue(key, out node)) return;

        foreach (var c in node.Children) c.Parents.Remove(node);
        foreach (var p in node.Parents) p.Children.Remove(node);
        Nodes.Remove(key);
    }

    
    // Returns true if atleast one child was added to target
    private bool AddChildren(PublicationNode tar, 
                             List<PublicationDtoTitle> newChildren) {

        bool hasAdded = false;
        // Check which children target is missing
        foreach (PublicationDtoTitle missing in tar.MissingChildren(newChildren)) {
            PublicationNode node = TryGetNode(missing);
            node.Parents.Add(tar);
            tar.Children.Add(node);
            hasAdded = true;
        }
        return hasAdded;
    }
}