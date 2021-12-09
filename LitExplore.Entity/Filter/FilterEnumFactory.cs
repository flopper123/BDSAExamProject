namespace LitExplore.Entity.Filter;

public static class FilterEnumFactory
{
    static Dictionary<Type, FilterType> T_To_filterT = null!;

    static FilterEnumFactory()
    {
        initFilterTypeMap();
    }

    static void initFilterTypeMap()
    {
        FilterEnumFactory.T_To_filterT = new Dictionary<Type, FilterType>();
        FilterEnumFactory.T_To_filterT.Add(typeof(PublicationDto), FilterType.PUB);
        // Add future types like this:
        // FilterEnumFactory.T_To_filterT.Add(typeof(ArticleDto), FilterType.ART);
    }


    /// <summary> Save parse of an type to FilterType. Returns FilterType.None if no
    /// mapping for the input type exist. </summary>
    public static FilterType GetFilterType(this Type t)
    {
        FilterType ret;
        return (T_To_filterT.TryGetValue(t, out ret) ? ret : FilterType.NONE);
    }
}