namespace LitExplore.Entity.Filter;

static class FilterEnumFactory
{
    static Dictionary<Type, FilterType> T_To_filterT = null!;

    static FilterEnumFactory()
    {
        EmptyFilter<PublicationDto>.Get();

        initFilterTypeMap();
    }

    static void initFilterTypeMap()
    {
        FilterEnumFactory.T_To_filterT = new Dictionary<Type, FilterType>();
        FilterEnumFactory.T_To_filterT.Add(typeof(PublicationDto), FilterType.PUB);
        // Add future types like this:
        // FilterEnumFactory.T_To_filterT.Add(typeof(ArticleDto), FilterType.ART);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns> Extension method for filter type
    public static FilterType GetFilterType(this Type t)
    {
        FilterType ret;
        return (T_To_filterT.TryGetValue(t, out ret) ? ret : FilterType.NONE);
    }
}