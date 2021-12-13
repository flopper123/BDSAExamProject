namespace LitExplore.Core.Graph;

public class NodeDetails<T> : IHasNodeDetails {
    public T? Details { get; init; }
    public UInt64 Depth { get; init; }
    public UInt64 Size { get; init; }
}
