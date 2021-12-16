namespace LitExplore.Core.Filter;

public static class FilterOption 
{    
    [Flags]
    public enum SearchDirection 
    {
        CHILDREN = 0b1,
        PARENTS = 0b10,
        BOTH = 0b11,
        DEFAULT = BOTH,
    }
}