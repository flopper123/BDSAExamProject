namespace LitExplore.Tests.Util;

public static class RandomizerExtensions 
{
    private static Collection<T> DEFAULT_COLLECTION<T>() {
        ConstructorInfo? cInfo = typeof(List<T>).GetConstructor(Type.EmptyTypes);
        if (cInfo == null) throw new NullReferenceException($"Failed reflective construction of Empty List<{typeof(T).Name}>");
        return (Collection<T>) cInfo.Invoke(new Object[0]);
    }

    public static UInt64 NextUInt64(this Random rnd) {
        var buffer = new byte[sizeof(UInt64)];
        rnd.NextBytes(buffer);
        return BitConverter.ToUInt64(buffer, 0);
    }

    public static Collection<T> NextEmptyCollection<T>(this Random rnd) 
    {
        // collection choices
        var rnd_cols = new Type[] 
        { 
            typeof(HashSet<T>), typeof(List<T>), typeof(Dictionary<int, T>),
            typeof(LinkedList<T>), typeof(PriorityQueue<T, int>), typeof(Queue<T>) 
        };

        Type rnd_t = rnd_cols[rnd.Next() % rnd_cols.Length];
        // Parameterless constructor
        ConstructorInfo? cInfo = rnd_t.GetConstructor(Type.EmptyTypes);
        if (cInfo == null) return DEFAULT_COLLECTION<T>();
        return (Collection<T>) cInfo.Invoke(new Object[0]);
    }

    /// <summary>
    /// Inserts all elements of target into a random collection
    /// </summary>
    public static Collection<T> RandomCollection<T>(this IEnumerable<T> tar) 
    {
        Random rnd = new Random();
        Collection<T> ret = rnd.NextEmptyCollection<T>();
        foreach(T t in tar) ret.Add(t);
        return ret;
    }

    public static Collection<T> RandomizedSubset<T>(this IEnumerable<T> tar) 
    {
        Random rnd = new Random();
        Collection<T> ret = rnd.NextEmptyCollection<T>();
        int N = tar.Count();

        SortedSet<int> indexes = new SortedSet<int>();
        for (int i = 0; i < N; i++) indexes.Add(rnd.Next() % N);
        
        var tar_i = 0;

        var i_enumerator = indexes.GetEnumerator();
        var tar_enumerator = tar.GetEnumerator();
        tar_enumerator.MoveNext();
        i_enumerator.MoveNext();
        
        while (i_enumerator.MoveNext()) {
            
            int next = i_enumerator.Current;

            while (tar_i != next) {
                tar_enumerator.MoveNext();
                tar_i++;
            }

            ret.Add(tar_enumerator.Current);
        }

        return ret;
    }
}