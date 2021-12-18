namespace LitExplore.Core.Graph;

public class NodeDetails<T> : IHasNodeDetails {
    public T? Details { get; init; }
    public UInt64 Depth { get; set; } = 0;

    public NodeDetails() : this(default(T)) {}

    public NodeDetails(T? details) {
        Details = details;
    }

    public NodeDetails(T? details, UInt64 depth) {
        Details = details;
        Depth = depth;
    }
}
