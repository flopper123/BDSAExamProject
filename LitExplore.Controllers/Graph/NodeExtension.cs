namespace LitExplore.Controllers.Graph;

public static class NodeExtension {
    public static VisualGraphRelationNode ToVisual(this PublicationNode n) 
    {
        
        if (n is VisualGraphRelationNode) return (VisualGraphRelationNode) n;
        
        // Maps a title to (x, y) using heuristics
        var point = (
            x: n.Details.Title.StringHeuristicEqualityFactor("first"),
            y: n.Details.Title.StringHeuristicEqualityFactor("second")
        );

        if (n is VisualGraphNode) point = ((VisualGraphNode) n).Point;
        
        return new VisualGraphRelationNode(n.Details, point);
    }

    // Returns a double based on how much actual reminds of comparing
    public static double StringHeuristicEqualityFactor(this string actual, string comparing) {
        // Make sure not to divide by 0
        if (actual.Length == 0 || comparing.Length == 0) return 0.0;

        // Compare how many chars are shared, divide the total amount by length of string to get a value in range [0-1]
        var chars =
        from c in actual
        where comparing.Contains(c, StringComparison.OrdinalIgnoreCase)
        select c;

        return ((double) chars.Count()) / ((double) actual.Length);
    }
}