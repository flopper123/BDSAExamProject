namespace LitExplore.Core.Publication;

using System.Collections;
using System.Linq;
using LitExplore.Core.Filter;
using LitExplore.Core.Graph;

public class PublicationGraph {
    
    Dictionary<string, PublicationNode> nodes = new Dictionary<string, PublicationNode>();

    // last accessed root
    PublicationNode? prv_root = null;

    public int Size { get { return this.nodes.Count(); } }

    public IEnumerable<PublicationNode> GetNodes() {
        foreach (PublicationNode n in nodes.Values) {
            yield return n;
        }
    }

    // Parralel filtering of the entire graph
    public void Filter(Filter<NodeDetails<PublicationNode>> filter) {

        var toRemove = new ConcurrentBag<PublicationNode>();

        Parallel.ForEach(nodes.Values, n => {

            var d = new NodeDetails<PublicationNode>(n);
            if (filter.ShouldRemove(d)) toRemove.Add(n);
        });
        // cant parrelel remove unless we implement child and parents as a concurrent bag
        foreach (var removal in toRemove) Delete(removal.Details.Title);
    }

    // Filters a specific branch of the graph. Parameter @root decides what branch to pick.
    //
    // Traverse down the tree starting @Title and remove all objects that doesnt satisfy
    // filter for the current traversal.
    //
    // if no existing nodes hold a reference to a removed object, it will also be removed from the dictionary 
    //
    // A node will only be visisted multiple times, if the previous visit was at a shallower depth. 
    // Example:
    //      A Node is visited initially at depth 3. Later a nested filtering call revisits it at depth@9
    //      The revisit at depth@9 will result in a halt of the recursion branch.
    //
    public void FilterBranch(Filter<NodeDetails<PublicationNode>> filter, PublicationDtoTitle root)
    {
        // Maintainces the smallest depth node@PublicationDtoTitle has been visisted in.
        var visitHistory = new Dictionary<string, UInt32>(nodes.Count());
        
        // throws KeyNotFoundException if invalid title
        PublicationNode prv_root = nodes[root.Title];

        var details = new NodeDetails<PublicationNode>() {
            Details = prv_root,
            Depth = 1,
        };

        // remove root
        if (filter.ShouldRemove(details))
        {
            // reset
            Delete(prv_root.Details.Title);
            return;
        } 
        prv_root.FilterChildren(filter, visitHistory, 1);
    }

    // Expands the given PublicationDtoDetails into the graph.
    public void Add(PublicationDtoDetails details) {
        PublicationNode n = GetNode(details);
        n.UpdateDetails(details);
        AddChildren(n, details.References.ToList());
    }

    // Delete key from all children, parents and the nodes dictionary
    public void Delete(string key)
    {
        PublicationNode? node;
        // if not in dictionary, we just exit
        if (!nodes.TryGetValue(key, out node)) return;

        foreach (var c in node.Children) c.Parents.Remove(node);
        foreach (var p in node.Parents) p.Children.Remove(node);
        nodes.Remove(key);
    }

    // Returns true if atleast one child was added to target
    private bool AddChildren(PublicationNode tar, 
                             List<PublicationDtoTitle> newChildren) {

        bool hasAdded = false;
        // Check which children target is missing
        foreach (PublicationDtoTitle missing in tar.MissingChildren(newChildren)) {
            PublicationNode node = GetNode(missing);
            node.Parents.Add(tar);
            tar.Children.Add(node);
            hasAdded = true;
        }
        return hasAdded;
    }

    /// <summary>
    ///  retrieves the node saved under title from the dictionary.
    ///  If no such key found, it returns a new empty PublicationNode with attached details. 
    /// </summary>
    private PublicationNode GetNode(PublicationDtoTitle key) {
        
        PublicationNode? node = null;
        nodes.TryGetValue(key.Title, out node);
        
        if (node == null) {
            node = new PublicationNode(new PublicationDtoDetails { Title = key.Title } );
            this.nodes.Add(key.Title, node);
        }

        return node;
    }
}