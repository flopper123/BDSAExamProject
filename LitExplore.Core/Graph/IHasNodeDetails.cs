namespace LitExplore.Core.Graph;

public interface IHasNodeDetails {
    // Depth of node
    UInt64 Depth { get; init; }

    // Size of subtree rooted at this node
    UInt64 Size { get; init; }
}