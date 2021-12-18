namespace LitExplore.Tests.Utilities;

public static class GraphMockData {
    public static HashSet<PublicationDtoTitle> GetHashSet(params string[] titles) {
        HashSet<PublicationDtoTitle> set = new HashSet<PublicationDtoTitle>(titles.Length);
        foreach (string t in titles) set.Add(new PublicationDtoTitle { Title = t });
        return set;
    }
    
    /// 
    /// Created connected Cycle data by adding one child to the previous object,
    /// and setting the N'th child to point to fst.
    ///  
    ///  if N=1            if N = 3
    ///   _____       _________________
    ///  |     |     |                 |
    ///  v     |     v                 |
    ///  * --> *     * --> * --> * --> *
    ///  
    ///  The Title of the n'th node = n.ToString()
    ///  
    public static IEnumerable<PublicationDtoDetails> GetConnectedCycleData(int N) 
    {
        for (int i = 0; i < N; i++)
        {
            yield return new PublicationDtoDetails
            {
                Title = i.ToString(),
                References = GetHashSet($"{i + 1}"),
            };
        }

        yield return new PublicationDtoDetails { Title = $"{N}", References = GetHashSet("1") };
    }
    
    /// 
    /// Repeats the following pattern N times:
    ///   --> * -->
    /// * --> * --> *
    ///   --> * -->
    ///        
    ///  Where the objects @second row of * constitutes the @childCOunt                   
    internal static IEnumerable<PublicationDtoDetails> GetConnectedAcyclicData(int repeat, int childCount = 3) 
    {
        for (int d = 0; d < repeat; d++)
        {
            var refs = new HashSet<PublicationDtoTitle>();
            for (int ci = 0; ci < childCount; ci++)
            {
                refs.Add(new PublicationDtoTitle { Title = $"{d}{ci}" });
                yield return new PublicationDtoDetails { Title = $"{d}{ci}", References = GetHashSet($"{d+1}") };
            }
            yield return new PublicationDtoDetails { Title = $"{d}", References = refs };
        }
    }
}