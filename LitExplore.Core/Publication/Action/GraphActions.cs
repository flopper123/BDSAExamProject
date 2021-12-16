namespace LitExplore.Core.Publication.Action;

public class GraphAction 
{
    private Action<NodeDetails<PublicationNode>, PublicationGraph> _action;
    
    public GraphAction(Action<NodeDetails<PublicationNode>, PublicationGraph> a) {
        this._action = a;
    }

    public void Invoke(NodeDetails<PublicationNode> d, PublicationGraph g) {
        this._action(d, g);
    }
}