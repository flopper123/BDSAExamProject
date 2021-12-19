namespace LitExplore.Core.Filter;

public static class FilterOption 
{    
    [Flags]
    public enum SearchDirection 
    {
        CHILDREN = 0b1,
        PARENTS = 0b10,
        BI = 0b11,

        // If a node is encountered twice, revisit it if the previous 
        // visitation depth is larger than current 
        VISIT_MINDEPTH = 0b100, 
        VISIT_ONCE = 0b1000, //  visit node exactly once, Not implemented
        DEFAULT = CHILDREN | VISIT_MINDEPTH,
    }

    public static bool IsSet(this SearchDirection src, SearchDirection needle) {
        return (src & needle) != 0;
    } 
}